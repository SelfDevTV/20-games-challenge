using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public bool IsFloating = false;
	public const float JumpVelocity = -250.0f;
	public const float rotationSpeed = 200f;

	[Export]
	public bool dead = false;

	public override void _PhysicsProcess(double delta)
	{
		if (dead)
		{

			Velocity = new();
			return;
		}
		Vector2 velocity = Velocity;

		if (!IsFloating)
			velocity += GetGravity() * (float)delta;


		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && !IsFloating)
		{
			velocity.Y = JumpVelocity;
		}

		// Rotate the bird.
		if (velocity.Y < 0)
		{
			float newRotation = Rotation - Mathf.DegToRad(rotationSpeed * (float)delta);
			Rotation = Mathf.Max(newRotation, Mathf.DegToRad(-45));
		}
		else
		{
			float newRotation = Rotation + Mathf.DegToRad(rotationSpeed * (float)delta);
			Rotation = Mathf.Min(newRotation, Mathf.DegToRad(45));
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
