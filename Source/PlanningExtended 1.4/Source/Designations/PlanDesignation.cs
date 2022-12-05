using PlanningExtended.Defs;
using PlanningExtended.Plans;
using PlanningExtended.Plans.Appearances;
using RimWorld;
using UnityEngine;
using Verse;

namespace PlanningExtended.Designations
{
    public class PlanDesignation : Designation
    {
        PlanDesignationType _planType;
        public PlanDesignationType PlanType => _planType;

        PlanDesignationUpdateType _planDesignationUpdateType;

        Quaternion _rotationTransform;

        RotationDirection _rotation;
        public RotationDirection Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _rotationTransform = GetRotation();
            }
        }

        Color Color => colorDef.color.ToTransparent(PlanAppearanceManager.GetPlanOpacity(_planType));

        Material _material;
        Material Material
        {
            get
            {
                if (_material is null || _planDesignationUpdateType is PlanDesignationUpdateType.Material)
                {
                    if (_planType is PlanDesignationType.Unknown)
                        _planType = DesignationDefUtilities.GetType(def);

                    if (colorDef != null)
                        _material = new(def.iconMat) { color = Color };
                    else
                        _material = def.iconMat;

                    _planDesignationUpdateType = PlanDesignationUpdateType.None;
                }
                else if (_planDesignationUpdateType is PlanDesignationUpdateType.Color)
                {
                    if (colorDef != null)
                        _material = new(def.iconMat) { color = Color };

                    _planDesignationUpdateType = PlanDesignationUpdateType.None;
                }
                else if (_planDesignationUpdateType is PlanDesignationUpdateType.Opacity)
                {
                    _material.color = Color;
                    _planDesignationUpdateType = PlanDesignationUpdateType.None;
                }

                return _material;
            }
        }

        public bool IsDoor => _planType == PlanDesignationType.PlanDoors;

        public bool IsWall => _planType == PlanDesignationType.PlanWall;

        public bool IsDoorOrWall => IsDoor | IsWall;

        public PlanDesignation()
            : base()
        {
        }

        public PlanDesignation(PlanDesignationType planType, LocalTargetInfo target, DesignationDef def, ColorDef colorDef, RotationDirection rotation)
            : base(target, def, colorDef)
        {
            _planType = planType;
            _rotation = rotation;
            _rotationTransform = GetRotation();
        }

        public override void DesignationDraw()
        {
            if (!PlanManager.IsPlanVisible(_planType))
                return;

            Graphics.DrawMesh(MeshPool.plane10, DrawLoc(), _rotationTransform, Material, 0);
        }

        internal void InvokeUpdate(PlanDesignationType planDesignationType, PlanDesignationUpdateType planDesignationUpdateType)
        {
            if (planDesignationType == PlanDesignationType.Unknown || planDesignationType == _planType)
                _planDesignationUpdateType = planDesignationUpdateType;
        }

        Quaternion GetRotation()
        {
            return _rotation switch
            {
                RotationDirection.Clockwise => Quaternion.LookRotation(Vector3.right),
                RotationDirection.Counterclockwise => Quaternion.LookRotation(Vector3.left),
                RotationDirection.Opposite => Quaternion.LookRotation(Vector3.back),
                _ => Quaternion.identity
            };
        }
    }
}
