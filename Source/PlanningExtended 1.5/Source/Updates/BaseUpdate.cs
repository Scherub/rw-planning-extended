using System;
using Verse;

namespace PlanningExtended.Updates
{
    internal abstract class BaseUpdate
    {
        public abstract int Version { get; }

        public bool Apply(Map map)
        {
            bool result = false;

            try
            {
                OnUpdate(map);

                result = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

            return result;
        }

        protected abstract void OnUpdate(Map map);
    }
}
