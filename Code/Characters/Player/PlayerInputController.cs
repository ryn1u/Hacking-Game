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

			EventBus.Relay.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnGameplayStateChanged));
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
			if(property == GameplayState.PropertyName.GameplayMode)
			{
				OnInputModeChanged(state.GameplayMode);
			}
		}

		public void OnInputModeChanged(GameplayMode mode)
		{
			switch(mode)
			{
				case GameplayMode.World:
					currentHandler = worldInputHandler;
					break;
				case GameplayMode.Hacking:
					currentHandler = hackingInputHandler;
					break;
			}
		}
	}
}