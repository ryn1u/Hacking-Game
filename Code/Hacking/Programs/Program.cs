using Godot;
using System;
using HackingGame.Hacking;

[GlobalClass]
[Tool]
public partial class Program : Resource
{
    [Export] public string ProgramName { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    [Export(PropertyHint.Range, "0,65535,")] public int Addr1;
    [Export(PropertyHint.Range, "0,255,")] public int Addr2;

    [Signal] public delegate void ProgramBehaviourEventHandler(ProgramCallEventArgs args);
}
