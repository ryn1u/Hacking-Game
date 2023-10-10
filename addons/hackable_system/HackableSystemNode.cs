using System;
using System.Collections.Generic;
using Godot;

namespace HackingGame.Hacking.Systems
{
    public partial class HackableSystemNode : Resource
    {
        [Export] public Program Program { get; set; }
        [Export] public Vector2 Position { get; set; }
        [Export] public Node ConnectedNode {get; set; }
    }
}