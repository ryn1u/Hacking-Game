using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages temporary object instances for other Nodes.
///	Instances are assigned to owners and can be assigned an id. An id key is used to allow selection which instances are deleted.
/// Passing an ID "all" deletes all instances assigned to owner. Any other value deletes instances with provided ID.
///	</summary>
public partial class TempInstanceManager : Node
{
	private Dictionary<string, Node> registry;

    public override void _Ready()
    {
        registry = new Dictionary<string, Node>();

		EventBus.Node.CreateTemporaryInstance += OnCreateTemporaryInstance;
		EventBus.Node.DeleteTemporaryInstance += OnDeleteTemporaryInstance;
    }

	public void OnCreateTemporaryInstance(string id, TempInstanceArgs args)
	{
		args.Instance = args.Scene.Instantiate();
		registry[id] = args.Instance;
	}

	public void OnDeleteTemporaryInstance(string id)
	{
		if(registry.TryGetValue(id, out var node))
		{
			node.QueueFree();
		}
	}
}

public partial class TempInstanceArgs : RefCounted
{
	public PackedScene Scene { get; set; }
	public Node Instance { get; set; }
}
