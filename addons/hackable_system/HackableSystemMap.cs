using Godot;
using System;
using System.Linq;

[Tool]
public partial class HackableSystemMap : Control
{
	[Export] public string Data;

	[Export] public SystemResource systemResource;


	private const string NODE_SCENE_PATH = "addons/hackable_system/HackableSystemNode.tscn";
	private const string EDGE_SCENE_PATH = "addons/hackable_system/SystemEdge.tscn";

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
			var resource = (PackedScene)ResourceLoader.Load(NODE_SCENE_PATH);
			foreach (var nodeResource in systemResource.Nodes)
			{
				var instance = resource.Instantiate<Control>();

				instance.Position = nodeResource.Position;
				
				var node = (HackableSystemNode)instance;
				node.Program = nodeResource.Program;
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
