using Godot;
using System;

public partial class PCHackingWindowController : PanelContainer
{
	[Export] private PCWindowInventoryController inventoryController;

	public override void _Ready()
	{
		var hacking = GameplayState.State.HackingGameplayState;

		hacking.SetInventoryCursor(69);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
