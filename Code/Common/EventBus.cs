global using EventsNames = EventBus.SignalName;

using Godot;
using System.Linq;

using HackingGame.Characters;
using HackingGame.Characters.Player;

public partial class EventBus : Node
{
	public static EventBus Node;

	// PINGS
	[Signal] public delegate void OnPingTileMapEventHandler(SignalEventArguments<TileMap> tileMapArgs);
	[Signal] public delegate void OnPingPlayerStateEventHandler(SignalEventArguments<CharacterState> stateArgs);

	// TempInstanceManager
	[Signal] public delegate void CreateTemporaryInstanceEventHandler(string id, TempInstanceArgs args);
	[Signal] public delegate void DeleteTemporaryInstanceEventHandler(string id);

	// HACKING
	[Signal] public delegate void PlayerStartHackingEventHandler(HackableSystemMap system);

	// INPUT
	[Signal] public delegate void PlayerChangeControlsEventHandler(PlayerInputController.InputMode mode);

    public override void _Ready()
    {
        Node = this;
    }

	public static void Call(string name, params Variant[] args)
	{
		GD.Print($"Event Bus Call: {name} -> {string.Join(' ', args.Select(x => x.ToString()))}");
		Node.EmitSignal(name, args);
	}

	public static void Register(string name, Callable callable)
	{
		Node.Connect(name, callable);
	}
}

public static class CallableExtensions
{
	public static Callable ToCall(this Node gObj, string method)
	{
		return new Callable(gObj, method);
	}
}