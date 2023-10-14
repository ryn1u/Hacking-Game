using Godot;
using HackingGame.Common;

public partial class GameplayState : Node
{
	public static GameplayState State {get; set; }

	[ExportCategory("Player's variables")]
	[Export] public bool IsHacking { get; private set; }
	[Export] public HackingGameplayState HackingGameplayState { get; private set; }

	[Export] public Godot.Collections.Array<Program> Programs { get; set; }

	public static void SetIsHacking(bool value)
	{
		State.IsHacking = value; 
		NotifyStateChange(PropertyName.IsHacking);
	}

	private static void NotifyStateChange(string property)
	{
		EventBus.Call(EventsNames.OnGameplayStateChanged, State, property);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		State = this;
		HackingGameplayState = new HackingGameplayState(NotifyStateChange, PropertyName.HackingGameplayState);
	}
}
