using System;
using Godot;
public partial class Enemy : CharacterBody2D
{
	[Export] private int _Speed = 2000;
	[Export] private int _Direction = 1;
	[Export] private Sprite2D _BodyEnemy;
	[Export] private AnimationPlayer _AnimationEnemy;
	[Export] private RayCast2D _RayCastEnemy;
	[Export] private bool _Special = false;
	[Export] private Color _Special_Color;
	[Export] public float JumpVelocity = -200.0f;

	public override void _Ready()
	{
		_AnimationEnemy.Play("Walk");
		if(_Special)
		{
			_BodyEnemy.Modulate = _Special_Color;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		if (Input.IsActionJustPressed("jump") && IsOnFloor() && _Special)
        {
            velocity.Y = JumpVelocity;
        }
		
		velocity.X = _Speed * _Direction * (float)delta;

		Velocity = velocity;
		_Set_Direction();
		MoveAndSlide();
	}
	public void _Set_Direction()
	{
		
		if(IsInstanceValid(_RayCastEnemy.GetCollider()))
		{
			if(_RayCastEnemy.IsColliding() && _RayCastEnemy.GetCollider() is Node collider && collider.IsInGroup("TileMap"))
			{
				_BodyEnemy.FlipH = !_BodyEnemy.FlipH;
				if(_Direction == 1)
				{
					_Direction = -1;
					_RayCastEnemy.TargetPosition = new Vector2(-10, 0);
				}
				else
				{
					_Direction = 1;
					_RayCastEnemy.TargetPosition = new Vector2(10, 0);
				}
			}
		}
	}
	public void _on_area_2d_kill_body_entered(Node body)
	{
		if (body is CharacterBody2D characterBody && characterBody.Name == "Player")
		{
			GetTree().ReloadCurrentScene();
		}
	}

	public void _on_area_2d_death_body_entered(Node body)
	{
		if (body is CharacterBody2D characterBody && characterBody.Name == "Player")
		{
			_AnimationEnemy.Play("Kill");
		}
	}
	public void _on_animation_player_animation_finished(string anim_name)
	{
		if (anim_name == "Kill")
		{
			QueueFree();
		}
	}
}
