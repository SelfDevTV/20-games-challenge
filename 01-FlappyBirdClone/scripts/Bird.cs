using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public bool IsFloating = false;
	public const float JumpVelocity = -250.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if(!IsFloating)
			velocity += GetGravity() * (float)delta;


		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && !IsFloating)
		{
			velocity.Y = JumpVelocity;
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
