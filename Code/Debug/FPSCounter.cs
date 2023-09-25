using Godot;
using System;

public partial class FPSCounter : RichTextLabel
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int fps = (int)Math.Round(1 / delta);
		Text = $"FPS: {fps}";
	}
}
