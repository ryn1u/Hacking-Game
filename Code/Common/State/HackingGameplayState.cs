global using HackingProperties = HackingGame.Common.HackingGameplayState.PropertyName;
using System;
using Godot;
using HackingGame.Hacking;

namespace HackingGame.Common
{
    public partial class HackingGameplayState : RefCounted
    {
        private event Action<string> OnHackingGameplayStateChangedEvent;

        //GENERAL
        [Export] public SystemResource CurrentSystem { get; private set; }

        //HACK INTERFACE STATE
        [Export] public CurrentSelector CurrentSelector { get; private set; }
        [Export] public int InventoryCursorPosition { get; private set; }
        [Export] public int SequenceCursorPosition { get; private set; }
        [Export] public Godot.Collections.Array<HackingSequenceElement> HackingSequence { get; private set; }

        //HACK EXECUTION STATE
        [Export] public int ExecutionPointerPosition { get; private set; }
        [Export] public int NodePointerPosition { get; private set; }

        // prefix is used for state change notifycation. Prefix means the "HackingGameplayState" part of "HackingGameplayState/<propertyname>"
        private string notificationPrefix; 


        public HackingGameplayState(Action<string> onStateChangeCallback, string prefix)
        {
            notificationPrefix = prefix;
            OnHackingGameplayStateChangedEvent += onStateChangeCallback;
            EventBus.Relay.PlayerStartedHacking += (_) => ResetState();
        }

        public void ResetState()
        {
            HackingSequence = new Godot.Collections.Array<HackingSequenceElement>();
            InventoryCursorPosition = 0;
            SequenceCursorPosition = 0;
            CurrentSelector = CurrentSelector.Inventory;
            ExecutionPointerPosition = 0;
            NodePointerPosition = 0;
            CurrentSystem = null;
        }

        private void NotifyStateChange(string property)
        {
            OnHackingGameplayStateChangedEvent.Invoke( notificationPrefix + "/" + property);
        }

        // GENERAL
        public void SetCurrentSystem(SystemResource system)
        {
            CurrentSystem = system;
            NotifyStateChange(PropertyName.CurrentSystem);
        }

        // EXECUTION CONTROLS
        public void SetExecutionPointerPosition(int value)
        {
            ExecutionPointerPosition = value;
            NotifyStateChange(PropertyName.ExecutionPointerPosition);
        }

        public void SetNodePointerPosition(int value)
        {
            NodePointerPosition = value;
            NotifyStateChange(PropertyName.NodePointerPosition);
        }

        // SEQUENCE CONTROLS
        public void ResetSequence()
        {
            HackingSequence = new Godot.Collections.Array<HackingSequenceElement>();
            NotifyStateChange(PropertyName.HackingSequence);
        }

        public void AddProgramToSequence(Program program, bool isTemp, NodePath path)
        {
            HackingSequence.Add(new HackingSequenceElement(program, isTemp, path));
            NotifyStateChange(PropertyName.HackingSequence);
        }

        public void RemoveElementFromSequence(int position)
        {
            HackingSequence.RemoveAt(position);
            NotifyStateChange(PropertyName.HackingSequence);
        }
        // INVENTORY CONTROLS
        public void SetInventoryCursor(int position)
        {
            InventoryCursorPosition = position;
            NotifyStateChange(PropertyName.InventoryCursorPosition);
        }

        public void SetSequenceCursor(int position)
        {
            SequenceCursorPosition = position;
            NotifyStateChange(PropertyName.SequenceCursorPosition);
        }

        public void SetCurrentSelector(CurrentSelector selector)
        {
            CurrentSelector = selector;
            NotifyStateChange(PropertyName.CurrentSelector);
        }

        public void ChangeCurrentSelector()
        {
            CurrentSelector = CurrentSelector == CurrentSelector.Inventory ? CurrentSelector.Sequence : CurrentSelector.Inventory;
            NotifyStateChange(PropertyName.CurrentSelector);
        }
    }
}