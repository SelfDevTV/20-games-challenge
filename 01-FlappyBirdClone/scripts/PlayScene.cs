using Godot;
using System;

public partial class PlayScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		World world = GetNode<World>("World");
		world.GameOver += OnGameOver;
		world.Scored += OnScored;
	}

	public void OnGameOver()
	{
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/title_scene.tscn");
	}

	public void OnScored()
	{
		GD.Print("Scored!");
	}


}
