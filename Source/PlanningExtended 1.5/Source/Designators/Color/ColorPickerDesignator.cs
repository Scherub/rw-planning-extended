using System;
using RimWorld;
using Verse;

namespace PlanningExtended.Designators
{
    public class ColorPickerDesignator : Designator
    {
        readonly Action<ColorDef> _colorPicked;
        
        readonly string _rejectMessage;

        public ColorPickerDesignator(Action<ColorDef> colorPicked, string rejectMessage, string desc)
        {
            _colorPicked = colorPicked;
            _rejectMessage = rejectMessage;

            defaultLabel = "DesignatorEyedropper".Translate();
            defaultDesc = desc;
            icon = Designator_Eyedropper.EyeDropperTex;
            useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (GetColorDefAt(c) != null)
                return AcceptanceReport.WasAccepted;

            if (!_rejectMessage.NullOrEmpty())
                return _rejectMessage;
            
            return false;
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            ColorDef colorDef = GetColorDefAt(cell);
            
            if (colorDef != null)
            {
                _colorPicked?.Invoke(colorDef);

                Messages.Message("GrabbedColor".Translate() + ": " + colorDef.LabelCap, null, MessageTypeDefOf.NeutralEvent, false);
                return;
            }
            
            if (!_rejectMessage.NullOrEmpty())
                Messages.Message(_rejectMessage, null, MessageTypeDefOf.RejectInput, false);
        }

        public override void DrawMouseAttachments()
        {
            if (useMouseIcon)
            {
                string text = string.Empty;
                ColorDef colorDef = GetColorDefAt(UI.MouseCell());
                
                if (colorDef != null)
                    text = "Grab".Translate() + ": " + colorDef.LabelCap;
                else if (!_rejectMessage.NullOrEmpty())
                    text = _rejectMessage;
                
                GenUI.DrawMouseAttachment(icon, text, iconAngle, iconOffset, null, false, default, null, null);
            }
        }

        ColorDef GetColorDefAt(IntVec3 cell)
        {
            if (!cell.InBounds(Map))
                return null;

            Designation designation = Map.designationManager.GetPlanDesignationAt(cell);

            if (designation == null)
                return null;

            return designation.colorDef;
        }
    }
}
