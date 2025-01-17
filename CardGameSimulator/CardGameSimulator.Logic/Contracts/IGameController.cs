using CardGameSimulator.Logic.Args;

namespace CardGameSimulator.Logic.Contracts;

interface IGameController
{
        IGame? Game { get; }
        IDeck? PlayCards { get; }

        List<Player>? AllPlayers { get; }
        Player? CurrentPlayer { get; set; }
        ICard? RevealedCard { get; set; }


        TurnState? TurnState { get; }
        event EventHandler<TurnEventArgs>? TurnStateChanged;
        void ChangeTurnState( );
        void OnTurnStateChange( );


        GameState? State { get; }
        event EventHandler<GameEventArgs>? GameStateChanged;
        void ChangeGameState( );
        void OnGameStateChange( GameEventArgs state );
}
