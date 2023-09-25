using System;
using Godot;
using System.Collections.Generic;
using System.Linq;
using HackingGame.Common;

namespace HackingGame.Characters.Player
{
    public class MovementStack
    {
        // This class implements a special kind of stack that puches direction vectors always at the begining, but removes vectors wherever they are.
        // This is used to keep track of latest pressed input comands and returning back to previous ones after r

        private List<MoveCommand> stack;

        public bool HasVaule => stack.Count > 0;
        public Direction Top
        {
            get
            {
                return stack[0].direction;
            }
        }

        public MovementStack()
        {
            stack = new List<MoveCommand>();
        }

        public void Push(Direction direction)
        {
            MoveCommand command = new MoveCommand(direction);
            if(stack.All(x => x.direction != direction))
            {
                stack.Insert(0, command);
            }
        }

        public void Remove(Direction direction)
        {
            for(int i = 0; i < stack.Count; i++)
            {
                if (stack[i].direction == direction)
                {
                    var command = stack[i];
                    command.Remove();
                    stack[i] = command;
                }
            }
        }

        public void DebugStack()
        {
            GD.Print($"STACK:{String.Join(", ", stack.Select(x => $"[{x.direction}({x.toRemove})]"))}");
        }

        public void Update(bool debug=false)
        {
            for(int i = stack.Count - 1; i >= 0; i--)
            {
                if ((stack[i].toRemove) || i > 2)
                {
                    stack.RemoveAt(i);
                }
            }
            if(debug) DebugStack();
        }

        public struct MoveCommand
        {
            public MoveCommand(Direction direction)
            {
                this.direction = direction;
                toRemove = false;
            }

            public Direction direction;
            public bool toRemove;

            public void Remove()
            {
                toRemove= true;
            }

            public void Debug()
            {

            }
        }
    }
}