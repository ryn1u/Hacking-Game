using Godot;
using System;

public partial class PCInterfaceController : CanvasLayer
{
	public void InitializePCInterface(SystemResource system)
	{
		GD.Print($"zinicjalizowano chuja o nazwie: {system.ResourceName}");
	}

    public override void _Ready()
    {

    }
}
