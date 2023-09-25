using Godot;
using System;
using HackingGame.Characters;


namespace HackingGame.Map
{
	public partial class DoorTileController : Node2D
	{
		[Export] private bool Open {get; set; }
		[Export] private CollisionShape2D OpenShape;
		[Export] private CollisionShape2D ClosedShape;
		[Export] private AnimatedSprite2D Sprite;

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

		public async void Interact()
		{
			if(!Open)
			{
				// Open door
				Open = true;
				ClosedShape.Disabled = true;

				Sprite.SpeedScale = 1;
				Sprite.Play();
				await ToSignal(Sprite, AnimatedSprite2D.SignalName.AnimationFinished);

				OpenShape.Disabled = false;
			}
			else
			{
				// Close door
				Open = false;
				ClosedShape.Disabled = false;

				Sprite.SpeedScale = -1;
				Sprite.Play();
				await ToSignal(Sprite, AnimatedSprite2D.SignalName.AnimationFinished);

				OpenShape.Disabled = true;
			}
		}
	}
}
