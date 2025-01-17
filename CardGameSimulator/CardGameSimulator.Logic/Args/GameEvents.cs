namespace CardGameSimulator.Logic.Args;

class CardEventArgs( Card? card ) : EventArgs
{
        public Card? Card => card;
}

class TurnEventArgs( Player? player , Card? revealedCard , TurnState? state ) : EventArgs
{
        public Player? Player { get; } = player;
        public Card? RevealedCard { get; } = revealedCard;
        public TurnState? State { get; } = state;
}

class GameEventArgs( GameState? state ) : EventArgs
{
        public GameState? GameState { get; }
                = state is GameState && state.HasValue
                ? state
                : throw new Exception( );
}

