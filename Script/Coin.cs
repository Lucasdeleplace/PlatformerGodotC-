using Godot;
using System;

public partial class Coin : Area2D
{

	[Export] private AnimationPlayer AnimationCoin;
	[Export] private Ui ui; 
	public override void _Ready()
	{
		AnimationCoin.Play("Coin");
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			ui.AddCoins(1);	
			QueueFree();
		}
	}
	
}
