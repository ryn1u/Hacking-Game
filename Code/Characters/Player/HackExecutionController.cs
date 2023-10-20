using Godot;
using System;
using System.Threading.Tasks;
using HackingGame.Common;

namespace HackingGame.Hacking
{
    public partial class HackExecutionController : Node
    {
        [Export] private PackedScene nodeIndicatorScene;

        private IHackingGameplayStateController hackingController;
        private bool nodePointerMovedFlag;

        public override void _Ready()
        {
            EventBus.Relay.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnGameplayStateChanged));

            hackingController = ControllerRegistry.Get<IHackingGameplayStateController>();
        }

        private void OnGameplayStateChanged(GameplayState state, string property)
        {
            if(property == GameplayState.PropertyName.GameplayMode && state.GameplayMode == Common.GameplayMode.HackExecution)
            {
                ExecuteHack(state);
            }
            else if(property == HackingProperties.NodePointerPosition)
            {
                nodePointerMovedFlag = true;
            }
        }

        private async void ExecuteHack(GameplayState state)
        {
            // Spawn node pointer indicator
            EventBus.Call(EventsNames.ToggleNodeIndicator, true);

            await Task.Delay(500);

            while(true)
            {
                var program = hackingController.GetCurrentSequenceProgram();
                nodePointerMovedFlag = false;

                // Execute program at sequence pointer
                var args = new ProgramCallEventArgs();
                program.EmitSignal(Program.SignalName.ProgramBehaviour, args);

                if(args.CallCompletionTask is not null)
                {
                    await args.CallCompletionTask;
                }

                // If Node pointer is moved add program at next node to sequence
                if(nodePointerMovedFlag)
                {
                    var newNode = SignalEventArguments<HackableSystemNode>.Call(EventsNames.RequestNodeAtPointerPosition);
                    var newProgram = newNode.Program;
                    hackingController.AddProgramToSequence(newProgram);
                }

                // Move sequence pointer to next node
                var sequencePointerPos = hackingController.GetExecutionPointerPosition() + 1;
                if(sequencePointerPos >= hackingController.GetHackingSequenceCount())
                {
                    break;
                }
                else
                {
                    hackingController.SetExecutionPointerPosition(sequencePointerPos);
                }

                await Task.Delay(500);
            }

            await Task.Delay(1000);

            // FINISH
            EventBus.Call(EventsNames.ToggleNodeIndicator, false);

            hackingController.SetExecutionPointerPosition(0);
            hackingController.SetNodePointerPosition(0);

            EventBus.Call(EventsNames.HackExecutionFinished);
        }
    }
}
