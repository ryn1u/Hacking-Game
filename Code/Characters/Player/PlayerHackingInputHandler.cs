using Godot;
using System;

using HackingGame.Common;


namespace HackingGame.Characters.Player
{
	public partial class PlayerHackingInputHandler : Node, IInputHandler
	{
		private Func<InputEvent, bool>[] funcs;
		private readonly Direction[] directions = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
		private IGameplayStateController gameplayStateController;
		private IHackingGameplayStateController hackingGameplayStateController;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			gameplayStateController = ControllerRegistry.Get<IGameplayStateController>();
			hackingGameplayStateController = ControllerRegistry.Get<IHackingGameplayStateController>();

			funcs = new Func<InputEvent, bool>[] { HandleInteractionInput, HandleDirectionInput, HandleSelectInput };
		}

		public bool HandleInput(InputEvent @event)
		{
			foreach(var func in funcs)
			{
				if(func(@event))
				{
					return true;
				}
			}
			return false;
		}

		private bool HandleInteractionInput(InputEvent @event)
		{
			if(@event.IsAction("interact") && @event.IsPressed())
			{
				EventBus.Call(EventsNames.PlayerStoppedHacking);
				return true;
			} 
			return false;
		}

		private bool HandleDirectionInput(InputEvent @event)
		{
			// POO POO nesting below FIX!!!
			if(@event.IsReleased()) return false;
			
			for (int i = 0; i < directions.Length; i++)
			{
				var direction = directions[i];
				var name = direction.Name().ToLower();

				if(@event.IsAction(name))
				{
					if(direction == Direction.Left || direction == Direction.Right)
					{
						hackingGameplayStateController.ChangeCurrentSelector();
					}
					else
					{
						var delta = direction == Direction.Up ? -1 : 1;
						// Emit signal so that interface handles this
						switch(hackingGameplayStateController.GetCurrentSelector())
						{
							case CurrentSelector.Inventory:
								// TODO: Move this logic into controller
								hackingGameplayStateController.MoveInventoryCursor(delta);
								break;
							case CurrentSelector.Sequence:
								hackingGameplayStateController.MoveSequenceCursor(delta);
								break;
						}
						
					}
					return true;
				}
			}
			return false;
		}
		
		private bool HandleSelectInput(InputEvent @event)
		{
			if(@event.IsReleased()) return false;
			
			switch(hackingGameplayStateController.GetCurrentSelector())
			{
				case CurrentSelector.Inventory:
					var program = gameplayStateController.GetProgramAtIndex(hackingGameplayStateController.GetInventoryCursorPosition());
					hackingGameplayStateController.AddProgramToSequence(program);
					break;
				case CurrentSelector.Sequence:
					hackingGameplayStateController.RemoveProgramFromSequence(hackingGameplayStateController.GetSequenceCursorPosition());

					var maxValue = hackingGameplayStateController.GetHAckingSequenceCount()- 1;
					hackingGameplayStateController.SetSequenceCursor(Math.Clamp(hackingGameplayStateController.GetSequenceCursorPosition(), 0, maxValue));
					break;
			}
			return true;
		}
		
		private bool HandleCommitInput(InputEvent @event)
		{
			return false;
		}
    }
}