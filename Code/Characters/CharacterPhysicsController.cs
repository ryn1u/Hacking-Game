using Godot;
using System;
using HackingGame.Common;

namespace HackingGame.Characters
{
	public partial class CharacterPhysicsController : CharacterBody2D
	{
		[Export] private CharacterState state;
		[Export] private float speed = 300;

		public void OnPlayerStateChanged(CharacterState state, string property)
		{
			Velocity = state.Direction.ToVec2() * speed;
		}

		public override void _PhysicsProcess(double delta)
		{
			if(state.IsWalking)
			{
				MoveAndSlide();
			}
		}
	}
}
