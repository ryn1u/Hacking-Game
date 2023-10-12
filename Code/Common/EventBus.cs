global using EventsNames = EventBus.SignalName;

using Godot;
using System.Linq;

using HackingGame.Common;
using HackingGame.Characters;
using HackingGame.Characters.Player;

public partial class EventBus : Node
{
	public static EventBus Relay;

	// PINGS
	[Signal] public delegate void OnPingTileMapEventHandler(SignalEventArguments<TileMap> tileMapArgs);

	// TempInstanceManager
	[Signal] public delegate void CreateTemporaryInstanceEventHandler(string id, TempInstanceArgs args);
	[Signal] public delegate void DeleteTemporaryInstanceEventHandler(string id);

	// HACKING
	[Signal] public delegate void PlayerStartedHackingEventHandler(SystemResource system);
	[Signal] public delegate void PlayerStoppedHackingEventHandler();

	// INPUT
	[Signal] public delegate void PlayerChangeControlsEventHandler(PlayerInputController.InputMode mode);

	// GAME STATE
	[Signal] public delegate void OnGameplayStateChangedEventHandler(GameplayState state, string property);

    public override void _Ready()
    {
        Relay = this;
    }

	public static void Call(string name, params Variant[] args)
	{
		GD.Print($"Event Bus Call: {name} -> {string.Join(' ', args.Select(x => x.ToString()))}");
		Relay.EmitSignal(name, args);
	}

	public static void Register(string name, Callable callable)
	{
		Relay.Connect(name, callable);
	}
}

public static class CallableExtensions
{
	public static Callable ToCall(this Node gObj, string method)
	{
		return new Callable(gObj, method);
	}
}