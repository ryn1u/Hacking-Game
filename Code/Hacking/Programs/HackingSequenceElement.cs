using System;
using Godot;

namespace HackingGame.Hacking
{
    public partial class HackingSequenceElement : RefCounted
    {
        public HackingSequenceElement(Program program, bool isTemporary, NodePath path)
        {
            Program = program;
            IsTemporary = isTemporary;
            ConnectedNode = path;
        }

        public Program Program { get; private set; }
        public bool IsTemporary { get; private set; }
        public NodePath ConnectedNode {get; private set; }
    }
}