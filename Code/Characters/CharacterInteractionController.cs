using Godot;
using System;
using HackingGame.Common;
using HackingGame.Map;

namespace HackingGame.Characters
{
	public partial class CharacterInteractionController : Area2D
	{
		[Signal] public delegate void OnNotifyInteractionAvailableEventHandler(Interaction interaction);
		[Signal] public delegate void OnNotifyInteractionUnavailableEventHandler();
		[Export] private CharacterState state; // REFACTOR: Remove this 
		[Export] private float distance;

		public void NotifyInteractionAvailable(Interaction interaction)
		{
			// Interaction flow: Entity enters interaction zone -> zone notifies Entity's interaction controller with available interactions
			//		-> interaction controller emits signal with interaction callback (HERE) -> other nodes in entity decide what to do.
			state.SetInteraction(interaction);
		}

		public void NotifyInteractionUnavailable()
		{
			// Interaction flow: Entity enters interaction zone -> zone notifies Entity's interaction controller with available interactions
			//		-> interaction controller emits signal with interaction callback (HERE) -> other nodes in entity decide what to do.
			state.SetInteraction(null);
		}

        public override void _Process(double delta)
        {
			Position = state.Direction.ToVec2() * distance;
        }
	}
}
