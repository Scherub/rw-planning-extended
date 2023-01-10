using System.Collections.Generic;
using PlanningExtended.Plans.Appearances;
using PlanningExtended.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designators
{
    public abstract class BasePlanDesignator : Designator
    {
        public override int DraggableDimensions => 2;

        public override bool DragDrawMeasurements => true;

        public override bool DragDrawOutline => false;

        protected PlanningSettings Settings => PlanningMod.Settings;

        protected virtual bool DrawMapBounds => true;

        protected virtual bool DrawMouseOverBrackets => true;

        protected virtual bool HasLeftClickPopupMenu => false;

        protected bool IsModifierKeyPressed { get; private set; }

        protected bool IsSkipExistingDesignationsKeyPressed { get; private set; }

        string _mouseAttachmentText;
        protected string MouseAttachmentText
        {
            get
            {
                _mouseAttachmentText ??= GetMouseAttachmentText();

                return _mouseAttachmentText;
            }
        }

        protected BasePlanDesignator(string name)
        {
            defaultLabel = $"PlanningExtended.Designator.{name}.Label".Translate();
            defaultDesc = $"PlanningExtended.Designator.{name}.Desc".Translate();
            icon = GetIcon(name);

            soundSucceeded = SoundDefOf.Designate_PlanAdd;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override void ProcessInput(Event ev)
        {
            if (!CheckCanInteract())
                return;

            bool handleDfaultProcessInput = HasLeftClickPopupMenu ? !ShowLeftClickPopupMenu() : true;

            if (handleDfaultProcessInput)
                base.ProcessInput(ev);

            Find.DesignatorManager.Select(this);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            if (!loc.InBounds(Map))
                return false;

            if (loc.InNoBuildEdgeArea(Map))
                return "TooCloseToMapEdge".Translate();

            return true;
        }

        public override void DrawMouseAttachments()
        {
            CheckPressedKeys();

            if (useMouseIcon)
                GenUI.DrawMouseAttachment(icon, MouseAttachmentText, iconAngle, iconOffset, null, false, default, null, null);
        }

        //public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        //{
        //    GizmoResult gizmoResult = base.GizmoOnGUI(topLeft, maxWidth, parms);

        //    if (HasLeftClickPopupMenu)
        //        Designator_Dropdown.DrawExtraOptionsIcon(topLeft, this.GetWidth(maxWidth));

        //    return gizmoResult;
        //}

        public override void RenderHighlight(List<IntVec3> dragCells)
        {
            DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);
        }

        public override void SelectedUpdate()
        {
            PlanAppearanceManager.SetIsPlanVisible(PlanDesignationType.Unknown, true);

            if (DrawMouseOverBrackets)
                GenUI.RenderMouseoverBracket();

            if (DrawMapBounds)
                GenDraw.DrawNoBuildEdgeLines();
        }

        protected virtual bool ShowLeftClickPopupMenu()
        {
            return false;
        }

        protected virtual Texture2D GetIcon(string name)
        {
            return ContentFinder<Texture2D>.Get($"UI/Designators/{name}", true);
        }

        protected virtual string GetMouseAttachmentText()
        {
            return "";
        }

        protected virtual void CheckPressedKeys()
        {
            if (IsModifierKeyPressed != PlanningKeyBindingDefOf.Planning_Modifier.IsDown)
            {
                IsModifierKeyPressed = PlanningKeyBindingDefOf.Planning_Modifier.IsDown;
                OnModifierKeyChanged(IsModifierKeyPressed);
            }

            if (IsSkipExistingDesignationsKeyPressed != PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.IsDown)
            {
                IsSkipExistingDesignationsKeyPressed = PlanningKeyBindingDefOf.Planning_NoOverwrite_Mode.IsDown;
                OnSkipExistingDesignationsKeyChanged(IsModifierKeyPressed);
            }
        }

        protected virtual void OnModifierKeyChanged(bool isPressed)
        {

        }

        protected virtual void OnSkipExistingDesignationsKeyChanged(bool isPressed)
        {

        }

        protected void ResetMouseAttachmentText()
        {
            _mouseAttachmentText = null;
        }
    }
}
