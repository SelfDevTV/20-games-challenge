using Godot;
using System;

public partial class PlayScene : Node2D
{
	// Called when the node enters the scene tree for the first time.

	Ui ui;
	int score = 0;

	public override void _Ready()
	{
		World world = GetNode<World>("World");
		world.GameOver += OnGameOver;
		world.Scored += OnScored;
		ui = GetNode<Ui>("Ui");

	}

	public void OnGameOver()
	{
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/title_scene.tscn");
	}

	public void OnScored()
	{
		score++;
		ui.SetScore(score);
	}


}
