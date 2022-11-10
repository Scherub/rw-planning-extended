using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Defs
{
    public class DesignationDefContainer
    {
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

        public DesignationDefContainer(string label, DesignationDef @default, DesignationDef colored)
        {
            Label = $"PlanningExtended.{label}";
            Default = @default;
            Colored = colored;
        }
    }
}
