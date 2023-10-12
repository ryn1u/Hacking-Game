using Godot;
using System;

[Tool]
[GlobalClass]
public partial class NodeResource : Resource
{
    [Export] public Vector2 Position;
    [Export] public Program Program;
    [Export] public NodePath ConnectedNode;

    public NodeResource()
    {
        Position = Vector2.Zero;
        Program = null;
        ConnectedNode = null;
    }

    public NodeResource(Vector2 position, Program program)
    {
        Position = position;
        Program = program;
        ConnectedNode = null;
    }
}
