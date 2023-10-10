using Godot;
using HackingGame.Hacking.Systems;
using System;

[Tool]
public partial class HackableSystemMap : Resource
{
    [Export] public string Data { get; set; }
    [Export] public Godot.Collections.Array<HackableSystemNode> Nodes { get; set; }
    [Export] public Godot.Collections.Array<Vector2I> Edges { get; set; }
}
