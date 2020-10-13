using Godot;

[Tool]
public class Voxel : RigidBody {

	[Export]
	private Color _color;
	private MeshInstance _meshInstance;

	public override void _EnterTree() {
		_meshInstance = GetNode<MeshInstance>("MeshInstance");
	}

	public override void _PhysicsProcess(float delta) {
		((SpatialMaterial)_meshInstance.MaterialOverride).AlbedoColor = _color;
	}

}
