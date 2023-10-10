using Godot;
using System;
using ToDoPlugin;

[Tool]
public partial class ToDoListController : VBoxContainer
{
	[Export] private LineEdit lineEdit;
	[Export] private Control container;
	[Export] private PackedScene listElementScene;

	public void OnAddButtonPressed()
	{
		var text = lineEdit.Text;
		lineEdit.Text = "";

		var instance = listElementScene.Instantiate();

		Callable callback = new Callable(this, MethodName.OnDeleteButtonPressed);
		instance.Connect(ListElement.SignalName.OnDeleteButtonPressed, callback);

		container.AddChild(instance);
		GD.Print(instance.GetClass());
		instance.Call(ListElement.MethodName.Initialize, text, false);
	}

	public void OnDeleteButtonPressed(Node node)
	{
		container.RemoveChild(node);
		node.QueueFree();
	}
}
