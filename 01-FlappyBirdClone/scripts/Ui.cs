using Godot;
using System;

public partial class Ui : Control
{
	Label scoreLbl;

	public override void _Ready()
	{
		scoreLbl = GetNode<Label>("Label");
	}

	// Called when the node enters the scene tree for the first time.
	public void SetScore(int score)
	{
		scoreLbl.Text = "Score: " + score.ToString();
	}
}
