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

    public void _on_body_entered(Node body) {
    if (body is CharacterBody2D characterBody && characterBody.Name == "Player" && ui.currentCoins >= _AmountCoinsNeeded) {
        GD.Print(GetTree().CurrentScene.SceneFilePath + " " +  _NextScene);
        if (GetTree().CurrentScene.SceneFilePath != "res://Scenes/Hub.tscn") {
            if(_NextScene == GetTree().CurrentScene.SceneFilePath)
            {
                GetTree().CallDeferred("change_scene_to_file", _NextScene);
                return;
            }
            ui.StopTimer();
            GetTree().CallDeferred("change_scene_to_file", _NextScene);
        } else {
            GetTree().CallDeferred("change_scene_to_file", _NextScene);
        }
    }
}
}