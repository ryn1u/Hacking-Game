using Godot;
using System;
using HackingGame.Common;

public partial class PlayerHackingController : Node
{
	[Export] private PackedScene PC_Interface;
	private const string KEY = "player_pc_interface";
	private IGameplayStateController gameplayStateController;
	private IHackingGameplayStateController hackingGameplayStateController;

	public override void _Ready()
	{
		EventBus.Relay.Connect(EventsNames.PlayerStartedHacking, this.ToCall(MethodName.OnPlayerStartHack));
		EventBus.Relay.Connect(EventsNames.PlayerStoppedHacking, this.ToCall(MethodName.OnPlayerStoppedHacking));

		gameplayStateController = ControllerRegistry.Get<IGameplayStateController>();
		hackingGameplayStateController = ControllerRegistry.Get<IHackingGameplayStateController>();
	}

	private void OnPlayerStartHack(SystemResource system)
	{
		if(gameplayStateController.GetIsHacking()) return;

		var pcInterface = InstantiateInterface();
		pcInterface.InitializePCInterface(system);

		gameplayStateController.SetIsHacking(true);
		hackingGameplayStateController.ResetState();
	}

	private void OnPlayerStoppedHacking()
	{
		if(!gameplayStateController.GetIsHacking()) return;

		DestroyInterface();
		gameplayStateController.SetIsHacking(false);
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
