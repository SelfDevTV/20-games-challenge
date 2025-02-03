using Godot;
using System;

public partial class PipePair : Node2D
{
	
	const float PIPE_GAP_MAX = 80;
	const float PIPE_GAP_MIN = 60;
	const float SCREEN_HEIGHT = 512;

	[Signal] public delegate void GameOverEventHandler();
	[Signal] public delegate void ScoredEventHandler();



    // TODO: Randomize the gap between the pipes
    // TODO: Randomize the height of the gap

    public override void _Ready()
    {
        RandomizePipeGap();
		GetNode<Area2D>("Top").BodyEntered += OnPipePairBodyEntered;
		GetNode<Area2D>("Bot").BodyEntered += OnPipePairBodyEntered;
		GetNode<Area2D>("Gap").BodyEntered += OnGapBodyEntered;

		GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += () => {
			QueueFree();
			GD.Print("Freed pipe");
		};


    }

	public void OnGapBodyEntered(Node body)
	{
		if (body is Bird)
		{
			EmitSignal(SignalName.Scored);
		}
	}

    public override void _Process(double delta)
    {
        Position += Vector2.Left * 100 * (float)delta;
    }

	public void OnPipePairBodyEntered(Node body)
	{
		if (body is Bird)
		{
			EmitSignal(SignalName.GameOver);
		}
	}

    public void RandomizePipeGap()
	{
		float halfScreen = SCREEN_HEIGHT / 2;
		float randY = (float)GD.RandRange(halfScreen - PIPE_GAP_MAX, halfScreen + PIPE_GAP_MAX);
		Position = new Vector2(Position.X, randY);

		float randGap = (float)GD.RandRange(PIPE_GAP_MIN, PIPE_GAP_MAX);
		GetNode<Area2D>("Top").Position += new Vector2(0, -randGap);


	}

	


}
