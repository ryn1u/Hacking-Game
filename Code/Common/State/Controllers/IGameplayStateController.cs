using System;
using System.Collections.Generic;
using Godot;
using HackingGame.Common.Controllers;

namespace HackingGame.Common
{
    public interface IGameplayStateController : IModelController
    {
        public void SetIsHacking(bool value);
        public bool GetIsHacking();

        public void AddProgramToInventory(Program program);
        public int GetInventoryProgramCount();
        public Program GetProgramAtIndex(int idx);

        public IEnumerable<Program> GetInventoryProgramsEnumerable();
    }
}