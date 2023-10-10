using Godot;

[Tool]
public partial class SceneRoot : Node2D
{
	public static void Log(string msg)
	{
		GD.Print(msg);
	}
}
