using Godot;

public class CharacterPickingSystem : Node {

	[Signal]
	public delegate void Picked(Spatial obj);
	[Signal]
	public delegate void Unpicked();

	[Export]
    public NodePath characterPath;
    private Character _character;
	private PhysicsBody _pickedObject;

    public override void _Ready() {
        _character = GetNodeOrNull<Character>(characterPath);
    }

    public override void _PhysicsProcess(float delta) {
        if (IsPicked()) {
            _pickedObject.Translation = _character.GetArm().GlobalTransform.origin;
        }
    }

    public override void _Input(InputEvent @event) {
		if (@event.IsActionPressed("interact")) {
			InteractWithObject();
		}
	}

	public void InteractWithObject() {
		if (IsPicked()) {
			Unpick();

			return;
		}
		Object collider = GetFocusedObject();
		if (collider != null && !IsPicked()) {
			if (collider is PickableVoxel) {
				Voxel voxel = (Voxel)collider;

				if (!IsPicked()) {
					Pick(voxel);
				}
			}
		}
	}

	public Character GetCharacter() {
		return _character;
    }

	public Spatial GetFocusedObject() {
		return (Spatial)_character.GetPickRayCast().GetCollider();
    }

	public void Pick(Node node) {

		_pickedObject = (PhysicsBody) node;

		if(GetPickedObject() is RigidBody) {
			((RigidBody)GetPickedObject()).Mode = RigidBody.ModeEnum.Kinematic;
        }

		EmitSignal(nameof(Picked), _pickedObject);
	}

	public void Unpick() {
		if (GetPickedObject() is RigidBody) {
			((RigidBody)GetPickedObject()).Mode = RigidBody.ModeEnum.Rigid;
			((RigidBody)GetPickedObject()).Sleeping = false;
		}

		_pickedObject = null;

		EmitSignal(nameof(Unpicked));
	}

	public bool IsPicked() {
		return GetPickedObject() != null;
	}

	public PhysicsBody GetPickedObject() {
		return _pickedObject;
    }
}