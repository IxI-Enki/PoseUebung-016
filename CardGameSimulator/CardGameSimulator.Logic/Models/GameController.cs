

namespace CardGameSimulator.Logic.Models;

class GameController : IGameController
{

        public Player? CurrentPlayer
        {
                get;
                set;
        }
        public Card? RevealedCard
        {
                get;
                set;
        }

        private TurnState? _turnState;
        public TurnState? TurnState
        {
                get => _turnState;
                set => _turnState = value;
        }

        public Game? Game
        {
                get;
                set;
        }
        private Deck? _deck;
        public Deck? PlayCards
        {
                get => _deck;
                set => _deck = value;
        }
        private List<Player>? _players;
        public List<Player>? AllPlayers
        {
                get => _players;
                set => _players = value;
        }

        public GameState? State
        {
                get => _state;
                set => _state = value;
        }
        private GameState? _state;

        public GameController( Game game )
        {
                Game = game;
                PlayCards = DeckFactory.CreateDeck( );
                
                AllPlayers = PlayerFactory.SetPlayers( );
        }

        public event EventHandler<GameEvents>? TurnStateChanged;
        public event EventHandler<GameEvents>? GameStateChanged;
        public event EventHandler<GameEvents>? CardDrawn;
        public event EventHandler<GameEvents>? PlayerChanged;

        public void ChangeGameState( )
        {
                throw new NotImplementedException( );
        }

        public void ChangeTurnState( ) => OnTurnStateChange( new TurnEventArgs( RevealedCard , TurnState ) );

        public void OnGameStateChange( GameEvents state )
        {
                throw new NotImplementedException( );
        }

        public void OnTurnStateChange( GameEvents state )
        {
                if(state is TurnEventArgs t)
                        TurnStateChanged?.Invoke( this , t );
                else
                        throw new Exception( );
        }

        public void DrawCard( )
        {
                throw new NotImplementedException( );
        }

        public void OnCardDrawn( GameEvents state )
        {
                {
                        if(state is CardEventArgs c)
                                CardDrawn?.Invoke( this , c );
                        else
                                throw new Exception( );
                }
        }

        public void ChangeCurrentPlayer( )
        {
                throw new NotImplementedException( );
        }

        public void OnPlayerChanged( GameEvents state )
        {
                {
                        if(state is PlayerEventArgs p)
                                PlayerChanged?.Invoke( this , p );
                        else
                                throw new Exception( );
                }
        }
}
