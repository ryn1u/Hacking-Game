using Godot;
using System;

namespace ToDoPlugin
{
	[Tool]
	public partial class ListElement : HBoxContainer
	{
		[Signal] public delegate void OnDeleteButtonPressedEventHandler(Node node);
		[Export] RichTextLabel Label;
		[Export] CheckBox checkBox;

		private string myString;

		public void Initialize(string text, bool toggled)
		{
			myString = text;
			checkBox.ButtonPressed = toggled;
			OnCheckBoxToggled(toggled);
		}

		public void OnDeleteButtonPressedEvent()
		{
			EmitSignal(SignalName.OnDeleteButtonPressed, this);
		}

		public void OnCheckBoxToggled(bool toggle)
		{
			if(toggle)
			{
				Label.Text = $"[s]{myString}[/s]";
			}
			else
			{
				Label.Text = myString;
			}
		}
	}
}
