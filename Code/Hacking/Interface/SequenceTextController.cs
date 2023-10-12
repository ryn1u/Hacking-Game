using Godot;
using System;
using System.Text;
using System.Collections.Generic;
using HackingProperties = HackingGame.Common.HackingInterfaceState.PropertyName;

public partial class SequenceTextController : RichTextLabel
{
	private List<string> sequnceStateProperties;
	private string baseText;

    public override void _Ready()
    {
		baseText = Text;
		var baseName = GameplayState.PropertyName.HackingInterfaceState;

		sequnceStateProperties = new List<string>() {
			$"{baseName}/{HackingProperties.CurrentSelector}",
			$"{baseName}/{HackingProperties.SequenceCursorPosition}",
			$"{baseName}/{HackingProperties.HackingSequence}"
		};

		EventBus.Relay.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnHackingSequenceChanged));
    }

	private void OnHackingSequenceChanged(GameplayState state, string property)
	{
		if(sequnceStateProperties.Contains(property))
		{
			var hackingState = state.HackingInterfaceState;
			var rows = new List<string>();

			for (int i = 0; i < hackingState.HackingSequence.Count; i++)
			{
				var program = hackingState.HackingSequence[i];
				var cellStrs = new string[] { program.Addr1.ToString("X4"), program.Addr2.ToString("X2"), "10", program.ProgramName.ToUpper(), "--" };
				if(hackingState.CurrentSelector == HackingGame.Common.CurrentSelector.Sequence && hackingState.SequenceCursorPosition == i)
				{
					var row = "[cell bg=white][color=5e3449]" + string.Join("[/color][/cell][cell bg=white][color=5e3449]", cellStrs) + "[/color][/cell]";
					rows.Add(row);
				}
				else
				{
					var row = "[cell]" + string.Join("[/cell][cell]", cellStrs) + "[/cell]";
					rows.Add(row);
				}
			}

			Text = baseText + string.Join('\n', rows);
		}
	}
}
