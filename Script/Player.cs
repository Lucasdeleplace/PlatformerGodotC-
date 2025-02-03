using Godot;
public partial class Player : CharacterBody2D
{
    [Export] public float JumpVelocity = -300.0f;
    [Export] public AnimationPlayer AnimationPlayer;
    [Export] public Sprite2D Sprite;
    [Export] public PointLight2D Torche;
    [Export] public float Speed = 200.0f;
    [Export] public CpuParticles2D JumpParticles;
    [Export] public CpuParticles2D WalkParticles;
    [Export] public float DashSpeed = 600.0f;
    [Export] public float DashDuration = 0.2f;
    [Export] public int MaxJumps = 1;

    private bool _isDashing = false;
    private float _dashTime = 0.0f;
    private int _jumpsLeft;

    public override void _Ready()
    {
        _jumpsLeft = MaxJumps;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        if (_isDashing)
        {
            _dashTime -= (float)delta;
            if (_dashTime <= 0)
            {
                _isDashing = false;
            }
        }
        else
        {
            // Add the gravity.
            if (!IsOnFloor())
            {
                velocity += GetGravity() * (float)delta;
            }

            // Handle Jump.
            if (Input.IsActionJustPressed("jump") && _jumpsLeft > 0)
            {
                velocity.Y = JumpVelocity;
                JumpParticles.Emitting = true;
                _jumpsLeft--;
            }

            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * Speed;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            }

            // Handle Dash.
            if (Input.IsActionJustPressed("dash") && _dashCooldown <= 0)
            {
                StartDash(direction);
            }
        }

        if (IsOnFloor())
        {
            _jumpsLeft = MaxJumps;
        }

        if (_isDashing)
        {
            velocity = _dashDirection * DashSpeed;
            _dashTime -= (float)delta;
            if (_dashTime <= 0)
            {
                _isDashing = false;
                _dashCooldown = DashCooldownDuration; // Start cooldown
            }
        }

        // Update cooldown timer
        if (_dashCooldown > 0)
        {
            _dashCooldown -= (float)delta;
        }

        Velocity = velocity;
        UpdateAnimation(direction);
        MoveAndSlide();
    }

    private Vector2 _dashDirection;
    private float _dashCooldown = 0.0f;
    [Export] public float DashCooldownDuration = 0.1f; // Cooldown duration in seconds


    private void StartDash(Vector2 direction)
    {
        if (direction == Vector2.Zero)
        {
            return;
        }

        _isDashing = true;
        _dashTime = DashDuration;

        if (IsOnFloor())
        {
            direction.Y = 0;
        }

        _dashDirection = direction.Normalized();
    }

    public void UpdateAnimation(Vector2 dir)
    {
        if (dir.X == 1)
        {
            WalkParticles.Emitting = true;
            Sprite.FlipH = false;
            AnimationPlayer.Play("Walk");
        }
        else if (dir.X == -1)
        {
            WalkParticles.Emitting = true;
            Sprite.FlipH = true;
            AnimationPlayer.Play("Walk");
        }
        else
        {
            WalkParticles.Emitting = false;
            AnimationPlayer.Play("Idle");
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("torche"))
        {
            Torche.Visible = !Torche.Visible;
        }
    }
}