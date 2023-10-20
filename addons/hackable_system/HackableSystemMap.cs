using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using HackingGame.Common;

[Tool]
public partial class HackableSystemMap : Control
{
	[Export] public string Data;

	[Export] public SystemResource systemResource;

	[Export] private PackedScene pointerScene;

	private const string INDICATOR_KEY = "node_pointer_indicator";
	private Dictionary<int, HackableSystemNode> nodes;

    public override void _Ready()
    {
		if(!Engine.IsEditorHint())
		{
			EventBus.Relay.Connect(EventsNames.RequestNodeAtPointerPosition, this.ToCall(MethodName.OnRequestNodeAtPointerPosition));
			EventBus.Relay.Connect(EventsNames.ToggleNodeIndicator, this.ToCall(MethodName.OnToggleNodeIndicator));
		}
    }

    public void OnToggleNodeIndicator(bool toggle)
	{
		if(toggle)
		{
			var args = new TempInstanceArgs(pointerScene);
			EventBus.Call(EventsNames.CreateTemporaryInstance, INDICATOR_KEY, args);
			AddChild(args.Instance);
		}
		else
		{
			EventBus.Call(EventsNames.DeleteTemporaryInstance, INDICATOR_KEY);
		}
	}

	public void OnRequestNodeAtPointerPosition(SignalEventArguments<HackableSystemNode> args)
	{
		var controller = ControllerRegistry.Get<IHackingGameplayStateController>();
		var nodePointerPosition = controller.GetNodePointerPosition();

		args.Data = nodes[nodePointerPosition];
	}


	// RESOURCE TOOLS
	private const string NODE_SCENE_PATH = "addons/hackable_system/HackableSystemNode.tscn";
	private const string EDGE_SCENE_PATH = "addons/hackable_system/SystemEdge.tscn";

	public void OnPCInterfaceInitialized(SystemResource system)
	{
		systemResource = system;
		Load();
	}

	public void AddNewNode()
	{
		GD.Print("Add new node");

		var resource = (PackedScene)ResourceLoader.Load(NODE_SCENE_PATH);
		var instance = resource.Instantiate<Control>();

		instance.Position = Size / 2 - instance.Size / 2;

		AddChild(instance);
		instance.Owner = Engine.IsEditorHint() ? GetTree().EditedSceneRoot : this;
	}

	public void AddNewEdge()
	{
		GD.Print("Add new edge");
		
		var resource = (PackedScene)ResourceLoader.Load(EDGE_SCENE_PATH);
		var instance = resource.Instantiate();
		var edge = (SystemEdge)instance;

		AddChild(edge);
		instance.Owner = Engine.IsEditorHint() ? GetTree().EditedSceneRoot : this;
	}

	public void Save()
	{
		if (systemResource is null)
		{
			GD.PushError("Set target resource");
		}
		else
		{
			systemResource.Nodes = new Godot.Collections.Array<NodeResource>();
			systemResource.Edges = new Godot.Collections.Array<Vector2I>();

			var children = GetChildren();
			foreach (var child in children)
			{
				if (child is HackableSystemNode node)
				{
					var nodeResource = new NodeResource(node.Position, node.Program);
					systemResource.Nodes.Add(nodeResource);
				}
				else if (child is SystemEdge edge)
				{
					var fromIdx = children.IndexOf(edge.from);
					var toIdx = children.IndexOf(edge.to);

					systemResource.Edges.Add(new Vector2I(fromIdx, toIdx));
				}
			}

			ResourceSaver.Save(systemResource, systemResource.ResourcePath, ResourceSaver.SaverFlags.ReplaceSubresourcePaths);
			GD.Print($"Saved System to Resource {systemResource.ResourcePath}");
		}
	}

	public void Load()
	{
		if (systemResource is not null)
		{
			nodes = new Dictionary<int, HackableSystemNode>();

			var resource = (PackedScene)ResourceLoader.Load(NODE_SCENE_PATH);
			foreach (var nodeResource in systemResource.Nodes)
			{
				var instance = resource.Instantiate<Control>();

				instance.Position = nodeResource.Position;
				
				var node = (HackableSystemNode)instance;
				node.Program = nodeResource.Program;
				node.Id = nodes.Count;
				nodes.Add(node.Id, node);
				//node.ConnectedObject = nodeResource.ConnectedNode;

				AddChild(instance);
				instance.Owner = Engine.IsEditorHint() ? GetTree().EditedSceneRoot : this;

			}

			var children = GetChildren();
			resource = (PackedScene)ResourceLoader.Load(EDGE_SCENE_PATH);
			
			foreach(var edgeVec in systemResource.Edges)
			{
                var instance = resource.Instantiate();
				var edge = (SystemEdge)instance;

                AddChild(edge);
				instance.Owner = Engine.IsEditorHint() ? GetTree().EditedSceneRoot : this;

				edge.from = (HackableSystemNode)children[edgeVec.X];
				edge.to = (HackableSystemNode)children[edgeVec.Y];
			}
		}
	}
}
