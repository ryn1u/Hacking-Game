using Godot;
using System;

public partial class PCInterfaceController : CanvasLayer
{
	[Signal] public delegate void OnPCInterfaceInitializedEventHandler(SystemResource system);

	public void InitializePCInterface(SystemResource system)
	{
		EmitSignal(SignalName.OnPCInterfaceInitialized, system);
	}
}
