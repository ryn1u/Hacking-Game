using Godot;
using System;

public partial class PlayerHackingController : Node
{
	[Export] private PackedScene PC_Interface;
	private const string KEY = "player_pc_interface";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventBus.Node.Connect(EventsNames.PlayerStartHacking, this.ToCall(MethodName.OnPlayerStartHack));
		EventBus.Node.Connect(EventsNames.PlayerStoppedHacking, this.ToCall(MethodName.OnPlayerStoppedHacking));
	}

	private void OnPlayerStartHack(HackableSystemMap system)
	{
		if(GameplayState.State.IsHacking) return;

		var pcInterface = InstantiateInterface();
		pcInterface.InitializePCInterface(system);

		GameplayState.SetIsHacking(true);
		GameplayState.State.HackingGameplayState.ResetState();
	}

	private void OnPlayerStoppedHacking()
	{
		if(!GameplayState.State.IsHacking) return;

		DestroyInterface();
		GameplayState.SetIsHacking(false);
	}

	private PCInterfaceController InstantiateInterface()
	{
		var args = new TempInstanceArgs() { Scene = PC_Interface };
		EventBus.Call(EventsNames.CreateTemporaryInstance, KEY, args);

		// Some coupling here. Maybe fix by moving all interface related state variables into gameplay state
		var instance = (PCInterfaceController)args.Instance;
		GetTree().Root.AddChild(instance);

		return instance;
	}

	private void DestroyInterface()
	{
		EventBus.Call(EventsNames.DeleteTemporaryInstance, KEY);	
	}
}
