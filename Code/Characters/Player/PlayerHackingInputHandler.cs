using Godot;
using System;

using HackingGame.Common;


namespace HackingGame.Characters.Player
{
	public partial class PlayerHackingInputHandler : Node, IInputHandler
	{
		private Func<InputEvent, bool>[] funcs;


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			funcs = new Func<InputEvent, bool>[] { HandleInteractionInput };
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
    }
}