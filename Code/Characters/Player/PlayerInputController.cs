using System;
using Godot;
using HackingGame.Common;

namespace HackingGame.Characters.Player
{
	/// <summary>
	/// Handles and forwards input events
	/// </summary>
	public partial class PlayerInputController : Node
	{
		[Export] private 
		PlayerWorldInputHandler worldInputHandler;
		private Viewport viewport;

		private IInputHandler currentHandler;

		public override void _Ready()
		{
			viewport = GetViewport();
			currentHandler = worldInputHandler;
		}



		public override void _UnhandledInput(InputEvent @event)
		{
			if(@event.IsActionType())
			{
				var result = currentHandler.HandleInput(@event);
				if(result)
				{
					viewport.SetInputAsHandled();
				}
			}
		}

		public void OnInputModeChanged(InputMode mode)
		{
			switch(mode)
			{
				case InputMode.World:
					currentHandler = worldInputHandler;
					break;
				case InputMode.Hacking:
					currentHandler = null;
					break;
			}
		}

		public enum InputMode
		{
			World,
			Hacking,
		}
	}
}