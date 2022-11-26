using System.Collections.Generic;
using System.Linq;
using PlanningExtended.Cells;
using PlanningExtended.Defs;
using PlanningExtended.Designations;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine.UIElements;
using Verse;
using Verse.Noise;

namespace PlanningExtended.Designators
{
    public class RemovePlanDesignator : BaseShapePlanDesignator
    {
        protected override DesignationDef Designation => PlanningDesignationDefOf.PlanObjects;

        public override bool DragDrawOutline => true;

        protected override bool HasLeftClickPopupMenu => false;

        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions => GetRemovePlanMenuOptions();

        public RemovePlanDesignator()
            : base("RemovePlan")
        {
            soundSucceeded = SoundDefOf.Designate_PlanRemove;
            hotKey = KeyBindingDefOf.Designator_Cancel;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c))
                return false;

            return Map.designationManager.HasPlanDesignationAt(c);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            Map.designationManager.RemovePlanDesignationsAt(c);

            PlanDesignationPlacerUtilities.UpdateAdjecentPositions(Map, c);
        }

        List<FloatMenuOption> GetRemovePlanMenuOptions()
        {
            List<FloatMenuOption> floatMenuOptionList = new();

            foreach (DesignationDefContainer designationDefContainer in PlanningDesignationDefOf.DesignationDefs)
            {
                List<Designation> designationsDefault = Map.designationManager.designationsByDef[designationDefContainer.Default];
                List<Designation> designationsColored = Map.designationManager.designationsByDef[designationDefContainer.Colored];

                int designationsCount = designationsDefault.Count + designationsColored.Count;

                if (designationsCount > 0)
                {
                    //designationDefContainer.Default.LabelCap
                    floatMenuOptionList.Add(new FloatMenuOption("PlanningExtended.RemovePlanDesignations".Translate(designationsCount, designationDefContainer.Label.Translate()), () =>
                    {
                        List<Designation> designationsDefault = Map.designationManager.designationsByDef[designationDefContainer.Default];
                        List<Designation> designationsColored = Map.designationManager.designationsByDef[designationDefContainer.Colored];

                        AreaDimensions areaDimensions = CellUtilities.DetermineAreaDimensions(designationsDefault.Select(d => d.target.Cell));
                        AreaDimensions areaDimensionsColored = CellUtilities.DetermineAreaDimensions(designationsColored.Select(d => d.target.Cell));

                        areaDimensions = areaDimensions.Merge(areaDimensionsColored);

                        PlanLayout undoPlanLayout = CreateUndoPlanLayout(areaDimensions);

                        Map.designationManager.RemoveDesignations(designationsDefault);
                        Map.designationManager.RemoveDesignations(designationsColored);

                        CreateRedoPlanLayout(undoPlanLayout);
                    }));
                }
            }

            return floatMenuOptionList;
        }
    }
}
