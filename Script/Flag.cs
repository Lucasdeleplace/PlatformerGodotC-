using Godot;
using System;

public partial class Flag : Area2D
{
	[Export] private string _NextScene;
	[Export] private bool _HideSprite;
	[Export] private int _AmountCoinsNeeded = 0;
	[Export] private AnimatedSprite2D _AnimatedSprite;
	[Export] private Ui ui; 

	public override void _Ready()
	{
		if(!_HideSprite)
		{
			_AnimatedSprite.Play("Idle");
			_AnimatedSprite.Visible = true;
		}
	}

	public void _on_body_entered(CharacterBody2D body) {
    if(body.Name == "Player" && ui.currentCoins >= _AmountCoinsNeeded) {
        GetTree().CallDeferred("change_scene_to_file", _NextScene);
    }
}
}
