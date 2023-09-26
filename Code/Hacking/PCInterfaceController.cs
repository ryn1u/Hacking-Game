using Godot;
using System;

public partial class PCInterfaceController : CanvasLayer
{
	public void InitializePCInterface(HackableSystemMap system)
	{
		GD.Print($"zinicjalizowano chuja o nazwie: {system.Data}");
	}
}
