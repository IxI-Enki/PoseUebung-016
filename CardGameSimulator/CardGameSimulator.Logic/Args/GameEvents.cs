namespace CardGameSimulator.Logic.Args;


class GameEvents : EventArgs { }

class CardEventArgs( Card? card ) : GameEvents
{
        public Card? Card => card;
}

class PlayerEventArgs( Player? currentPlayer ) : GameEvents
{
        public Player? CurrentPlayer => currentPlayer;

}

class TurnEventArgs( Card? revealedCard , TurnState? state ) : GameEvents
{
        public Card? RevealedCard => revealedCard;
        public TurnState? State => state;
}

class GameEventArgs( GameState? state ) : GameEvents
{
        public GameState? GameState { get; }
                = state is GameState && state.HasValue
                ? state
                : throw new Exception( );
}

class ControllerArgs( IGameController? gameController ) : GameEvents
{
        public IGameController? GameController => gameController;
}