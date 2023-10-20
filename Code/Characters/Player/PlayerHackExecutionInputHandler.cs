using Godot;
using System;

namespace HackingGame.Characters.Player
{
    public partial class PlayerHackExecutionInputHandler : Node, IInputHandler
    {
        public bool HandleInput(InputEvent @event)
		{
			return false;
		}
    }
}