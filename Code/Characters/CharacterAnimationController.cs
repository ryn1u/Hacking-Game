using Godot;
using HackingGame.Common;

namespace HackingGame.Characters
{
	public partial class CharacterAnimationController : AnimatedSprite2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Play();
		}

		public void PlayAnimation(string animation)
		{
			Play(animation);
		}

		public void OnPlayerStateChanged(CharacterState state, string property)
		{
			if(property == CharacterProperties.Direction || property == CharacterProperties.IsWalking)
			{
				string animationType = state.IsWalking ? "walk_" : "idle_";
				Play(animationType + state.Direction.Name());
			}
		}
	}
}
