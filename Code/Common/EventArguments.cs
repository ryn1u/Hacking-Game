using Godot;

public partial class SignalEventArguments<T> : RefCounted where T : GodotObject
{
    public T Data {get; set;}

    public static T Call(string signal)
    {
        SignalEventArguments<T> args = new();
        EventBus.Call(signal, args);
        return args.Data;
    }
}