using Godot;
using System;
using System.Linq;
using HackingGame.Hacking;

namespace HackingGame.Common
{
    public partial class DefaultHackingGameplayStateController : Node, IHackingGameplayStateController
    {
        [Export] private NodePath gameplayStatePath;
        private GameplayState gameplayState { get; set; }

        public Type ControllerInterface => typeof(IHackingGameplayStateController);

        public override void _Ready()
        {
            gameplayState = GetNode<GameplayState>(gameplayStatePath);
        }

        //GENERAL

        public void SetCurrentSystem(SystemResource system)
        {
            gameplayState.HackingGameplayState.SetCurrentSystem(system);
        }

        public SystemResource GetCurrentSystem()
        {
            return gameplayState.HackingGameplayState.CurrentSystem;
        }

        public void ResetState()
        {
            gameplayState.HackingGameplayState.ResetState();
        }

        //SEQUENCE
        public void AddProgramToSequence(Program program, bool isTemp = false, NodePath connectedNode = null)
        {
            gameplayState.HackingGameplayState.AddProgramToSequence(program, isTemp, connectedNode);
        }
        public void RemoveProgramFromSequence(int position)
        {
            gameplayState.HackingGameplayState.RemoveElementFromSequence(position);
        }
        public int GetHackingSequenceCount()
        {
            return gameplayState.HackingGameplayState.HackingSequence.Count;
        }
        public void ResetSequence()
        {
            gameplayState.HackingGameplayState.ResetSequence();
        }

        public HackingSequenceElement GetCurrentSequenceElement()
        {
            var idx = gameplayState.HackingGameplayState.ExecutionPointerPosition;
            return gameplayState.HackingGameplayState.HackingSequence[idx];
        }

        public void RemoveTemporaryFromSequence()
        {
            for(int i = gameplayState.HackingGameplayState.HackingSequence.Count - 1; i >= 0; i--)
            {
                var sequenceElement = gameplayState.HackingGameplayState.HackingSequence[i];
                if(sequenceElement.IsTemporary)
                {
                    gameplayState.HackingGameplayState.RemoveElementFromSequence(i);
                }
            }
        }

        // SELECTOR
        public void SetCurrentSelector(CurrentSelector selector)
        {
            throw new NotImplementedException();
        }

        public void ChangeCurrentSelector()
        {
            gameplayState.HackingGameplayState.ChangeCurrentSelector();
        }

        public CurrentSelector GetCurrentSelector()
        {
            return gameplayState.HackingGameplayState.CurrentSelector;
        }

        // HACKING INTERFACE
        public int GetInventoryCursorPosition()
        {
            return gameplayState.HackingGameplayState.InventoryCursorPosition;
        }

        public void SetInventoryCursor(int position)
        {
            gameplayState.HackingGameplayState.SetInventoryCursor(position);
        }

        public int GetSequenceCursorPosition()
        {
            return gameplayState.HackingGameplayState.SequenceCursorPosition;
        }

        public void SetSequenceCursor(int position)
        {
            gameplayState.HackingGameplayState.SetSequenceCursor(position);
        }

        public void MoveInventoryCursor(int delta)
        {
            var maxValue = gameplayState.Programs.Count - 1;
            var newValue = Math.Clamp(gameplayState.HackingGameplayState.InventoryCursorPosition + delta, 0, maxValue);
            SetInventoryCursor(newValue);
        }

        public void MoveSequenceCursor(int delta)
        {
            var maxValue = Math.Max(gameplayState.HackingGameplayState.HackingSequence.Count - 1, 0);
            var newValue = Math.Clamp(gameplayState.HackingGameplayState.SequenceCursorPosition + delta, 0, maxValue);
			SetSequenceCursor(newValue);
        }

        // HACKING GAMEPLAY EXECUTION
        public int GetExecutionPointerPosition()
        {
            return gameplayState.HackingGameplayState.ExecutionPointerPosition;
        }

        public int GetNodePointerPosition()
        {
            return gameplayState.HackingGameplayState.NodePointerPosition;
        }

        public void SetExecutionPointerPosition(int value)
        {
            gameplayState.HackingGameplayState.SetExecutionPointerPosition(value);
        }

        public void SetNodePointerPosition(int value)
        {
            gameplayState.HackingGameplayState.SetNodePointerPosition(value);
        }

        public NodeResource GetNodeAtPointerPosition()
        {
            var pointer = gameplayState.HackingGameplayState.NodePointerPosition;
            return gameplayState.HackingGameplayState.CurrentSystem.Nodes[pointer];
        }

        public int GetNextNodePointerPosition()
        {
            var system = gameplayState.HackingGameplayState.CurrentSystem;
            var currentIdx = gameplayState.HackingGameplayState.NodePointerPosition;
            return system.Edges.First(edge => edge.X == currentIdx).Y;
        }
    }
}
