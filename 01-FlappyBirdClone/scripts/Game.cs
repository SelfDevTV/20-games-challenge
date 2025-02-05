using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		float scale = DisplayServer.ScreenGetScale();
		Window window = GetWindow();
		window.Size = new Vector2I(360 * (int)scale, 640 * (int)scale);
		CenterWindow(window);
	}

	public void CenterWindow(Window window)
	{
		int primaryScreenIndex = DisplayServer.GetPrimaryScreen();
		// Get the absolute position of the primary screen
		Vector2I primaryScreenPosition = DisplayServer.ScreenGetPosition(primaryScreenIndex);
		Vector2I primaryScreenSize = DisplayServer.ScreenGetSize(primaryScreenIndex);
		Vector2I windowSize = window.Size;

		// Calculate new position relative to global screen coordinates
		Vector2I newPosition = primaryScreenPosition + (primaryScreenSize - windowSize) / 2;

		// Set the new absolute position
		window.Position = newPosition;


	}


}
