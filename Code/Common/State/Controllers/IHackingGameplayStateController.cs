using System;
using Godot;
using HackingGame.Common.Controllers;
using System.Threading.Tasks;

namespace HackingGame.Common
{
    public interface IHackingGameplayStateController : IModelController
    {
        // OVERALL
        public void ResetState();

        // HACK EXECUTION
        public int GetExecutionPointerPosition();
        public void SetExecutionPointerPosition(int value);
        public int GetNodePointerPosition();
        public void SetNodePointerPosition(int value);


        // HACK SEQUENCE
        public void ResetSequence();
        public void AddProgramToSequence(Program program);
        public void RemoveProgramFromSequence(int position);
        public int GetHackingSequenceCount();
        public Program GetCurrentSequenceProgram();

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