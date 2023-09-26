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
		[Export] private PlayerWorldInputHandler worldInputHandler;
		[Export] private PlayerHackingInputHandler hackingInputHandler;
		private Viewport viewport;

		private IInputHandler currentHandler;

		public override void _Ready()
		{
			viewport = GetViewport();
			currentHandler = worldInputHandler;

			EventBus.Node.OnGameplayStateChanged += OnGameplayStateChanged;
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

		private void OnGameplayStateChanged(GameplayState state, string property)
		{
			if(property == GameplayState.PropertyName.IsHacking)
			{
				if(state.IsHacking)
				{
					OnInputModeChanged(InputMode.Hacking);
				}
				else
				{
					OnInputModeChanged(InputMode.World);
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
					currentHandler = hackingInputHandler;
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