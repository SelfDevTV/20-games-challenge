using Godot;
using System;

public partial class TransitionManager : Node
{

	private static TransitionManager _instance;
	public static TransitionManager Instance => _instance;
	PackedScene transitionScene;

	public bool isTransitioning = false;

	[Signal] public delegate void TransitionCompleteEventHandler();

	public override void _EnterTree()
	{
		ProcessMode = ProcessModeEnum.Always;
		if (_instance != null)
		{
			this.QueueFree();
		}
		_instance = this;
	}

	public override void _Ready()
	{
		transitionScene = GD.Load<PackedScene>("res://scenes/transition_scene.tscn");
	}
	public async void LoadScene(string scenePath)
	{
		isTransitioning = true;
		GetTree().Paused = true;
		TransitionScene scene = transitionScene.Instantiate<TransitionScene>();
		AddChild(scene);

		await scene.FadeIn();
		GD.Print("faded in");
		GetTree().ChangeSceneToFile(scenePath);


		await scene.FadeOut();


		EmitSignal(SignalName.TransitionComplete);
		RemoveChild(scene);
		GetTree().Paused = false;
		isTransitioning = false;


	}
}
