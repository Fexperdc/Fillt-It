using Godot;

public class Game : Spatial {

	public Character character;
	private FillingVoxelArtSystem _fillingVoxelArtSystem;

	public override void _Ready() {
		character = GetNode<Character>("Character");
		_fillingVoxelArtSystem = (FillingVoxelArtSystem)FindNode("FillingVoxelArtSystem");

		GetNode<PlaceInContainerSystem>("Character/PlaceInContainerSystem").Connect(nameof(PlaceInContainerSystem.InteractedWithContainer), this, nameof(ContainerInteracted));
	}

	public override void _Input(InputEvent @event) {
		//if(@event.IsActionPressed("ui_up")) {
		//}

		//if (@event.IsActionPressed("ui_down")) {
		//}
	}

	public override void _PhysicsProcess(float delta) {
		GetNode<Sprite3D>("TestPoint").Translation = character.GetPickRayCast().GetCollisionPoint();
		
		if (Input.IsActionPressed("ui_left")) {
			character.MoveLeft();
		}
		if (Input.IsActionPressed("ui_right")) {
			character.MoveRight();
		}
		if (Input.IsActionPressed("ui_up")) {
			character.MoveUp();
		}
		if (Input.IsActionPressed("ui_down")) {
			character.MoveDown();
		}
	}

	private void ContainerInteracted(Spatial obj) {
		GD.Print(_fillingVoxelArtSystem.IsAllContainersSuccess());
	}
}
