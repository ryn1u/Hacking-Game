using Godot;
using HackingGame.Hacking.Systems;
using System;

[Tool]
public partial class SystemNodeScene : MarginContainer
{
  [Export] public HackableSystemNode SystemNode { get; set; }

  public override void _Process(double delta)
  {
    if (SystemNode is not null) SystemNode.Position = Position;
  }

  private event Action OnDeleteNodeEvent;

  public void RegisterDeleteEvent(Action action)
  {
    OnDeleteNodeEvent += action;
  }

  public override void _ExitTree()
  {
    OnDeleteNodeEvent?.Invoke();
  }
}
