using Godot;
using System;

using HackingGame.Common;


namespace HackingGame.Characters.Player
{
	public partial class PlayerHackingInputHandler : Node, IInputHandler
	{
		private Func<InputEvent, bool>[] funcs;
		private readonly Direction[] directions = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
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
					var state = GameplayState.State;
					var hackingState = state.HackingGameplayState;
					if(direction == Direction.Left || direction == Direction.Right)
					{
						hackingState.ChangeCurrentSelector();
					}
					else
					{
						var delta = direction == Direction.Up ? -1 : 1;
						// Emit signal so that interface handles this
						switch(hackingState.CurrentSelector)
						{
							case CurrentSelector.Inventory:
								var newValueI = Math.Clamp(hackingState.InventoryCursorPosition + delta, 0, state.Programs.Count - 1);
								hackingState.SetInventoryCursor(newValueI);
								break;
							case CurrentSelector.Sequence:
								// TODO: Fix max value
								var newValueS = Math.Clamp(hackingState.SequenceCursorPosition + delta, 0, hackingState.HackingSequence.Count - 1);
								hackingState.SetSequenceCursor(newValueS);
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
			
			var state = GameplayState.State;
			var hackingState = state.HackingGameplayState;
			switch(hackingState.CurrentSelector)
			{
				case CurrentSelector.Inventory:
					var program = state.Programs[hackingState.InventoryCursorPosition];
					hackingState.AddProgramToSequence(program);
					break;
				case CurrentSelector.Sequence:
					hackingState.RemoveProgramFromSequence(hackingState.SequenceCursorPosition);
					hackingState.SetSequenceCursor(Math.Clamp(hackingState.SequenceCursorPosition, 0, hackingState.HackingSequence.Count - 1));
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