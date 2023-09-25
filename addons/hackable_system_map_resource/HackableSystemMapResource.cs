#if TOOLS
using Godot;
using System;

[Tool]
public partial class HackableSystemMapResource : EditorPlugin
{
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
        var script = GD.Load<Script>("res://addons/hackable_system_map_resource/HackableSystemMap.cs");
        var texture = GD.Load<Texture2D>("res://Assets/Graphics/icons/1bit_icons_pc_1.png");
        AddCustomType("HackableSystemMap", "Resource", script, texture);
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
        RemoveCustomType("HackableSystemMap");
	}
}
#endif
