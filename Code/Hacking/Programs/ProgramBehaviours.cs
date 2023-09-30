using Godot;
using System;

[Tool]
public partial class ProgramBehaviours : Node
{
	[Export] public Godot.Collections.Dictionary<string, Godot.GodotObject> ProgramBehavioursRegistry { get; set; }

	private void ExecuteMvptr()
	{
		GD.Print("test program behaviour");
	}

	public override void _Ready()
	{
		if(Engine.IsEditorHint())
		{
			if(ProgramBehavioursRegistry is null)
			{
				ProgramBehavioursRegistry = new Godot.Collections.Dictionary<string, Godot.GodotObject>();
			}

			SearchForMissingMembers();
		}
		else
		{
			SubscribeProgramsToSignals();
		}
	}

	private void SearchForMissingMembers()
	{
		GD.Print("ProgramBehavioursRegistry: Checking for missing fields:");
		var fields =  typeof(ProgramBehaviours.MethodName).GetFields();
		foreach(var field in fields)
		{
			var fieldName = field.Name;
			if(fieldName.Contains("Execute"))
			{
				GD.Print($"Adding missing key");
				if(!ProgramBehavioursRegistry.ContainsKey(fieldName))
				{
					ProgramBehavioursRegistry.Add(fieldName, null);
				}
			}
		}
	}

	private void SubscribeProgramsToSignals()
	{
		foreach(var pair in ProgramBehavioursRegistry)
		{
			var method = pair.Key;
			var program = pair.Value;
			if(program is not null)
			{
				program.Connect(Program.SignalName.ProgramBehaviour, new Callable(this, method));
			}
		}
	}
}
