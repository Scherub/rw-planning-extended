using PlanningExtended.Materials;
using PlanningExtended.Plans;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designations
{
    public class PlanDesignation : Designation
    {
        bool requiresColorUpdate;

        [Unsaved]
        Material _material;
        Material Material
        {
            get
            {
                if (_material == null)
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

        }

        public PlanDesignation(LocalTargetInfo target, DesignationDef def, ColorDef colorDef = null)
            : base(target, def, colorDef)
        {

        }

        public override void DesignationDraw()
        {
            if (!PlanManager.ArePlansVisible)
                return;

            Graphics.DrawMesh(MeshPool.plane10, DrawLoc(), Quaternion.identity, Material, 0);
        }

        public void InvokeColorUpdate()
        {
            requiresColorUpdate = true;
        }
    }
}
