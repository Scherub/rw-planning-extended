using System;
using Verse;

namespace PlanningExtended.Plans
{
    public class PlanInfo : IExposable
    {
        string name;
        public string Name => name;

        public string fileName;

        public DateTime created;
        public DateTime Created => created;

        PlanLayout planLayout;
        public PlanLayout PlanLayout => planLayout;

        public PlanInfo()
        {
        }

        public PlanInfo(string name, DateTime created, PlanLayout planLayout)
        {
            this.name = name;
            this.created = created;
            this.planLayout = planLayout;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref name, nameof(Name));
            ExposeDateTime(ref created, nameof(Created));
            Scribe_Deep.Look(ref planLayout, nameof(PlanLayout));
        }

        void ExposeDateTime(ref DateTime dateTime, string label)
        {
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                long ticks = dateTime.Ticks;

                Scribe_Values.Look(ref ticks, label);

                created = new(ticks);
            }
            else if (Scribe.mode == LoadSaveMode.Saving)
            {
                long ticks = created.Ticks;

                Scribe_Values.Look(ref ticks, label);
            }
        }
    }
}
