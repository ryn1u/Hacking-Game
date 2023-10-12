using Godot;
using System;
using HackingGame.Common;
using HackingGame.Map;

namespace HackingGame.Characters
{
	public partial class CharacterInteractionController : Area2D
	{
		[Signal] public delegate void OnInteractionAvailabilityChangedEventHandler(Interaction interaction);
		[Export] private Direction direction = Direction.Down;
		[Export] private float distance;

		public void NotifyInteractionAvailable(Interaction interaction)
		{
			EmitSignal(SignalName.OnInteractionAvailabilityChanged, interaction);
		}

		public void NotifyInteractionUnavailable()
		{
			EmitSignal(SignalName.OnInteractionAvailabilityChanged, null);
		}

		public void OnCharacterStateChanged(CharacterState state, string property)
		{
			if(property == CharacterProperties.Direction)
			{
				direction = state.Direction;
			}
		}

        public override void _Process(double delta)
        {
			Position = direction.ToVec2() * distance;
        }
	}
}
