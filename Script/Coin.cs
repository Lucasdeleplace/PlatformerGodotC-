using Godot;

public partial class Coin : Area2D
{
    [Export] private AnimationPlayer AnimationCoin;
    [Export] private Ui ui; 
    [Export] public AudioStreamPlayer CoinSound;

    public override void _Ready()
    {  
            AnimationCoin.Play("Coin");

    }

    private void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player")
        {
            ui.AddCoins(1);
            CoinSound.Play();
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        }
    }

    public void _on_audio_stream_player_finished()
    {
        QueueFree();
    }
}