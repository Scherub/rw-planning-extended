using PlanningExtended.Materials;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designations
{
    public class PlanDesignation : Designation
    {
        readonly PlanDesignitionType planType;
        
        bool requiresMaterialUpdate, requiresColorUpdate;

        [Unsaved]
        Material _material;
        Material Material
        {
            get
            {
                if (_material is null || requiresMaterialUpdate)
                {
                    if (colorDef != null)
                    {
                        _material = new(def.iconMat)
                        {
                            color = colorDef.color.ToTransparent(MaterialsManager.PlanOpacityAlpha)
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
                    _material.color = colorDef.color.ToTransparent(MaterialsManager.PlanOpacityAlpha);
                    requiresColorUpdate = false;
                }

                return _material;
            }
        }

        public PlanDesignation()
            : base()
        {
            planType = PlanningDesignationDefOf.GetType(def);
        }

        public PlanDesignation(LocalTargetInfo target, DesignationDef def, ColorDef colorDef = null)
            : base(target, def, colorDef)
        {
            planType = PlanningDesignationDefOf.GetType(def);
        }

        public override void DesignationDraw()
        {
            if (!PlanManager.ArePlansVisible)
                return;

            Graphics.DrawMesh(MeshPool.plane10, DrawLoc(), Quaternion.identity, Material, 0);
        }

        public void InvokeMaterialUpdate(PlanDesignitionType planDesignitionType)
        {
            if (planDesignitionType == PlanDesignitionType.Unknown || planDesignitionType == planType)
                requiresMaterialUpdate = true;
        }

        public void InvokeColorUpdate(PlanDesignitionType planDesignitionType)
        {
            if (planDesignitionType is PlanDesignitionType.Unknown || planDesignitionType == planType)
                requiresColorUpdate = true;
        }
    }
}
