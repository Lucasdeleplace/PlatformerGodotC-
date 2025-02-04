using Godot;
using System;

public partial class Gatling : Node2D
{
    [Export] public PackedScene BulletScene;
    [Export] public float FireRate = 0.1f; // Temps entre chaque tir
	[Export] public Sprite2D Sprite;
    [Export] public AnimationPlayer AnimationGun;
    [Export] public AudioStreamPlayer ShootSound;
    private bool _canShoot = true;
    private Timer _fireTimer;

    public override void _Ready()
    {
        _fireTimer = new Timer();
        _fireTimer.WaitTime = FireRate;
        _fireTimer.OneShot = true;
        AddChild(_fireTimer);
        _fireTimer.Timeout += () => _canShoot = true;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("shoot") && _canShoot)
        {
            
            Shoot();
            AnimationGun.Play("Shoot");
            _canShoot = false;
            _fireTimer.Start();
        }
		if (Input.IsActionJustPressed("ui_left")) {
			Sprite.FlipH = true;
		} else if (Input.IsActionJustPressed("ui_right")) {
			Sprite.FlipH = false;
		}
    }

   private void Shoot()
{

    if (BulletScene == null)
    {
        GD.PrintErr("BulletScene non assignée !");
        return;
    }
        ShootSound.Play();
    var bullet = (Bullet)BulletScene.Instantiate();
    bullet.Position = GetNode<Marker2D>("Marker2D").GlobalPosition;

    // Ajuste la direction de la balle selon l'orientation de la Gatling
    bullet.Direction = Sprite.FlipH ? Vector2.Left : Vector2.Right;

    GetTree().CurrentScene.AddChild(bullet);
}
}
