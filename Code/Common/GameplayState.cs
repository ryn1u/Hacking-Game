using Godot;
using System;

public partial class GameplayState : Node
{
	public static GameplayState State {get; set; }
	[Export] public bool IsHacking;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		State = this;
	}
}
