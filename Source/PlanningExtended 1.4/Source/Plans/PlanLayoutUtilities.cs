using System.Collections.Generic;
using PlanningExtended.Cells;
using PlanningExtended.Colors;
using PlanningExtended.Designations;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Plans
{
    public static class PlanLayoutUtilities
    {
        //public static void RenderHighlightOverSelectableCells(Designator designator, List<IntVec3> dragCells)
        //{
        //    foreach (IntVec3 intVec in dragCells)
        //    {
        //        Vector3 position = intVec.ToVector3Shifted();
        //        position.y = AltitudeLayer.MetaOverlays.AltitudeFor();
        //        Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, DesignatorUtility.DragHighlightCellMat, 0);
        //    }
        //}

        public static PlanLayout CreateCopy(AreaDimensions areaDimensions, Map map)
        {
            List<PlanCell> planCells = new();

            foreach (var cell in CellUtilities.GetCells(areaDimensions))
            {
                Designation designation = map.designationManager.GetPlanDesignationAt(cell);

                if (designation == null)
                    continue;

                PlanDesignitionType planDesignitionType = PlanDesignitionTypeConverter.Convert(designation.def.defName);

                PlanCell planCell = new(cell.ToIntVec2, planDesignitionType, designation.colorDef.defName);

                planCells.Add(planCell);
            }

            PlanLayout planLayout = new(planCells, areaDimensions);

            //Log.Warning($"Copy of PlanLayout {planLayout} created.");

            return planLayout;
        }

        public static PlanLayout Create(CellArea cellArea, Map map)
        {
            List<PlanCell> planCells = new();

            foreach (var cell in cellArea.Cells)
            {
                Designation designation = map.designationManager.GetPlanDesignationAt(cell);

                IntVec3 position = cell - cellArea.Dimensions.Min;

                PlanDesignitionType planDesignitionType = PlanDesignitionTypeConverter.Convert(designation.def.defName);

                PlanCell planCell = new(position.ToIntVec2, planDesignitionType, designation.colorDef.defName);

                //Log.Warning($"PlanCell: {planCell}");

                planCells.Add(planCell);
            }

            PlanLayout planLayout = new(planCells);

            //Log.Warning($"PlanLayout {planLayout} created.");

            return planLayout;
        }

        public static PlanLayout Flip(PlanLayout planLayout, FlipDirection flipDirection)
        {
            IntVec2 areaSize = planLayout.Dimensions.Max.ToIntVec2;

            List<PlanCell> planCells = new(planLayout.CellCount);

            foreach (PlanCell planCell in planLayout.Cells)
                planCells.Add(planCell.Flip(flipDirection, areaSize));

            return new PlanLayout(planCells);
        }

        public static PlanLayout Rotate(PlanLayout planLayout, RotationDirection rotationDirection)
        {
            IntVec2 areaSize = planLayout.Dimensions.Max.ToIntVec2;

            IntVec2 offset = areaSize.Rotate(rotationDirection).DetermineOffsetForRotation();

            List<PlanCell> planCells = new(planLayout.CellCount);

            foreach (PlanCell planCell in planLayout.Cells)
                planCells.Add(planCell.Rotate(rotationDirection, offset));

            return new PlanLayout(planCells);
        }

        public static void Draw(PlanLayout planLayout, IntVec3 origin, Map map)
        {
            //origin -= planLayout.FindMostBottomRightCell().ToIntVec3;

            List<IntVec3> list = new();

            foreach (PlanCell planCell in planLayout.Cells)
            {
                IntVec3 position = planCell.Position.ToIntVec3 + origin;

                if (!position.InBounds(map) || position.InNoBuildEdgeArea(map))
                    continue;

                Vector3 vector = position.ToVector3Shifted();
                vector.y = AltitudeLayer.MetaOverlays.AltitudeFor();

                DesignationDef designationDef = DesignationUtilities.GetDesignationDef(planCell.Designation);

                Graphics.DrawMesh(MeshPool.plane10, vector, Quaternion.identity, designationDef.iconMat, 0);

                list.Add(position);
            }

            //GenDraw.DrawFieldEdges(list);
        }

        public static void DesignateSnapshot(PlanLayout planLayout, Map map)
        {
            CellUtilities.ClearCells(planLayout.Dimensions, map);

            foreach (PlanCell planCell in planLayout.Cells)
            {
                IntVec3 position = planCell.Position.ToIntVec3;

                DesignationDef designationDef = DesignationUtilities.GetDesignationDef(planCell.Designation);
                ColorDef colorDef = ColorUtilities.GetColorDefByName(planCell.Color);

                map.designationManager.AddDesignation(new Designation(position, designationDef, colorDef));
            }
        }

        public static void Designate(PlanLayout planLayout, IntVec3 origin, Map map)
        {
            //origin -= planLayout.FindMostBottomRightCell().ToIntVec3;

            foreach (PlanCell planCell in planLayout.Cells)
            {
                IntVec3 position = planCell.Position.ToIntVec3 + origin;

                if (!position.InBounds(map) || position.InNoBuildEdgeArea(map))
                    continue;

                map.designationManager.RemovePlanDesignationsAt(position);

                DesignationDef designationDef = DesignationUtilities.GetDesignationDef(planCell.Designation);
                ColorDef colorDef = ColorUtilities.GetColorDefByName(planCell.Color);

                map.designationManager.AddDesignation(new Designation(position, designationDef, colorDef));
            }
        }
    }
}
