using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export] public float Speed = 800f;
    public Vector2 Direction = Vector2.Right;
	[Export] private RayCast2D _RayCastLeft;
	[Export] private RayCast2D _RayCastRight;

    public override void _Process(double delta)
    {
        Position += Direction * Speed * (float)delta;
    }

	public override void _PhysicsProcess(double delta) {
		 OnCollider();
	}

    private void OnAreaEntered(Area2D area)
    {
        if (area.GetParent() is Enemy enemy)  // Vérifie si le parent de l'objet touché est un ennemi
        {
            enemy.Die();
			QueueFree(); 
        } 
    }

	private void OnCollider(){
		if(IsInstanceValid(_RayCastLeft.GetCollider()))
		{
            if(_RayCastLeft.GetCollider() is Node colliderLeft && colliderLeft.IsInGroup("TileMap"))
			{
				// QueueFree();
				CallDeferred("queue_free");
			}
		}
			if(IsInstanceValid(_RayCastRight.GetCollider()))
		{
			if(_RayCastRight.GetCollider() is Node colliderRight && colliderRight.IsInGroup("TileMap"))
			{
				// QueueFree();
				CallDeferred("queue_free");
			}
		}
	}
}
