using Godot;
using System;

public partial class World : Node2D
{
	[Signal] public delegate void GameOverEventHandler();
	[Signal] public delegate void ScoredEventHandler();

	[Export] public PackedScene PipePairScene;

	Timer PipeTimer;

	public override void _Ready()
	{
		Area2D ground = GetNode<Area2D>("%HitArea");
		ground.BodyEntered += OnGroundBodyEntered;

		PipeTimer = GetNode<Timer>("%PipeTimer");
		PipeTimer.Timeout += SpawnPipePair;
	}

	public void SpawnPipePair()
	{
		PipePair pipePair = PipePairScene.Instantiate<PipePair>();
		AddChild(pipePair);
		pipePair.Position = new Vector2(GetNode<Marker2D>("PipeSpawnPoint").Position.X, pipePair.Position.Y);

		pipePair.GameOver += () => EmitSignal(SignalName.GameOver);
		pipePair.Scored += () => EmitSignal(SignalName.Scored);
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
