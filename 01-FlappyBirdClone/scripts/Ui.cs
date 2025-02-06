using Godot;
using System;

public partial class Ui : Control
{
	Label scoreLbl;
	Label maxScoreLbl;

	public override void _Ready()
	{
		scoreLbl = GetNode<Label>("Score");
		maxScoreLbl = GetNode<Label>("HighestScore");
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
