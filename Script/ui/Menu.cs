using Godot;

public partial class Menu : Control
{
	[Export] private Marker2D _Marker;
	[Export] private CharacterBody2D _Character;
	[Export] private AudioStreamPlayer _AudioStartGame;
	[Export] private Color opacity = new Color(1, 1, 1, 0);
	public void _on_button_play_pressed()
	{
		_AudioStartGame.Play();
		GetTree().ChangeSceneToFile("res://Scenes/Hub.tscn");
	}
	public void _on_button_option_pressed()
	{
		GD.Print("wip");
	}
	public void _on_button_quit_pressed()
	{
		GetTree().Quit();
	}
    public override void _PhysicsProcess(double delta)
    {
        float dist_to = _Character.GlobalPosition.DistanceTo(_Marker.GlobalPosition);
		if(dist_to < 362) {
			Modulate = Modulate.Lerp(opacity, 0.1f);
		} else {
			Modulate = Modulate.Lerp(new Color(1, 1, 1, 1), 0.1f);
			
		}
    }
}
