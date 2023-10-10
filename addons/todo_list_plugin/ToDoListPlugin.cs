#if TOOLS
using Godot;
using System;

namespace ToDoPlugin
{
	[Tool]
	public partial class ToDoListPlugin : EditorPlugin
	{
		Control dock;

		public override void _EnterTree()
		{
			dock = (Control)GD.Load<PackedScene>("addons/todo_list_plugin/TO DO.tscn").Instantiate();
			AddControlToDock(DockSlot.LeftBr, dock);
		}

		public override void _ExitTree()
		{
			RemoveControlFromDocks(dock);
			dock.Free();
		}
	}
}
#endif
