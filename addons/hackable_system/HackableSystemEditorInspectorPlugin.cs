using Godot;
using HackingGame.Hacking.Systems;
using System;

public partial class HackableSystemEditorInspectorPlugin : EditorInspectorPlugin
{
    private NodePath editorInspectorScene = "addons/hackable_system/controls/SystemEditorPropertyControls.tscn";

    public override bool _CanHandle(GodotObject @object)
    {
        if(@object is HackableSystemEditorController)
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
        if(@object is HackableSystemEditorController controller)
        {
            var scene = (PackedScene)ResourceLoader.Load(editorInspectorScene);
            var control = scene.Instantiate<Control>();

            AddCustomControl(control);

            var newCallback = new Callable(controller, HackableSystemEditorController.MethodName.OnNewNodeButtonPressed);
            control.Connect(SystemEditorPropertyControls.SignalName.NewNodeButtonPressed, newCallback);

            var saveCallback = new Callable(controller, HackableSystemEditorController.MethodName.OnSaveResourceButtonPressed);
            control.Connect(SystemEditorPropertyControls.SignalName.SaveResourceButtonPressed, saveCallback);
        }
    }
}
