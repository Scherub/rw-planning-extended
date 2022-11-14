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
        
        bool requiresMaterialUpdate, requiresColorUpdate;

        [Unsaved]
        Material _material;
        Material Material
        {
            get
            {
                if (_material is null || requiresMaterialUpdate)
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

                    requiresMaterialUpdate = false;
                }

                if (requiresColorUpdate)
                {
                    _material.color = colorDef.color.ToTransparent(MaterialsManager.GetPlanOpacity(planType));
                    requiresColorUpdate = false;
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
            if (!PlanManager.ArePlansVisible)
                return;

            Graphics.DrawMesh(MeshPool.plane10, DrawLoc(), Quaternion.identity, Material, 0);
        }

        public void InvokeMaterialUpdate(PlanDesignationType planDesignationType)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == planType)
                requiresMaterialUpdate = true;
        }

        public void InvokeColorUpdate(PlanDesignationType planDesignationType)
        {
            if (planDesignationType is PlanDesignationType.Unknown || planDesignationType == planType)
                requiresColorUpdate = true;
        }

        void DeterminePlanType()
        {
            planType = DesignationDefUtilities.GetType(def);
        }
    }
}
