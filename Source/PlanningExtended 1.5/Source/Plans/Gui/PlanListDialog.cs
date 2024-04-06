using System;
using System.Collections.Generic;
using System.IO;
using PlanningExtended.Plans.Persistence;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Plans.Gui
{
    public abstract class PlanListDialog : Window
    {
        readonly Color DefaultFileTextColor = new(1f, 1f, 0.6f);

        bool isPlanNameAreaFocused = false;

        Vector2 scrollPosition = Vector2.zero;

        string typingName = "";

        protected List<PlanFileInfo> files = [];

        protected virtual string InteractButtonLabel => "Error";

        protected virtual bool DisplayTypeInField => false;

        public override Vector2 InitialSize => new(620f, Mathf.Min(700f, UI.screenHeight));

        protected PlanListDialog()
        {
            absorbInputAroundWindow = true;
            closeOnAccept = false;
            closeOnClickedOutside = false;
            doCloseButton = true;
            doCloseX = true;
            forcePause = true;
            
            ReloadFiles();
        }

        public override void DoWindowContents(Rect inRect)
        {
            Vector2 rowSize = new(inRect.width - 16f, 40f);
            
            float height = (float)files.Count * rowSize.y;
            float y = inRect.height - Window.CloseButSize.y - 18f;
            
            Rect viewRect = new(0f, 0f, inRect.width - 16f, height);

            if (DisplayTypeInField)
                y -= 53f;

            Rect outRect = inRect.TopPartPixels(y);
            
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, true);
            
            y = 0f;
            int rowCount = 0;

            foreach (PlanFileInfo planFileInfo in files)
            {
                if (y + rowSize.y >= scrollPosition.y && y <= scrollPosition.y + outRect.height)
                {
                    Rect rowRect = new(0f, y, rowSize.x, rowSize.y);
                    
                    if (rowCount % 2 == 0)
                        Widgets.DrawAltRect(rowRect);
                    
                    Widgets.BeginGroup(rowRect);
                    
                    Rect deleteRect = new(rowRect.width - 36f, (rowRect.height - 36f) / 2f, 36f, 36f);

#if RIMWORLD_1_4
                    if (Widgets.ButtonImage(deleteRect, TexButton.DeleteX, Color.white, GenUI.SubtleMouseoverColor, true))
#else
                    if (Widgets.ButtonImage(deleteRect, TexButton.Delete, Color.white, GenUI.SubtleMouseoverColor, true))
#endif
                    {
                        FileInfo localFile = planFileInfo.FileInfo;
                        Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation("ConfirmDelete".Translate(localFile.Name), delegate
                        {
                            localFile.Delete();

                            PlanningMod.Settings.Plan.RemoveLastLoadedPlan(Path.GetFileNameWithoutExtension(localFile.Name));

                            ReloadFiles();
                        }, true, null, WindowLayer.Dialog));
                    }
                    
                    TooltipHandler.TipRegionByKey(deleteRect, "PlanningExtended.DeletePlan");
                    
                    Text.Font = GameFont.Small;
                    Rect interactButtonRect = new(deleteRect.x - 100f, (rowRect.height - 36f) / 2f, 100f, 36f);
                    
                    if (Widgets.ButtonText(interactButtonRect, InteractButtonLabel, true, true, true, null))
                        DoFileInteraction(Path.GetFileNameWithoutExtension(planFileInfo.FileName));
                    
                    Rect metaDataRect = new(interactButtonRect.x - 94f, 0f, 94f, rowRect.height);
                    
                    DoMetaData(planFileInfo, metaDataRect);
                    
                    GUI.color = Color.white;
                    Text.Anchor = TextAnchor.UpperLeft;
                    GUI.color = DefaultFileTextColor;
                    
                    Rect nameRect = new(8f, 0f, metaDataRect.x - 8f - 4f, rowRect.height);
                    Text.Anchor = TextAnchor.MiddleLeft;
                    Text.Font = GameFont.Small;
                    
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(planFileInfo.FileName);
                    
                    Widgets.Label(nameRect, fileNameWithoutExtension.Truncate(nameRect.width * 1.8f, null));
                    GUI.color = Color.white;
                    Text.Anchor = TextAnchor.UpperLeft;
                    
                    Widgets.EndGroup();
                }
                y += rowSize.y;
                rowCount++;
            }
            
            Widgets.EndScrollView();
            
            if (DisplayTypeInField)
                DoTypeInField(inRect.TopPartPixels(inRect.height - Window.CloseButSize.y - 18f));
        }

        public static void DoMetaData(PlanFileInfo planFileInfo, Rect rect)
        {
            Widgets.BeginGroup(rect);

            Text.Font = GameFont.Tiny;
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = PlanFileInfo.UnimportantTextColor;

            Rect lastTimeWriteRect = new(0f, 2f, rect.width, rect.height / 2f);

            Widgets.Label(lastTimeWriteRect, planFileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));

            Widgets.EndGroup();
        }

        protected abstract void DoFileInteraction(string fileName);

        protected void ReloadFiles()
        {
            files.Clear();

            foreach (FileInfo fileInfo in PlanPersistenceManager.GetAllPlanInfoFiles())
            {
                try
                {
                    PlanFileInfo saveFileInfo = new(fileInfo);
                    saveFileInfo.LoadData();
                    files.Add(saveFileInfo);
                }
                catch (Exception ex)
                {
                    Log.Error("Exception loading " + fileInfo.Name + ": " + ex.ToString());
                }
            }
        }

        protected virtual void DoTypeInField(Rect rect)
        {
            Widgets.BeginGroup(rect);

            bool flag = Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return;
            float y = rect.height - 35f;

            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.MiddleLeft;

            GUI.SetNextControlName("PlanNameField");

            string fileName = Widgets.TextField(new Rect(5f, y, 400f, 35f), typingName);

            if (GenText.IsValidFilename(fileName))
                typingName = fileName;

            if (!isPlanNameAreaFocused)
            {
                UI.FocusControl("PlanNameField", this);
                isPlanNameAreaFocused = true;
            }

            if (Widgets.ButtonText(new Rect(420f, y, rect.width - 400f - 20f, 35f), "SaveGameButton".Translate(), true, true, true, null) || flag)
            {
                if (typingName.NullOrEmpty())
                {
                    Messages.Message("NeedAName".Translate(), MessageTypeDefOf.RejectInput, false);
                }
                else
                {
                    string text = typingName;
                    DoFileInteraction(text?.Trim());
                }
            }

            Text.Anchor = TextAnchor.UpperLeft;

            Widgets.EndGroup();
        }
    }
}
