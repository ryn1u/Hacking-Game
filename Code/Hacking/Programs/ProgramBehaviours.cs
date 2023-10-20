using Godot;
using System;
using HackingGame.Common;

namespace HackingGame.Hacking
{
    [Tool]
    public partial class ProgramBehaviours : Node
    {
        [Export] public Godot.Collections.Dictionary<string, Godot.GodotObject> ProgramBehavioursRegistry { get; set; }

        private IGameplayStateController stateController;
		private IHackingGameplayStateController hackingController;

        private void ExecuteMvptr(ProgramCallEventArgs args)
        {
            var nodePointerPosition = hackingController.GetNodePointerPosition() + 1;
			hackingController.SetNodePointerPosition(nodePointerPosition);

            var animationName = "move_pointer_position_animation";
            AnimationSignalListener listener = new AnimationSignalListener(animationName);

            args.CallCompletionTask = listener.Task;
        }

		private void ExecuteDoorCtrl(ProgramCallEventArgs args)
		{
			GD.Print("Open door");
		}

        public override void _Ready()
        {
            if (Engine.IsEditorHint())
            {
                if (ProgramBehavioursRegistry is null)
                {
                    ProgramBehavioursRegistry = new Godot.Collections.Dictionary<string, Godot.GodotObject>();
                }

                SearchForMissingMembers();
            }
            else
            {
                Initialize();
                SubscribeProgramsToSignals();
            }
        }

        private void Initialize()
        {
			stateController = ControllerRegistry.Get<IGameplayStateController>();
			hackingController = ControllerRegistry.Get<IHackingGameplayStateController>();
        }

        private void SearchForMissingMembers()
        {
            var fields = typeof(ProgramBehaviours.MethodName).GetFields();
            foreach (var field in fields)
            {
                var fieldName = field.Name;
                if (fieldName.Contains("Execute"))
                {
                    if (!ProgramBehavioursRegistry.ContainsKey(fieldName))
                    {
                        ProgramBehavioursRegistry.Add(fieldName, null);
                    }
                }
            }
        }

        private void SubscribeProgramsToSignals()
        {
            foreach (var pair in ProgramBehavioursRegistry)
            {
                var method = pair.Key;
                var program = pair.Value;
                if (program is not null)
                {
                    program.Connect(Program.SignalName.ProgramBehaviour, new Callable(this, method));
                }
            }
        }
    }
}
