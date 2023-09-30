using Godot;
using System;

public partial class Program : Resource
{
    [Export] public string ProgramName { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }

    [Signal] public delegate void ProgramBehaviourEventHandler();
}
