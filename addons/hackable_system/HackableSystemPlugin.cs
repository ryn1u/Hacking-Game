#if TOOLS
using Godot;
using System;

[Tool]
public partial class HackableSystemPlugin : EditorPlugin
{
	private HackableSystemEditorInspectorPlugin plugin;
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
        var script = GD.Load<Script>("res://addons/hackable_system/HackableSystemMap.cs");
        var texture = GD.Load<Texture2D>("res://Assets/Graphics/icons/1bit_icons_pc_1.png");
        AddCustomType("HackableSystemMap", "Resource", script, texture);

		
        script = GD.Load<Script>("res://addons/hackable_system/HackableSystemNode.cs");
        texture = GD.Load<Texture2D>("res://Assets/Graphics/icons/1bit_icons_border.png");
        AddCustomType("HackableSystemNode", "Resource", script, texture);


		plugin = new HackableSystemEditorInspectorPlugin();
		AddInspectorPlugin(plugin);
	}

	public override void _ExitTree()
	{
        RemoveCustomType("HackableSystemMap");
        RemoveCustomType("HackableSystemNode");
		RemoveInspectorPlugin(plugin);
	}
}
#endif
