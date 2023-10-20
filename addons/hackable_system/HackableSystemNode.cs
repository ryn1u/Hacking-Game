using Godot;
using System;

[Tool]
public partial class HackableSystemNode : MarginContainer
{
	public int Id;
	[Export] public Program Program;
	[Export] public Node ConnectedObject;
}
