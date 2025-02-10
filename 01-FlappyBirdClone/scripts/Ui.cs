using Godot;
using System;

public partial class Ui : Control
{


	Label scoreLbl;
	Label maxScoreLbl;

	AnimationPlayer animationPlayer;

	Button backBtn, closeBtn;

	public override void _Ready()
	{
		scoreLbl = GetNode<Label>("Score");
		maxScoreLbl = GetNode<Label>("HighestScore");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		backBtn = GetNode<Button>("PauseMenu/BackBtn");
		closeBtn = GetNode<Button>("PauseMenu/CloseBtn");

		backBtn.Pressed += () =>
		{
			GetTree().Paused = false;
			TransitionManager.Instance.LoadScene("res://scenes/title_scene.tscn");
		};

		closeBtn.Pressed += () =>
		{
			GetTree().Quit();
		};
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			if (GetTree().Paused)
			{
				GetTree().Paused = false;
				animationPlayer.Play("slide_out");
			}
			else
			{
				GetTree().Paused = true;
				animationPlayer.Play("slide_in");
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public void SetScore(int score)
	{
		scoreLbl.Text = "Score: " + score.ToString();
	}

	public void SetMaxScore(int maxScore)
	{
		maxScoreLbl.Text = "Highest Score: " + maxScore.ToString();
	}
}
