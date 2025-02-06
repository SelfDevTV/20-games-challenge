using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public bool IsFloating = false;
	public const float JumpVelocity = -250.0f;
	public const float rotationSpeed = 200f;

	public GpuParticles2D particles;

	[Export]
	public bool dead = false;

	AudioStreamPlayer2D FlapSound;

	public override void _Ready()
	{
		FlapSound = GetNode<AudioStreamPlayer2D>("FlapSound");
		particles = GetNode<GpuParticles2D>("FlapParticles");

		AnimatedSprite2D sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.AnimationChanged += () => particles.Emitting = true;
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector2 velocity = Velocity;

		if (!IsFloating)
			velocity += GetGravity() * (float)delta;


		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && !IsFloating && !dead)
		{
			velocity.Y = JumpVelocity;
			FlapSound.Play();
			particles.Restart();
		}

		// Rotate the bird.
		if (velocity.Y < 0 && !IsFloating)
		{
			float newRotation = Rotation - Mathf.DegToRad(rotationSpeed * (float)delta);
			Rotation = Mathf.Max(newRotation, Mathf.DegToRad(-45));
		}
		else if (velocity.Y > 0 && !IsFloating)
		{
			float newRotation = Rotation + Mathf.DegToRad(rotationSpeed * (float)delta);
			Rotation = Mathf.Min(newRotation, Mathf.DegToRad(45));
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
