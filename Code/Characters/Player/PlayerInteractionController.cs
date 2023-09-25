using Godot;
using System;
using HackingGame.Map;

namespace HackingGame.Characters.Player
{
	public partial class PlayerInteractionController : Node
	{
		[Export] private PackedScene indicatorScene;
		[Export] private CharacterState playerState;
		private const string KEY = "player_indicator_key";	

		public void OnPlayerStateChanged(CharacterState state, string property)
		{
			if(property == CharacterState.PropertyName.Interaction)
			{
				if(state.Interaction is not null)
				{
					TempInstanceArgs args = new TempInstanceArgs() { Scene = indicatorScene };
					EventBus.Call(EventsNames.CreateTemporaryInstance, KEY, args);
					Owner.AddChild(args.Instance);
				}
				else
				{
					EventBus.Call(EventsNames.DeleteTemporaryInstance, KEY);
				}
			}
		}
		public void OnInputInteract()
		{
			if(playerState.Interaction is not null)
			{
				var interaction = playerState.Interaction;
				interaction.callback.Call();
			}
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}
	}
}
