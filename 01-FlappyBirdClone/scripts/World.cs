using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node2D
{
	[Signal] public delegate void GameOverEventHandler();
	[Signal] public delegate void ScoredEventHandler();

	[Export] public PackedScene PipePairScene;

	Godot.Collections.Array<Node> ParallaxLayers = new Godot.Collections.Array<Node>();

	Timer PipeTimer;

	AudioStreamPlayer2D GameOverSound;

	private Vector2 _markedScroll = Vector2.Zero;

	private List<PipePair> _pipePairs = new List<PipePair>();

	public override void _Ready()
	{
		Area2D ground = GetNode<Area2D>("%HitArea");
		ground.BodyEntered += OnGroundBodyEntered;
		ParallaxLayers = GetChildren();

		GameOverSound = GetNode<AudioStreamPlayer2D>("GameOverSound");

		PipeTimer = GetNode<Timer>("%PipeTimer");
		PipeTimer.Timeout += SpawnPipePair;
	}

	public void SpawnPipePair()
	{
		if (PipePairScene == null)
		{
			return;
		}
		PipePair pipePair = PipePairScene.Instantiate<PipePair>();
		AddChild(pipePair);
		_pipePairs.Add(pipePair);
		pipePair.Position = new Vector2(GetNode<Marker2D>("PipeSpawnPoint").Position.X, pipePair.Position.Y);

		pipePair.GameOver += EndGame;
		pipePair.Scored += () => EmitSignal(SignalName.Scored);
		pipePair.RemovePipePair += () =>
		{
			_pipePairs.Remove(pipePair);
			pipePair.QueueFree();
		};
	}

	public void OnGroundBodyEntered(Node2D body)
	{
		EndGame();
	}

	async void EndGame()
	{
		Bird bird = GetTree().GetFirstNodeInGroup("bird") as Bird;

		if (bird.dead)
		{
			return;
		}
		foreach (Node layer in ParallaxLayers)
		{
			if (layer is Parallax2D)
			{

				Parallax2D p = layer as Parallax2D;
				Vector2 pos = p.Position;
				p.Autoscroll = Vector2.Zero;
				p.Position = pos;

			}

		}
		PipeTimer.Stop();
		foreach (PipePair pipePair in _pipePairs)
		{
			pipePair.Stop();
		}

		bird.dead = true;
		GameOverSound.Play();
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		EmitSignal(SignalName.GameOver);
		GD.Print("Game Over");

	}
}
