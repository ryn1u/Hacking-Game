using Godot;
using System;


public partial class TileMapController : TileMap
{
	public override void _Ready()
	{
		EventBus.Node.OnPingTileMap += OnPingTileMap;
	}

	public void OnPingTileMap(SignalEventArguments<TileMap> tileMapArgs)
	{
		tileMapArgs.Data = this;
	}
}
