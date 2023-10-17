using Godot;
using System;
using System.Collections.Generic;
using HackingProperties = HackingGame.Common.HackingGameplayState.PropertyName;
using HackingGame.Common;

public partial class PCWindowInventoryController : VBoxContainer
{
	[Export] private PackedScene inventoryLabel;
	[Export] private RichTextLabel descriptionLabel;

	private string baseDescription;
	private List<RichTextLabel> labels;
	private IGameplayStateController gameplayStateController;
	private IHackingGameplayStateController hackingGameplayStateController;

	public override void _Ready()
	{
		gameplayStateController = ControllerRegistry.Get<IGameplayStateController>();
		hackingGameplayStateController = ControllerRegistry.Get<IHackingGameplayStateController>();

		if(descriptionLabel is null) throw new Exception($"No description text lable assinged to pc interface window");
		baseDescription = descriptionLabel.Text;

		labels = new List<RichTextLabel>();
		foreach(Program program in gameplayStateController.GetInventoryProgramsEnumerable())
		{
			var instance = inventoryLabel.Instantiate<RichTextLabel>();
			AddChild(instance);
			labels.Add(instance);
		}
		WrtieLabels();
		UpdateDescriptionBox();

		EventBus.Relay.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnHackingGameplayStateChanged));
	}
    

	public void OnHackingGameplayStateChanged(GameplayState state, string property)
	{
		var baseName = GameplayState.PropertyName.HackingGameplayState;
		
		if(property == $"{baseName}/{HackingProperties.CurrentSelector}")
		{
			WrtieLabels();
		}
		else if(property == $"{baseName}/{HackingProperties.InventoryCursorPosition}")
		{
			WrtieLabels();
			UpdateDescriptionBox();
		}
	}

	private void WrtieLabels()
	{
		for (int i = 0; i < labels.Count; i++)
		{
			var text = gameplayStateController.GetProgramAtIndex(i).ProgramName;

			bool isCurrentSelector = hackingGameplayStateController.GetCurrentSelector() == HackingGame.Common.CurrentSelector.Inventory;
			if(isCurrentSelector && hackingGameplayStateController.GetInventoryCursorPosition() == i)
			{
				text = "[bgcolor=white][color=5e3449]" + text + "[/color][/bgcolor]";
			}
			labels[i].Text = text;
		}
	}

	private void UpdateDescriptionBox()
	{
		var program = gameplayStateController.GetProgramAtIndex(hackingGameplayStateController.GetInventoryCursorPosition());
		var newText = baseDescription.Replace("<name>", program.ProgramName.ToUpper());
		newText = newText.Replace("<description>", program.Description);

		descriptionLabel.Text = newText;
	}
}
