using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions;

public abstract class BaseShapeDimensionsModifier
{
    bool _requiresUpdate;

    AreaDimensions _lastAreaDimensions;
    
    AreaDimensions _lastModfifiedAreaDimensions;

    public AreaDimensions Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation)
    {
        if (!_requiresUpdate && areaDimensions == _lastAreaDimensions)
            return _lastModfifiedAreaDimensions;

        var modifiedAreaDimensions = OnUpdate(shape, areaDimensions, mousePosition, rotation);

        _lastAreaDimensions = areaDimensions;
        _lastModfifiedAreaDimensions = modifiedAreaDimensions;
        _requiresUpdate = false;

        return modifiedAreaDimensions;
    }

    public void SetRequiresUpdate()
    {
        _requiresUpdate = true;
    }

    public abstract AreaDimensions OnUpdate(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation);
}
