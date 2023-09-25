using Godot;

namespace HackingGame.Characters.Player
{
    public interface IInputHandler
    {
        public bool HandleInput(InputEvent @event);
    }
}