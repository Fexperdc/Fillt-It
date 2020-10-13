using Godot;

[Tool]
public class ColoredContainer : Container {

	public const string COLORED_CONTAINER_GROUP = "colored_container";

	[Export]
	public int colorNumber;

	private Label _colorNumberLabel;

	public override void _Ready() {
		base._Ready();
		_colorNumberLabel = GetNode<Label>("Viewport/ColorNumberLabel");
	}

	public override void _PhysicsProcess(float delta) {
		if (Engine.EditorHint) {
			GetNode<Label>("Viewport/ColorNumberLabel").Text = "" + colorNumber;
		} else {
			_colorNumberLabel.Text = "" + colorNumber;
		}
	}

}
