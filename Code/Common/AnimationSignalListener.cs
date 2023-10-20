using System;
using System.Collections.Generic;
using Godot;
using System.Threading.Tasks;

namespace HackingGame.Common
{
    public partial class AnimationSignalListener : RefCounted
    {
        private string animation;
        private TaskCompletionSource tsc;

        public Task Task { get => tsc.Task; }

        public AnimationSignalListener(string animation)
        {
            this.animation = animation;
            Reset();
        }

        public void Reset()
        {
            EventBus.Relay.Connect(EventsNames.OnAnimationFinished, Callable.From<string>(OnAnimationFinished));
            tsc = new TaskCompletionSource();
        }

        private void OnAnimationFinished(string animation)
        {
            if(this.animation == animation)
            {
                tsc.SetResult();
                EventBus.Relay.Disconnect(EventsNames.OnAnimationFinished, Callable.From<string>(OnAnimationFinished));
            }
        }
    }
}