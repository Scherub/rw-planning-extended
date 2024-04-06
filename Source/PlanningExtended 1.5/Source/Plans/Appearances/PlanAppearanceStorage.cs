using System;
using System.Collections.Generic;
using PlanningExtended.Settings;

namespace PlanningExtended.Plans.Appearances
{
    internal class PlanAppearanceStorage
    {
        readonly Dictionary<PlanDesignationType, PlanAppearance> _planAppearances = [];

        readonly PlanAppearance _planDoor;

        readonly PlanAppearance _planFloor;

        readonly PlanAppearance _planObject;

        readonly PlanAppearance _planWall;

        PlanningSettings Settings => PlanningMod.Settings;

        public PlanAppearanceStorage()
        {
            _planDoor = CreatePlanAppearance(PlanDesignationType.PlanDoors);
            _planFloor = CreatePlanAppearance(PlanDesignationType.PlanFloors);
            _planObject = CreatePlanAppearance(PlanDesignationType.PlanObjects);
            _planWall = CreatePlanAppearance(PlanDesignationType.PlanWall);
        }

        public IEnumerable<PlanAppearance> PlanAppearances => _planAppearances.Values;

        public void ModifyPlanAppearance(PlanDesignationType planDesignationType, Action<PlanAppearance> modifyPlanAppearance)
        {
            foreach (var planAppearance in _planAppearances)
                if (planAppearance.Key == planDesignationType || planDesignationType == PlanDesignationType.Unknown)
                    modifyPlanAppearance(planAppearance.Value);
        }

        public IEnumerable<PlanAppearance> GetPlanAppearances(PlanDesignationType planDesignationType)
        {
            foreach (var planAppearance in _planAppearances)
                if (planAppearance.Key == planDesignationType || planDesignationType == PlanDesignationType.Unknown)
                    yield return planAppearance.Value;
        }

        public PlanAppearance GetPlanAppearance(PlanDesignationType planDesignationType)
        {
            return planDesignationType switch
            {
                PlanDesignationType.PlanDoors => _planDoor,
                PlanDesignationType.PlanFloors => _planFloor,
                PlanDesignationType.PlanObjects => _planObject,
                PlanDesignationType.PlanWall => _planWall,
                _ => null
            };
        }

        PlanAppearance CreatePlanAppearance(PlanDesignationType planDesignationType)
        {
            PlanAppearance planAppearance = new(planDesignationType,
                Settings.Plan.GetPlanColor(planDesignationType),
                Settings.Plan.GetPlanOpacity(planDesignationType),
                Settings.Plan.GetPlanTextureSet(planDesignationType),
                Settings.Plan.GetPlanVisibility(planDesignationType)
                );

            _planAppearances[planDesignationType] = planAppearance;
            
            return planAppearance;
        }
    }
}
