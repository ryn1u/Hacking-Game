using Godot;
using System;

public partial class DebugPanel : CanvasLayer
{
	[Export] public bool EnableDebug;

	public override void _Ready()
	{
		ToggleDebug();
	}

	private void ToggleDebug()
	{
		ProcessMode = EnableDebug ? ProcessModeEnum.Inherit : ProcessModeEnum.Disabled;
		Visible = EnableDebug;
	}
}
