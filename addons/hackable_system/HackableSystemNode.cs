using Godot;
using System;

[Tool]
public partial class HackableSystemNode : MarginContainer
{
	[Export] public Program Program;
	[Export] public Node ConnectedObject;
}
