using Godot;
using System;
using HackingGame.Common;

namespace HackingGame.Characters
{
	public partial class CharacterPhysicsController : CharacterBody2D
	{
		[Export] private bool IsWalking = false;
		[Export] private float speed = 300;

		public void OnPlayerStateChanged(CharacterState state, string property)
		{
			if(property == CharacterProperties.Direction)
			{
				Velocity = state.Direction.ToVec2() * speed;
			}
			else if(property == CharacterProperties.IsWalking)
			{
				IsWalking = state.IsWalking;
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			if(IsWalking)
			{
				MoveAndSlide();
			}
		}
	}
}
