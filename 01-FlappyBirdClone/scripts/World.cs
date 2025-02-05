using Godot;
using System;

public partial class World : Node2D
{
	[Signal] public delegate void GameOverEventHandler();
	[Signal] public delegate void ScoredEventHandler();

	[Export] public PackedScene PipePairScene;

	Godot.Collections.Array<Node> ParallaxLayers = new Godot.Collections.Array<Node>();

	Timer PipeTimer;

	private Vector2 _markedScroll = Vector2.Zero;

	public override void _Ready()
	{
		Area2D ground = GetNode<Area2D>("%HitArea");
		ground.BodyEntered += OnGroundBodyEntered;
		ParallaxLayers = GetChildren();

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
		pipePair.Position = new Vector2(GetNode<Marker2D>("PipeSpawnPoint").Position.X, pipePair.Position.Y);

		pipePair.GameOver += () => EmitSignal(SignalName.GameOver);
		pipePair.Scored += () => EmitSignal(SignalName.Scored);
	}

	public void OnGroundBodyEntered(Node2D body)
	{
		if (body is Bird)
		{
			Bird b = body as Bird;
			b.dead = true;

			foreach (Node layer in ParallaxLayers)
			{
				if (layer is Parallax2D)
				{

					Parallax2D p = layer as Parallax2D;

					p.Autoscroll = Vector2.Zero;

				}
			}
			EndGame();


		}
	}

	async void EndGame()
	{

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		EmitSignal(SignalName.GameOver);
		GD.Print("Game Over");

	}
}
