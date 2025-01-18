namespace CardGameSimulator.Logic.Contracts;

interface IGameController
{
        Card? RevealedCard { get; set; }
        Game? Game { get; internal set; }

        Deck? PlayCards { get; internal set; }
        event EventHandler<GameEvents>? CardDrawn;
        void DrawCard( );
        void OnCardDrawn( GameEvents state );

        List<Player>? AllPlayers { get; internal set; }
        Player? CurrentPlayer { get; set; }
        event EventHandler<GameEvents>? PlayerChanged;
        void ChangeCurrentPlayer();
        void OnPlayerChanged( GameEvents state );


        TurnState? TurnState { get; set; }
        event EventHandler<GameEvents>? TurnStateChanged;
        void ChangeTurnState( );
        void OnTurnStateChange( GameEvents state );


        GameState? State { get; internal set; }
        event EventHandler<GameEvents>? GameStateChanged;
        void ChangeGameState( );
        void OnGameStateChange( GameEvents state );
}
