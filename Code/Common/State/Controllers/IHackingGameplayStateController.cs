using System;
using Godot;
using HackingGame.Common.Controllers;
using HackingGame.Hacking;
using System.Threading.Tasks;

namespace HackingGame.Common
{
    public interface IHackingGameplayStateController : IModelController
    {
        // OVERALL
        public void SetCurrentSystem(SystemResource system);
        public SystemResource GetCurrentSystem();
        public void ResetState();

        // HACK EXECUTION
        public int GetExecutionPointerPosition();
        public void SetExecutionPointerPosition(int value);
        public int GetNodePointerPosition();
        public void SetNodePointerPosition(int value);
        public NodeResource GetNodeAtPointerPosition();
        public int GetNextNodePointerPosition();


        // HACK SEQUENCE
        public void ResetSequence();
        public void AddProgramToSequence(Program program, bool isTemp = false, NodePath connectedNode = null);
        public void RemoveProgramFromSequence(int position);
        public int GetHackingSequenceCount();
        public HackingSequenceElement GetCurrentSequenceElement();
        public void RemoveTemporaryFromSequence();

        // HACKING INTERFACE
        public int GetInventoryCursorPosition();
        public void SetInventoryCursor(int position);
        public void MoveInventoryCursor(int delta);
        
        public int GetSequenceCursorPosition();
        public void SetSequenceCursor(int position);
        public void MoveSequenceCursor(int delta);

        public CurrentSelector GetCurrentSelector();
        public void SetCurrentSelector(CurrentSelector selector);
        public void ChangeCurrentSelector();
    }
}