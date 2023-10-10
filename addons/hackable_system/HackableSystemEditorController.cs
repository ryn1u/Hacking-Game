using Godot;
using System;

namespace HackingGame.Hacking.Systems
{
	[Tool]
	public partial class HackableSystemEditorController : Node
	{
		[Export] private HackableSystemMap Map { get; set; }

		[ExportCategory("Properties")]
		[Export] private PackedScene SystemNodeScene;
		[Export] private Control nodesContainer;

		public void OnNewNodeButtonPressed()
		{
			if(Map is null)
			{
				GD.Print("Set Map property before adding node!");
			}
			else
			{
				var instance = SystemNodeScene.Instantiate<SystemNodeScene>();
				nodesContainer.AddChild(instance);
				instance.Owner = GetTree().EditedSceneRoot;
				instance.Position = nodesContainer.Size / 2 - instance.Size / 2;

				var node = new HackableSystemNode() { Position = instance.Position };

				Map.Nodes.Add(node);
				GD.Print(string.Join(" ", Map.Nodes));

				ResourceSaver.Save(Map, flags: ResourceSaver.SaverFlags.BundleResources);
				
				instance.SystemNode = node;
				instance.RegisterDeleteEvent(() => Map.Nodes.Remove(node));
			}
		}

		public void OnSaveResourceButtonPressed()
		{
			if(Map is null)
			{
				GD.PushError("Set Map property before saving!");
				return;
			}
			else
			{

			}

		}
	}
}