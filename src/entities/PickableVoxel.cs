using Godot;

[Tool]
public class PickableVoxel : Voxel {

	[Export]
	public bool colorNumberVisible;
	[Export]
	public int colorNumber;

	private Label _colorNumberLabel;

	public override void _EnterTree() {
		base._EnterTree();
		_colorNumberLabel = GetNode<Label>("Viewport/ColorNumberLabel");
	}

	public override void _Ready() {
		base._Ready();
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		_colorNumberLabel.Text = "" + colorNumber;

		if (colorNumberVisible) {
			_colorNumberLabel.Visible = colorNumberVisible;
		} else {
			_colorNumberLabel.Visible = colorNumberVisible;
		}

		if (Engine.EditorHint) {
		} else {
		}
	}

}
