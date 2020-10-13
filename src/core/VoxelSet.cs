using Godot;
using Godot.Collections;

public class VoxelSet : Resource {

    private Dictionary<int, Color> voxels = new Dictionary<int, Color>();

    public Color GetColor(int colorNumber) {
        return voxels[colorNumber];
    }

}