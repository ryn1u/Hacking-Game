global using CharacterProperties = HackingGame.Characters.CharacterState.PropertyName;

using System;
using Godot;
using HackingGame.Common;
using HackingGame.Map;

namespace HackingGame.Characters
{
	public partial class CharacterState : Node2D
	{
		[Signal] public delegate void OnPlayerStateChangedEventHandler(CharacterState state, string property);

		// MOVEMENT STATE
		[Export] public Direction Direction { get; private set; }
		[Export] public bool IsWalking { get; private set; }

		// INTERACTION STATE
		[Export] public Interaction Interaction { get; private set; }
		

		public void SetDirection(int direction)
		{
			Direction = (Direction)direction;
			PingStateChanged(CharacterProperties.Direction);
		}

		public void SetIsWalking(bool isWalking)
		{
			IsWalking = isWalking;
			PingStateChanged(CharacterProperties.IsWalking);
		}

		public void SetInteraction(Interaction interaction)
		{
			Interaction = interaction;
			PingStateChanged(CharacterProperties.Interaction);
		}

		public Interaction ConsumeInteraction()
		{
			var interaction = Interaction;
			SetInteraction(null);
			return interaction;
		}

		private void PingStateChanged(string property)
		{
			EmitSignal(SignalName.OnPlayerStateChanged, this, property);
		}
	}
}
