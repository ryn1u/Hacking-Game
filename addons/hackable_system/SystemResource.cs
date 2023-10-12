using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SystemResource : Resource
{
    [Export] public Godot.Collections.Array<NodeResource> Nodes;
    [Export] public Godot.Collections.Array<Vector2I> Edges;

    public SystemResource()
    {
        Nodes = new Godot.Collections.Array<NodeResource>();
    }
}
