using PlanningExtended.Plans;

namespace PlanningExtended.UndoRedo
{
    public class UndoRedoSnapshot
    {
        public PlanLayout UndoPlanLayout { get; }

        public PlanLayout RedoPlanLayout { get; }

        public UndoRedoSnapshot(PlanLayout undoPlanLayout, PlanLayout redoPlanLayout)
        {
            UndoPlanLayout = undoPlanLayout;
            RedoPlanLayout = redoPlanLayout;
        }
    }
}
