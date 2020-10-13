using Godot;

public class PlaceInContainerSystem : Node {

    [Signal]
    public delegate void InteractedWithContainer(PhysicsBody obj);

    [Export]
    public NodePath characterPickingSystemPath;
    private CharacterPickingSystem _characterPickingSystem;

    public override void _Ready() {
        _characterPickingSystem = GetNodeOrNull<CharacterPickingSystem>(characterPickingSystemPath);
    }

    public override void _Input(InputEvent @event) {
        if(@event.IsActionPressed("interact")) {
            InteractWithContainer();
        }
    }

    public void InteractWithContainer() {
        Container container = GetContainer();

        if (container != null) {
            if (!container.HasObject() && _characterPickingSystem.IsPicked()) {
                container.PlaceObject(_characterPickingSystem.GetPickedObject());

                if(FillingVoxelArtSystem.IsContainerSuccess(container))
                    ((PickableVoxel)container.GetObject()).colorNumberVisible = false;

                EmitSignal(nameof(InteractedWithContainer), container.GetObject());
            } else {
                if (container.HasObject()) {
                    EmitSignal(nameof(InteractedWithContainer), container.GetObject());

                    ((PickableVoxel)container.GetObject()).colorNumberVisible = true;
                    container.UnplaceObject();

                }
            }
        }
    }

    public Container GetContainer() {
        Container container = null;

        if(_characterPickingSystem.GetCharacter().GetFloorRayCast().GetCollider() is Container) {
            container = (Container)_characterPickingSystem.GetCharacter().GetFloorRayCast().GetCollider();
        }

        return container;
    }
}