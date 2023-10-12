using Godot;
using System;


public partial class TileMapController : TileMap
{
	public override void _Ready()
	{
		EventBus.Relay.Connect(EventsNames.OnPingTileMap, this.ToCall(MethodName.OnPingTileMap));
	}

	public void OnPingTileMap(SignalEventArguments<TileMap> tileMapArgs)
	{
		tileMapArgs.Data = this;
	}
}
