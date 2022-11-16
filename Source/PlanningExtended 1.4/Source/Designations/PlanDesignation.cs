using PlanningExtended.Defs;
using PlanningExtended.Materials;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designations
{
    public class PlanDesignation : Designation
    {
        PlanDesignationType planType;

        PlanDesignationUpdateType _planDesignationUpdateType;

        [Unsaved]
        Material _material;
        Material Material
        {
            get
            {
                if (_material is null || _planDesignationUpdateType is PlanDesignationUpdateType.Material)
                {
                    if (planType is PlanDesignationType.Unknown)
                        DeterminePlanType();

                    if (colorDef != null)
                    {
                        _material = new(def.iconMat)
                        {
                            color = colorDef.color.ToTransparent(MaterialsManager.GetPlanOpacity(planType))
                        };
                    }
                    else
                    {
                        _material = def.iconMat;
                    }

                    _planDesignationUpdateType = PlanDesignationUpdateType.None;
                }
                else if (_planDesignationUpdateType is PlanDesignationUpdateType.Color)
                {
                    _material.color = colorDef.color.ToTransparent(MaterialsManager.GetPlanOpacity(planType));
                    _planDesignationUpdateType = PlanDesignationUpdateType.None;
                }

                return _material;
            }
        }

        public PlanDesignation()
            : base()
        {
        }

        public PlanDesignation(LocalTargetInfo target, DesignationDef def, ColorDef colorDef = null)
            : base(target, def, colorDef)
        {
            DeterminePlanType();
        }

        public override void DesignationDraw()
        {
            if (!PlanManager.IsPlanVisible(planType))
                return;

            Graphics.DrawMesh(MeshPool.plane10, DrawLoc(), Quaternion.identity, Material, 0);
        }

        internal void InvokeUpdate(PlanDesignationType planDesignationType, PlanDesignationUpdateType planDesignationUpdateType)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planType)
                _planDesignationUpdateType = planDesignationUpdateType;
        }

        void DeterminePlanType()
        {
            planType = DesignationDefUtilities.GetType(def);
        }
    }
}
