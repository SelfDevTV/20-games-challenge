using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		float scale = DisplayServer.ScreenGetScale();
		DisplayServer.WindowSetSize(new Vector2I(360 * (int)scale, 640 * (int)scale));

		CenterWindow();

	}

	public void CenterWindow()
	{
		var screen_size = DisplayServer.ScreenGetSize();

		var window_size = DisplayServer.WindowGetSize();

		var new_position = (screen_size - window_size) / 2;


		DisplayServer.WindowSetPosition(new_position);
	}


}
