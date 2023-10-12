using Godot;
using System;

[Tool]
public partial class SystemEdge : Line2D
{
	[Export] public HackableSystemNode from;
	[Export] public HackableSystemNode to;

	private const double INTERVAL = 1 / 20;
	private double counter;

    public override void _Ready()
    {
		counter = 0;
		if(Points.Length != 2)
		{
			Points = new Vector2[2];
		}
    }

    public override void _Process(double delta)
    {
		if(from is null || to is null) return;

		if(counter <= 0)
		{
			SetPointPosition(0, from.Position + from.Size / 2);
			SetPointPosition(1, to.Position + to.Size / 2);

			counter = INTERVAL;
		}
		counter -= delta;
    }
}
