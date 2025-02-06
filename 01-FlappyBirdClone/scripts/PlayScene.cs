using Godot;
using System;

public partial class PlayScene : Node2D
{
	// Called when the node enters the scene tree for the first time.

	Ui ui;
	int score = 0;

	AudioStreamPlayer2D ScoredSound;
	AudioStreamPlayer2D GameOverSound;

	public override void _Ready()
	{
		int highScore = SaveSystem.Load();
		World world = GetNode<World>("World");
		world.GameOver += OnGameOver;
		world.Scored += OnScored;
		ui = GetNode<Ui>("Ui");
		ui.SetMaxScore(highScore);
		ScoredSound = GetNode<AudioStreamPlayer2D>("ScoredSound");
		// GetTree().Paused = TransitionManager.Instance.isTransitioning;

		TransitionManager.Instance.TransitionComplete += StartGame;


	}


	public void StartGame()
	{


	}

	public void OnGameOver()
	{
		int highScore = SaveSystem.Load();
		GD.Print(highScore);
		if (score > highScore)
		{
			SaveSystem.Save(score);
		}
		TransitionManager.Instance.LoadScene("res://scenes/title_scene.tscn");

	}

	public void OnScored()
	{
		score++;
		ui.SetScore(score);
		ScoredSound.Play();
	}


}
