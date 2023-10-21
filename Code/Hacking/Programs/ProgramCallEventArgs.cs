using System;
using Godot;
using System.Threading.Tasks;

namespace HackingGame.Hacking
{
    public partial class ProgramCallEventArgs : RefCounted
    {
        public Task CallCompletionTask { get; set; }
        public NodePath ConnectedObject { get; set; }
    }
}