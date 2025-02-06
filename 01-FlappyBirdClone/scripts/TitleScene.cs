using Godot;
using System;

public partial class TitleScene : Node2D
{

	AnimationPlayer animationPlayer;
	public int highestScore = 8;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		highestScore = SaveSystem.Load();
		GD.Print(highestScore);
		Button btn = GetNode<Button>("%Button");
		btn.Pressed += OnButtonPressed;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.AnimationFinished += OnFadeFinished;
	}

	public void OnButtonPressed()
	{

		// animationPlayer.Play("fade_out");
		TransitionManager.Instance.LoadScene("res://scenes/play_scene.tscn");


	}

	public void OnFadeFinished(StringName anim_name)
	{
		GetTree().ChangeSceneToFile("res://scenes/play_scene.tscn");
	}
}
