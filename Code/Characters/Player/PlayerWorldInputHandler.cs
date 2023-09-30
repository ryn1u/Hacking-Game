using Godot;
using System;

using HackingGame.Common;


namespace HackingGame.Characters.Player
{
	public partial class PlayerWorldInputHandler : Node, IInputHandler
	{
		[Signal] public delegate void OnInputDirectionChangedEventHandler(int direction);
		[Signal] public delegate void OnInputStopEventHandler();
		[Signal] public delegate void OnInputInteractEventHandler();

		private MovementStack stack;
		private readonly Direction[] directions = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
		private Func<InputEvent, bool>[] funcs;
		[Export] private bool DebugStack = false;


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			stack = new MovementStack();
			funcs = new Func<InputEvent, bool>[] { HandleInteractionInput, HandleDirectionInput };
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(stack.HasVaule)
			{
				var direction = stack.Top;
				EmitSignal(SignalName.OnInputDirectionChanged, (int)direction);
			}
			if(!stack.HasVaule)
			{
				EmitSignal(SignalName.OnInputStop);
			}
			stack.Update(DebugStack);
		}

		public bool HandleInput(InputEvent @event)
		{
			foreach(var func in funcs)
			{
				if(func(@event))
				{
					return true;
				}
			}
			return false;
		}

		private bool HandleInteractionInput(InputEvent @event)
		{
			if(@event.IsAction("interact") && @event.IsPressed())
			{
				EmitSignal(SignalName.OnInputInteract);
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool HandleDirectionInput(InputEvent @event)
		{
			for (int i = 0; i < directions.Length; i++)
			{
				var name = directions[i].Name().ToLower();

				if(@event.IsAction(name))
				{
					if(@event.IsPressed())
					{
						stack.Push(directions[i]);
					}
					else if(@event.IsReleased())
					{
						stack.Remove(directions[i]);
					}
					return true;
				}
			}
			return false;
		}
    }
}