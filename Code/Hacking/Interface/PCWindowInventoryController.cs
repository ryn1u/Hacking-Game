using Godot;
using System;
using System.Collections.Generic;
using HackingProperties = HackingGame.Common.HackingGameplayState.PropertyName;

public partial class PCWindowInventoryController : VBoxContainer
{
	[Export] private PackedScene inventoryLabel;
	[Export] private RichTextLabel descriptionLabel;

	private string baseDescription;
	private List<RichTextLabel> labels;

	public override void _Ready()
	{
		var inventory = GameplayState.State.Programs;

		if(descriptionLabel is null) throw new Exception($"No description text lable assinged to pc interface window");
		baseDescription = descriptionLabel.Text;

		labels = new List<RichTextLabel>();
		foreach(var program in inventory)
		{
			var instance = inventoryLabel.Instantiate<RichTextLabel>();
			AddChild(instance);
			labels.Add(instance);
		}
		WrtieLabels(GameplayState.State);
		UpdateDescriptionBox(GameplayState.State);

		EventBus.Node.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnHackingGameplayStateChanged));
	}
    

	public void OnHackingGameplayStateChanged(GameplayState state, string property)
	{
		var baseName = GameplayState.PropertyName.HackingGameplayState;
		
		if(property == $"{baseName}/{HackingProperties.CurrentSelector}")
		{
			WrtieLabels(state);
		}
		else if(property == $"{baseName}/{HackingProperties.InventoryCursorPosition}")
		{
			WrtieLabels(state);
			UpdateDescriptionBox(state);
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
				text = "[bgcolor=white][color=5e3449]" + text + "[/color][/bgcolor]";
			}
			labels[i].Text = text;
		}
	}

	private void UpdateDescriptionBox(GameplayState state)
	{
		var program = state.Programs[state.HackingGameplayState.InventoryCursorPosition];
		var newText = baseDescription.Replace("<name>", program.ProgramName.ToUpper());
		newText = newText.Replace("<description>", program.Description);

		descriptionLabel.Text = newText;
	}
}
