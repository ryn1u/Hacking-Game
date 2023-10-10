using Godot;
using System;

namespace HackingGame.Hacking.Systems
{
	[Tool]
	public partial class SystemEditorPropertyControls : VBoxContainer
	{
		[Signal] public delegate void NewNodeButtonPressedEventHandler();
		[Signal] public delegate void SaveResourceButtonPressedEventHandler();

		public void OnNewNodeButtonPressed()
		{
			EmitSignal(SignalName.NewNodeButtonPressed);
		}

		public void OnSaveResourceButtonPressed()
		{
			EmitSignal(SignalName.SaveResourceButtonPressed);
		}
	}
}