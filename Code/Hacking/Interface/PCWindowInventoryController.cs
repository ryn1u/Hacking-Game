using Godot;
using System;
using System.Collections.Generic;
using HackingProperties = HackingGame.Common.HackingGameplayState.PropertyName;

public partial class PCWindowInventoryController : VBoxContainer
{
	[Export] private PackedScene inventoryLabel;

	private List<Label> labels;

	public override void _Ready()
	{
		var inventory = GameplayState.State.Programs;

		labels = new List<Label>();
		foreach(var program in inventory)
		{
			var instance = inventoryLabel.Instantiate<Label>();
			AddChild(instance);
			labels.Add(instance);
		}
		WrtieLabels(GameplayState.State);

		EventBus.Node.OnGameplayStateChanged += OnHackingGameplayStateChanged;
	}

	public void OnHackingGameplayStateChanged(GameplayState state, string property)
	{
		var baseName = GameplayState.PropertyName.HackingGameplayState;
		
		if(property == $"{baseName}/{HackingProperties.CurrentSelector}" || property == $"{baseName}/{HackingProperties.InventoryCursorPosition}")
		{
			WrtieLabels(state);
		}
	}

	private void WrtieLabels(GameplayState state)
	{
		for (int i = 0; i < labels.Count; i++)
		{
			var text = state.Programs[i].ProgramName;

			bool isCurrentSelector = state.HackingGameplayState.CurrentSelector == HackingGame.Common.CurrentSelector.Inventory;
			if(isCurrentSelector && state.HackingGameplayState.InventoryCursorPosition == i)
			{
				text = ">" + text;
			}
			labels[i].Text = text;
		}
	}
}
