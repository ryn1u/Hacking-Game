using Godot;
using System;

using HackingGame.Characters;
using HackingGame.Map;

namespace HackingGame.Hacking
{
    public partial class HackableInteractionController : Node
    {
        //TODO: crate a resource for hack maps, assign each instance to different HackableTile, OnInteract call player hacking controller with map

        // Interaction flow: Entity enters interaction zone -> zone notifies Entity's interaction controller with available interaction (HERE)
		//		-> interaction controller emits signal with interaction callback -> other nodes in entity decide what to do.
        [Export] private HackableSystemMap system;

        public void OnArea2DEntered(Area2D area2D)
        {
            if(area2D is CharacterInteractionController interactionController)
            {
                var interaction = new Interaction(InteractionType.Hack, this.ToCall(MethodName.Interact));

                interactionController.NotifyInteractionAvailable(interaction);
            }
        }

        public void OnArea2DExited(Area2D area2D)
        {
            if(area2D is CharacterInteractionController interactionController)
            {
                interactionController.NotifyInteractionUnavailable();
            }
        }

        public void Interact()
        {
            EventBus.Call(EventsNames.PlayerStartHacking, system);
        }
    }
}