using Godot;
using System;

namespace HackingGame.Hacking.Systems
{
	[Tool]
	public partial class SystemEditorPropertyControls : VBoxContainer
	{
		[Signal] public delegate void NewNodeButtonPressedEventHandler();
		[Signal] public delegate void NewEdgeButtonPressedEventHandler();
		[Signal] public delegate void SaveButtonPressedEventHandler();
		[Signal] public delegate void LoadButtonPressedEventHandler();

		public void OnNewNodeButtonPressed()
		{
			EmitSignal(SignalName.NewNodeButtonPressed);
		}

		public void OnNewEdgeButtonPressed()
		{
			EmitSignal(SignalName.NewEdgeButtonPressed);
		}
		public void OnSaveButtonPressed()
		{
			EmitSignal(SignalName.SaveButtonPressed);
		}

		public void OnLoadButtonPressed()
		{
			EmitSignal(SignalName.LoadButtonPressed);
		}
	}
}