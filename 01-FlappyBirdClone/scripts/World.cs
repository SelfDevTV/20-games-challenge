using Godot;
using System;

public partial class World : Node2D
{
	[Signal] public delegate void GameOverEventHandler();

	public override void _Ready()
	{
		Area2D ground = GetNode<Area2D>("%HitArea");
		ground.BodyEntered += OnGroundBodyEntered;
	}

	public void OnGroundBodyEntered(Node2D body)
	{
		if (body is Bird)
		{
			EmitSignal(SignalName.GameOver);
			GD.Print("Game Over");
		}
	}
}
