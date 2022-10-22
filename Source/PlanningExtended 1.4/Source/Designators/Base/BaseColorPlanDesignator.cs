using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BaseColorPlanDesignator : BaseShapePlanDesignator
    {
        public override int DraggableDimensions => IsColorPickModeEnabled ? colorPicker.DraggableDimensions : base.DraggableDimensions;

        protected ColorPickerDesignator colorPicker;

        protected ColorDef colorDef;

        protected bool IsColorPickModeEnabled { get; private set; }

        protected abstract DesignationDef ColoredDesignation { get; }

        protected DesignationDef SelectedDesignation => colorDef == ColorDefinitions.NonColoredDef ? Designation : ColoredDesignation;

        protected BaseColorPlanDesignator(string name)
            : base(name)
        {
            defaultDesc = $"PlanningExtended.{name}.Desc".Translate(KeyBindingDefOf.ShowEyedropper.MainKeyLabel);

            colorDef = ColorDefinitions.NonColoredDef;
            colorPicker = new ColorPickerDesignator((newColorDef) =>
            {
                colorDef = newColorDef;
                ResetMouseAttachmentText();

                if (!IsColorPickModeEnabled)
                    Find.DesignatorManager.Select(this);
            }, "PlanningExtended.SelectColoredPlan".Translate(), "PlanningExtended.PickColorFromPlan".Translate());
        }

        public override void DrawMouseAttachments()
        {
            IsColorPickModeEnabled = KeyBindingDefOf.ShowEyedropper.IsDown;
            CheckModifierKey();

            if (IsColorPickModeEnabled)
            {
                colorPicker.DrawMouseAttachments();
                return;
            }

            if (useMouseIcon)
                GenUI.DrawMouseAttachment(icon, MouseAttachmentText, iconAngle, iconOffset, null, false, default, new Color?(colorDef.color));
        }

        protected override void ShowPopupMenu()
        {
            base.ShowPopupMenu();

            if (KeyBindingDefOf.ShowEyedropper.IsDown)
            {
                List<FloatMenuGridOption> list = new(ColorDefinitions.ColorDefs.Count + 1)
                {
                    new FloatMenuGridOption(Designator_Eyedropper.EyeDropperTex, () =>
                    {
                        Find.DesignatorManager.Select(colorPicker);
                    }, null, new TipSignal?("DesignatorEyeDropperDesc_Paint".Translate()))
                };

                foreach (ColorDef colorDef in ColorDefinitions.ColorDefs)
                {
                    list.Add(new FloatMenuGridOption(BaseContent.WhiteTex, () =>
                    {
                        Find.DesignatorManager.Select(this);
                        this.colorDef = colorDef;
                        ResetMouseAttachmentText();

                    }, new Color?(colorDef.color), new TipSignal?(colorDef.LabelCap)));
                }

                Find.WindowStack.Add(new FloatMenuGrid(list));
            }
        }

        protected override string GetMouseAttachmentText()
        {
            return "Color".Translate() + ": " + this.colorDef.LabelCap + "\n" + KeyBindingDefOf.ShowEyedropper.MainKeyLabel + ": " + "GrabExistingColor".Translate();
        }
    }
}
