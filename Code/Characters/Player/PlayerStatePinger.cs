using Godot;

namespace HackingGame.Characters.Player
{
	public partial class PlayerStatePinger : Node
	{
		[Export] CharacterState state;

        public override void _Ready()
        {
            if(state is null)
			{
				state = GetNode<CharacterState>("../PlayerState");
			}

			EventBus.Node.Connect(EventsNames.OnPingPlayerState, this.ToCall(MethodName.OnPingPlayerState));
        }

		public void OnPingPlayerState(SignalEventArguments<CharacterState> args)
		{
			args.Data = state;
		}
	}
}
