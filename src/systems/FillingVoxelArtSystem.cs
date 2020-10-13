using Godot;
using Godot.Collections;

public class FillingVoxelArtSystem : Node {

    public bool IsAllContainersSuccess() {
        bool result = false;

        Array containers = GetTree().GetNodesInGroup(ColoredContainer.COLORED_CONTAINER_GROUP);
        
        foreach(ColoredContainer container in containers) {
            if(IsContainerSuccess(container)) {
                result = true;
            } else {
                result = false;
                break;
            }
        }

        return result;
    }

    public static bool IsContainerSuccess(Container container) {
        bool result = false;

        if(container is ColoredContainer && container.HasObject()) {
            ColoredContainer coloredContainer = (ColoredContainer) container;

            Spatial obj = container.GetObject();
            if(obj is PickableVoxel) {
                PickableVoxel voxel = (PickableVoxel)obj;

                if(voxel.colorNumber == coloredContainer.colorNumber) {
                    result = true;
                }
            }
        }

        return result;
    }

}