using Godot;

public partial class Spick : Area2D
{
	public void _on_body_entered(Node body){
		if (body is CharacterBody2D characterBody && characterBody.Name == "Player")
		{
			GetTree().CallDeferred("reload_current_scene");
		}
	}
}
