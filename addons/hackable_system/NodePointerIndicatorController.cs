using Godot;
using System;
using HackingGame.Common;

namespace HackingGame.Hacking
{
    public partial class NodePointerIndicatorController : TextureRect
    {
        [Export] private double tweenDuration;

        public override void _Ready()
        {
            EventBus.Relay.Connect(EventsNames.OnGameplayStateChanged, this.ToCall(MethodName.OnGameplayStateChanged));
        }

        private void OnGameplayStateChanged(GameplayState state, string property)
        {
            var expected = GameplayState.HackingProperty(HackingProperties.NodePointerPosition);
            if(property == expected)
            {
                var pointer = state.HackingGameplayState.NodePointerPosition;
                var node = state.HackingGameplayState.CurrentSystem.Nodes[pointer];
                MovePointer(node);
            }
        }

        private async void MovePointer(NodeResource node)
        {
            var target = node.Position - Size / 2;

            var myTween = CreateTween();
            myTween.TweenProperty(this, PropertyName.Position.ToString(), target, tweenDuration)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.InOut);

            await ToSignal(myTween, Tween.SignalName.Finished);
            EventBus.Call(EventsNames.OnAnimationFinished, "move_pointer_position_animation");
        }
    }
}