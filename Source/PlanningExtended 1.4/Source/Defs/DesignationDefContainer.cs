using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Defs
{
    public class DesignationDefContainer
    {
        public PlanDesignitionType Type { get; }

        public string TextureName { get; }

        public string Label { get; }

        public DesignationDef Default { get; }

        public DesignationDef Colored { get; }

        public IEnumerable<DesignationDef> DesignationDefs
        {
            get
            {
                yield return Default;
                yield return Colored;
            }
        }

        // TODO: once PlanDesignationType.PlanWall(s) is fixed, remove separate textureName
        public DesignationDefContainer(PlanDesignitionType type, string textureName, string label, DesignationDef @default, DesignationDef colored)
        {
            Type = type;
            TextureName = textureName;
            Label = $"PlanningExtended.{label}";
            Default = @default;
            Colored = colored;
        }
    }
}
