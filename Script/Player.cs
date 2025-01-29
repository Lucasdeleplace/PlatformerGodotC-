using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float JumpVelocity = -300.0f;
    [Export] public AnimationPlayer AnimationPlayer;
    [Export] public Sprite2D Sprite;
	[Export] public PointLight2D Torche;
    [Export] public float Speed = 200.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        UpdateAnimation(direction);
        MoveAndSlide();
    }

    public void UpdateAnimation(Vector2 dir)
    {
        if (dir.X == 1)
        {
            Sprite.FlipH = false;
			AnimationPlayer.Play("Walk");
		} 
		else if (dir.X == -1)
		{
            Sprite.FlipH = true;
			AnimationPlayer.Play("Walk");
        } 
        else
        {
            AnimationPlayer.Play("Idle");
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed("torche"))
		{
			Torche.Visible = !Torche.Visible;
		}
    }
}