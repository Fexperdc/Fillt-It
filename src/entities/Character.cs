using Godot;

public class Character : KinematicBody {

	[Export]
	public Vector3 gravity;

	[Export]
	public Vector3 moveForce;
	[Export]
	public float minVelocity;
	[Export]
	public float maxVelocity;
	[Export]
	public float lerpWeight;

	private Vector3 _velocity;

	private RayCast _pickRayCast;
	private RayCast _floorRayCast;
	private MeshInstance _meshInstance;
	private Spatial _arm;

	public override void _Ready() {
		_pickRayCast = GetNode<RayCast>("PickRayCast");
		_floorRayCast = GetNode<RayCast>("FloorRayCast");
		_meshInstance = GetNode<MeshInstance>("MeshInstance");
		_arm = GetNodeOrNull<Spatial>("Arm");
	}

	public override void _PhysicsProcess(float delta) {
		if (_velocity != Vector3.Zero) {
			LookAt(new Vector3(-_velocity.x * 10000, Translation.y, -_velocity.z * 10000), Vector3.Up);
		}


		_velocity += gravity;

		_velocity.x = Mathf.Clamp(_velocity.x, minVelocity, maxVelocity);
		_velocity.z = Mathf.Clamp(_velocity.z, minVelocity, maxVelocity);

		_velocity.x = Mathf.Lerp(_velocity.x, 0.0001f, lerpWeight);
		_velocity.z = Mathf.Lerp(_velocity.z, 0.0001f, lerpWeight);

		_velocity = MoveAndSlide(_velocity, infiniteInertia: false);
	}

	public void MoveLeft() {
		_velocity.x -= moveForce.x;
	}

	public void MoveRight() {
		_velocity.x += moveForce.x;
	}

	public void MoveUp() {
		_velocity.z -= moveForce.x;
	}

	public void MoveDown() {
		_velocity.z += moveForce.x;
	}

	public RayCast GetPickRayCast() {
		return _pickRayCast;
	}

	public RayCast GetFloorRayCast() {
		return _floorRayCast;
	}

	public Spatial GetArm() {
		return _arm;
	}

}
