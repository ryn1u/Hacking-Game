using Godot;
using HackingGame.Common;

public partial class GameplayState : Node
{
	[ExportCategory("Player's variables")]
	[Export] public bool IsHacking { get; private set; }
	[Export] public HackingGameplayState HackingGameplayState { get; private set; }

	[Export] public Godot.Collections.Array<Program> Programs { get; set; }

	public void SetIsHacking(bool value)
	{
		IsHacking = value;
		NotifyStateChange(PropertyName.IsHacking);
	}

	private void NotifyStateChange(string property)
	{
		EventBus.Call(EventsNames.OnGameplayStateChanged, this, property);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HackingGameplayState = new HackingGameplayState(NotifyStateChange, PropertyName.HackingGameplayState);
	}
}
