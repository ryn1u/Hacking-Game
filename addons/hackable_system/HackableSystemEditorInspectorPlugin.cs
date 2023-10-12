using Godot;
using HackingGame.Hacking.Systems;
using System;
using System.Collections.Generic;

public partial class HackableSystemEditorInspectorPlugin : EditorInspectorPlugin
{
    private NodePath editorInspectorScene = "addons/hackable_system/controls/SystemEditorPropertyControls.tscn";

    public override bool _CanHandle(GodotObject @object)
    {
        if(@object is HackableSystemMap)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void _ParseBegin(GodotObject @object)
    {
        if(@object is HackableSystemMap map)
        {
            var scene = (PackedScene)ResourceLoader.Load(editorInspectorScene);
            var control = scene.Instantiate<Control>();

            AddCustomControl(control);

            var connections = new Dictionary<string, string>()
            {
                { HackableSystemMap.MethodName.AddNewNode, SystemEditorPropertyControls.SignalName.NewNodeButtonPressed },
                { HackableSystemMap.MethodName.AddNewEdge, SystemEditorPropertyControls.SignalName.NewEdgeButtonPressed },
                { HackableSystemMap.MethodName.Save, SystemEditorPropertyControls.SignalName.SaveButtonPressed },
                { HackableSystemMap.MethodName.Load, SystemEditorPropertyControls.SignalName.LoadButtonPressed }
            };

            foreach(var connection in connections)
            {
                var callback = new Callable(map, connection.Key);
                control.Connect(connection.Value, callback);
            }
        }
    }
}
