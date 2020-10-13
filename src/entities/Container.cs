using Godot;

public class Container : RigidBody {

	public const string CONTAINER_GROUP = "container";

	[Signal]
	public delegate void ObjectPlaced(PhysicsBody obj);
	[Signal]
	public delegate void ObjectUnplaced();

	private PhysicsBody _object;
	private PinJoint _pinJoint;
	private MeshInstance _meshInstance;

	public override void _Ready() {
		_meshInstance = GetNode<MeshInstance>("MeshInstance");
	}

	public void PlaceObject(PhysicsBody body) {
		if (GetObject() == null) {
			_object = body;
			_object.Translation = Translation;

			_pinJoint = new PinJoint();
			_pinJoint.Nodes__nodeA = GetPath();
			_pinJoint.Nodes__nodeB = body.GetPath();
			AddChild(_pinJoint);

			EmitSignal(nameof(ObjectPlaced), _object);
		}
	}

	public void UnplaceObject() {
		if(GetObject() != null) {
			_object = null;

			_pinJoint.QueueFree();
			_pinJoint = null;

			EmitSignal(nameof(ObjectUnplaced));
		}
	}

	public Spatial GetObject() {
		return _object;
	}

	public bool HasObject() {
		return GetObject() != null;
	}

	public MeshInstance GetMeshInstance() {
		return _meshInstance;
	}
}
