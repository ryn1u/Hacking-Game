using System;
using Godot;

namespace HackingGame.Common
{
    public partial class HackingInterfaceState : RefCounted
    {
        private event Action<string> OnHackingInterfaceStateChangedEvent;

        [Export] public CurrentSelector CurrentSelector { get; private set; }
        [Export] public int InventoryCursorPosition { get; private set; }
        [Export] public int SequenceCursorPosition { get; private set; }
        [Export] public Godot.Collections.Array<Program> HackingSequence { get; private set; }


        private string notificationPrefix;


        public HackingInterfaceState(Action<string> onStateChangeCallback, string prefix)
        {
            notificationPrefix = prefix;
            OnHackingInterfaceStateChangedEvent += onStateChangeCallback;
            EventBus.Relay.PlayerStartedHacking += (_) => ResetState();
        }

        public void ResetState()
        {
            HackingSequence = new Godot.Collections.Array<Program>();
            InventoryCursorPosition = 0;
            SequenceCursorPosition = 0;
            CurrentSelector = CurrentSelector.Inventory;
        }

        private void NotifyStateChange(string property)
        {
            OnHackingInterfaceStateChangedEvent.Invoke( notificationPrefix + "/" + property);
        }




        // SEQUENCE CONTROLS
        public void ResetSequence()
        {
            HackingSequence = new Godot.Collections.Array<Program>();
            NotifyStateChange(PropertyName.HackingSequence);
        }

        public void AddProgramToSequence(Program program)
        {
            HackingSequence.Add(program);
            NotifyStateChange(PropertyName.HackingSequence);
        }

        public void RemoveProgramFromSequence(int position)
        {
            HackingSequence.RemoveAt(position);
            NotifyStateChange(PropertyName.HackingSequence);
        }

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