using Godot;
using System;

namespace HackingGame.Characters.Player
{
	
	/// <summary>Manages responses to player character based on inputs and state</summary>
	public partial class PlayerMovementController : Node
	{
		[Export] private CharacterState state; // REFACTOR: Remove this

		public void OnInputDirectionChanged(int direction)
		{
			if(state.IsWalking == false)
			{
				state.SetIsWalking(true);
			}
			if((int)state.Direction != direction)
			{
				state.SetDirection(direction);
			}
		}

		public void OnInputStop()
		{
			state.SetIsWalking(false);
		}
	}
}
