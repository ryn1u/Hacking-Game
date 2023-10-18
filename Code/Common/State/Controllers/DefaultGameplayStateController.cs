using System;
using System.Collections.Generic;
using Godot;

namespace HackingGame.Common.Controllers
{
    public partial class DefaultGameplayStateController : Node, IGameplayStateController
    {
        [Export] private NodePath gameplayStatePath;
        private GameplayState gameplayState {get; set; }

        public Type ControllerInterface => typeof(IGameplayStateController);

        public override void _Ready()
        {
            gameplayState = GetNode<GameplayState>(gameplayStatePath);
        }

        public void AddProgramToInventory(Program program)
        {
            gameplayState.Programs.Add(program); // TODO FIX this
        }

        public void SetGameplayMode(GameplayMode mode)
        {
            gameplayState.SetGameplayMode(mode);
        }

        public GameplayMode GetGameplayMode()
        {
            return gameplayState.GameplayMode;
        }

        public int GetInventoryProgramCount()
        {
            return gameplayState.Programs.Count;
        }

        public IEnumerable<Program> GetInventoryProgramsEnumerable()
        {
            var enumerator = gameplayState.Programs.GetEnumerator();
            while(enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public Program GetProgramAtIndex(int idx)
        {
            return gameplayState.Programs[idx];
        }
    }
}