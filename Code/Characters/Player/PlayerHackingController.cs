using Godot;
using System;
using HackingGame.Common;

namespace HackingGame.Characters.Player
{
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

            EventBus.Relay.Connect(EventsNames.PlayerStartedHackExecution, this.ToCall(MethodName.OnPlayerStartedHackExecution));
            EventBus.Relay.Connect(EventsNames.HackExecutionFinished, this.ToCall(MethodName.OnHackExecutionFinished));

            gameplayStateController = ControllerRegistry.Get<IGameplayStateController>();
            hackingGameplayStateController = ControllerRegistry.Get<IHackingGameplayStateController>();
        }

        private void OnPlayerStartHack(SystemResource system)
        {
            if (gameplayStateController.GetGameplayMode() == GameplayMode.Hacking) return;

            var pcInterface = InstantiateInterface();
            pcInterface.InitializePCInterface(system);

            gameplayStateController.SetGameplayMode(GameplayMode.Hacking);
            hackingGameplayStateController.ResetState();
        }

        private void OnPlayerStoppedHacking()
        {
            if (gameplayStateController.GetGameplayMode() == GameplayMode.World) return;

            DestroyInterface();
            gameplayStateController.SetGameplayMode(GameplayMode.World);
        }

        private void OnPlayerStartedHackExecution()
        {
            gameplayStateController.SetGameplayMode(GameplayMode.HackExecution);
        }

        private void OnHackExecutionFinished()
        {
            gameplayStateController.SetGameplayMode(GameplayMode.Hacking);
        }

        private PCInterfaceController InstantiateInterface()
        {
            var args = new TempInstanceArgs(PC_Interface);
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
}
