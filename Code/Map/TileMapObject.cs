using Godot;
using System;

/// <summary>A node that snaps its self to TilmapGrid</summary>
[Tool]
public partial class TileMapObject : Node2D
{
	[Signal] public delegate void OnAreaEnteredEventHandler(Area2D area);
	[Signal] public delegate void OnAreaExitedEventHandler(Area2D area);

	private TileMap tileMap;
	private TileMap TileMap
	{
		get
		{
			if(tileMap is null)
			{
				try
				{
					tileMap = Owner.GetParent<TileMap>();
				}
				catch
				{
					tileMap = null;
				}
			}
			return tileMap;
		}
	}

	private Node2D owner;
	private Node2D Owner2D
	{
		get
		{
			if(owner is null)
			{
				Node2D node2D = Owner as Node2D;
				if(node2D is null)
				{
					node2D = this;
				}
				owner = node2D;
			}
			return owner;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Engine.IsEditorHint() && TileMap is not null)
		{
			var pos = Owner2D.Position;
			var cell = TileMap.LocalToMap(pos);
			Owner2D.Position = TileMap.MapToLocal(cell);
		}
	}

	public void RelayOnAreaEntered(Area2D area)
	{
		EmitSignal(SignalName.OnAreaEntered, area);
	}
	public void RelayOnAreaExited(Area2D area)
	{
		EmitSignal(SignalName.OnAreaExited, area);
	}
}
