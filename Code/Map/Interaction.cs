using System;
using Godot;

namespace HackingGame.Map
{
    public partial class Interaction : RefCounted
    {
        public Interaction(InteractionType type, Callable callback)
        {
            this.type = type;
            this.callback = callback;
        }
        public InteractionType type;
        public Callable callback;
    }

    public enum InteractionType
    {
        Hack,

    }
}