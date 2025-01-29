using Godot;
using System;

public partial class Ui : CanvasLayer
{
	[Export] public Label CoinLabel;

	public int currentCoins = 0;

	public override void _Ready()
	{
		CoinLabel.Text = "X " + currentCoins.ToString();
	}

	public void AddCoins(int amount)
	{
		currentCoins += amount;
		CoinLabel.Text = "X " + currentCoins.ToString();
	}
}
