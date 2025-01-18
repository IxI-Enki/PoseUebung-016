namespace CardGameSimulator.ConApp;

internal class Program
{
        static void Main( )
        {
                Console.SetWindowSize( 60 , 14 );

                GameController game = new( );

                Console.ReadLine( );
        }
}

class GameController
{
        public DeckController? Deck { get; set; }
        public PlayerController? Players { get; private set; }
        public StateController? State { get; set; }
        public PrintController? Print { get; set; }
        public bool PrintDebug { get; set; }
        public event EventHandler? Updated;
        public GameController( bool printDebug = false )
        {
                PrintDebug = printDebug;

                State = StateController.Instance;
                State!.Updated += StateControllerUpdated!;

                Print = new( this );
                Print!.Updated += PrintControllerUpdated!;
                Print!.DisplayCurrent( );

                Deck = new( this );
                Console.WriteLine( Deck.DeckArgs( ) );
                Thread.Sleep( 500 );

                State.AdvanceState( );

                Deck.Deck.Shuffle( );
                Console.WriteLine( Deck.DeckArgs( ) );
                Thread.Sleep( 500 );

                State.AdvanceState( );

                Players = new( this );
                Players.GetPlayerAmount( );

                State.AdvanceState( );

                Players.Shuffle( );

                Console.WriteLine( Players );
                Console.ReadKey( );

                State.AdvanceState( );

                Players.HandOut( );

                State.AdvanceState( );

                Deck.Deck.Reveal( );
                Console.WriteLine( Deck.DeckArgs( ) );
                Console.ReadKey( );

                State.AdvanceState( );

                this.Run( );
        }
        public void Run( )
        {
                int numberOfStates = StateController.GameStates.Length;

                bool run = true;
                while(run)
                {
                        Console.Clear( );
                        PrintLoop( );
                        ChoiceLoop( );
                        if(Players!.CurrentPlayer!.Hand!.Count == 0)
                                run = false;
                        if(run)
                                Players!.Next( );
                }
                Console.WriteLine( "\n\n\t Gewonnen:" + Players!.CurrentPlayer );


        }
        private void ChoiceLoop( )
        {
                var input = Console.ReadLine( );


                if(Players!.ValidChoices! != null && Players.ValidChoices.Contains( input!.ToString( ) ))
                {
                        _ = Int32.TryParse( input , out int i );

                        Card toPlay = Players.PlayAbleCards( )!.ElementAt( i );
                        Console.Write( $"\n\t -> Karte: {toPlay} abgelegt" );

                        Players.PlayCard( toPlay );
                        Thread.Sleep( 500 );
                }

                if(Players!.ValidChoices! == null || !Players.ValidChoices.Contains( input!.ToString( ) ))
                {
                        Console.Write( "\n\t [Eingabe] -> Karte heben" );
                        Players.DrawCard( );
                        Thread.Sleep( 500 );
                }
        }
        private void PrintLoop( )
        {
                Console.WriteLine( new String( '-' , 60 ) );
                Console.WriteLine( $" Aufgedeckte Karte : {Deck!.Deck.Revealed} \n" );
                Console.WriteLine( new String( '-' , 60 ) );
                Console.WriteLine( "     Turn of: " );
                Console.WriteLine( $"    {Players!.AllPlayers![ Players.LastPlayerIndex ]}              " );
                Console.WriteLine( $"    - Hand      : {Players!.AllPlayers![ Players.LastPlayerIndex ].PrintHand( )}" );
                Console.WriteLine( new String( '.' , 60 ).ForegroundColor( "30,30,30" ) );
                Console.WriteLine( $"      unspielbar: {Players.UnPlayableCardsString( )}".ForegroundColor( "30,30,30" ) );
                Console.WriteLine( new String( '.' , 60 ).ForegroundColor( "30,30,30" ) );
                Console.WriteLine( new String( '-' , 60 ) );
                Console.WriteLine( $"      spielbar  : {Players.PlayableCardsString( )}" );
                Console.WriteLine( $"      press     : {Players.Legend}" );
                Console.WriteLine( new String( '_' , 60 ) );
                Console.Write( $"  draw:[enter]  :  " );


        }
        public void StateControllerUpdated( object controller , StateArg state )
        {
                if(PrintDebug)
                        Console.WriteLine( "  State-Controller updated\n".ForegroundColor( "green" ) +
                                          $"    - current State: - {state}\n" );

                Updated?.Invoke( controller , state );
        }
        public void PrintControllerUpdated( object controller , PrintArg state )
        {
                if(PrintDebug)
                        Console.WriteLine( "  Print-Controller updated\n".ForegroundColor( "green" ) +
                                          $"    - current State: - {state}\n" );

                Console.WriteLine( state.ToPrint );

                State!.AdvanceState( );

        }
        public void DeckControllerUpdated( object controller , DeckArg state )
        {
                if(PrintDebug)
                        Console.WriteLine( "  Deck-Controller updated\n".ForegroundColor( "green" ) +
                                          $"    - current State: - {state}\n" );

        }
        //public void PlayerControllerUpdated( object controller , PlayerArg state )
        //{
        //        if(PrintDebug)
        //                Console.WriteLine( "  Player-Controller updated\n".ForegroundColor( "green" ) +
        //                                  $"    - current State: - {state}\n" );
        //
        //}
}
class Card( uint n , Color c )
{
        public uint N { get; set; } = ( uint )n <= 9 ? n : 0;
        public Color C { get; set; } = ( Color )c;
        public override string ToString( ) => $"{N}".BackgroundColor( ColorCardOutput( ) ).ForegroundColor( "0,0,0" );
        public string ColorCardOutput( )
        => C switch
        {
                Color.Yellow => "255,255,80",
                Color.Green => "80,255,80",
                Color.Blue => "80,80,255",
                Color.Red => "255,80,80",
                _ => ""
        };
        internal static class Factory
        {
                public static Card Create( uint n , Color c ) => new( n , c );
                public static List<Card> Create( Color[] colors , uint amountOfCards = 1 )
                {
                        List<Card>? cards = [];

                        while(amountOfCards >= 1)
                                for(uint i = 0 ; i < colors.Length ; i++)
                                {
                                        for(uint j = 0 ; j < 10 ; j++)
                                        {
                                                Card c = new( j , colors[ i ] );
                                                //  Console.Write( $"Karte: {c} " + "erstellt\n".ForegroundColor( "70,70,70" ) );
                                                cards.Add( c );
                                                amountOfCards--;
                                        }
                                }
                        return cards;
                }
        }
}
class Deck
{
        public void Reveal( )
        {
                Revealed = Cards!.ElementAt( 1 );
                Cards!.RemoveAt( 1 );
        }
        public Card? Revealed { get; set; }
        private bool _isShuffled = false;
        public Deck( ) { }
        public List<Card>? Cards { get; set; }
        public bool IsShuffled { get => _isShuffled; set => _isShuffled = value; }
        public void Shuffle( )
        {
                int n = Cards!.Count;
                List<Card>? p = Cards;
                while(n > 1)
                {
                        n--;
                        int k = new Random( ).Next( n + 1 );
                        (p[ n ], p[ k ]) = (p[ k ], p[ n ]);
                }
                Cards = p;
                IsShuffled = true;
        }
        public override string ToString( )
        {
                StringBuilder sb = new( "\n  Gesamtes Deck:\n ---------------------------------------------\n   ".ForegroundColor( "green" ) );

                for(int i = 0 ; i < Cards!.Count ; i++)
                {
                        sb.Append( $" [{Cards[ i ]}]" );

                        if(i != 0 && ((i + 1) % 10) == 0 && (i + 1) != Cards.Count)
                                sb.Append( "\n   " );

                        if(i == Cards.Count - 1)
                                sb.Append( '\n' );
                }
                return sb.Append( " ---------------------------------------------\n".ForegroundColor( "green" ) ).ToString( );
        }
}
class DeckController
{
        public DeckController( GameController gameController )
        {
                GameController = gameController;

                Deck = Factory.Create( );
        }
        static class Factory
        {
                public static Deck Create( )
                {
                        Deck d = new( );

                        List<Card> c = Card.Factory.Create( [ Color.Green , Color.Red , Color.Yellow , Color.Blue ] , 80 );

                        d.Cards = c;

                        return d;
                }
        }
        public GameController GameController { get; }
        public Deck Deck { get; }
        public DeckArg DeckArgs( ) => new( this.Deck );
}
class DeckArg( Deck d ) : EventArgs
{
        public Card? Revealed { get; set; } = d.Revealed;
        public Deck Deck { get; set; } = d;
        public List<Card>? Cards { get => this.Deck!.Cards; }
        public uint? Count { get => ( uint )Cards!.Count; }
        public override string ToString( )
                => new StringBuilder( $"DeckState: \n  Revealed Card:{(Revealed == null ? " keine aufgedeckte Karte" : $"{Revealed}")}" )
                             .Append( $"           \n  IsShuffled   : ist{(Deck.IsShuffled == true ? " " : " nicht ")}gemischt" )
                             .Append( $"           \n  Cards in Deck: {Count} Karten" )
                             .AppendLine( this.Deck.ToString( ) )
                         .ToString( );
}
class Player
{
        public bool HasPlayedACard { get; set; }
        public static uint Id = 0;
        public string? Name { get; set; }
        public Player( )
        {
                Index = Id;
                Console.Write( "\n - Spielernamen eingeben:  " );
                Name = Console.ReadLine( );
                Id++;
                Hand = [];
                HasPlayedACard = false;
        }
        public List<Card>? Hand { get; set; }
        public uint Index { get; set; }
        public override string ToString( ) => $"\t {Name,6} ".BackgroundColor( "0,50,0" ) +
                $" - {Index + 1}.Spieler";
        public void AddToHand( Card c ) => Hand!.Add( c );
        internal string PrintHand( )
        {
                StringBuilder sb = new( );
                foreach(Card c in Hand!)
                        sb.Append( $"[{c}]" );
                return sb.ToString( );
        }
        internal void PlayCard( Card c ) => Hand!.Remove( c );
}
class PlayerController
{
        public PlayerController( GameController gc )
        {
                Controller = gc;
                AllPlayers = [];
                LastPlayerIndex = 0;
                // Shuffle( );
        }
        public void GetPlayerAmount( )
        {
                Console.Write( "\n - Geben Sie die Spieler-Anzahl ein:  " );
                string? input = Console.ReadLine( );

                if(UInt32.TryParse( input , out uint i ))
                        for(int j = 0 ; j < i ; j++)
                        {
                                AllPlayers!.Add( new Player( ) );
                                Console.WriteLine(
                                        $"{AllPlayers.ElementAt( j ),2}" +
                                        " wurde erstellt.".ForegroundColor( "green" )
                                        );
                        }
        }
        public void Shuffle( )
        {
                int n = AllPlayers!.Count;
                List<Player>? p = AllPlayers;
                while(n > 1)
                {
                        n--;
                        int k = new Random( ).Next( n + 1 );
                        (p[ n ], p[ k ]) = (p[ k ], p[ n ]);
                }
                uint i = 0;
                foreach(Player P in p)
                {
                        P.Index = i;
                        i++;
                }
                AllPlayers = p;
        }
        public void HandOut( )
        {
                for(int j = 0 ; j < Controller.Players!.AllPlayers!.Count ; j++)
                {
                        for(int i = 0 ; i < 5 ; i++)
                        {
                                Card? c = Controller.Deck!.Deck.Cards!.ElementAtOrDefault( 1 );
                                Controller.Players.AllPlayers[ j ].AddToHand( c! );
                                Controller.Deck!.Deck.Cards!.RemoveAt( 1 );
                        }
                        Console.WriteLine( $"{Controller.Players.AllPlayers[ j ]}´s-Hand: " + Controller.Players.AllPlayers[ j ].PrintHand( )! );
                }
        }
        public Player? CurrentPlayer { get => AllPlayers![ LastPlayerIndex ]; }
        public void Next( ) => LastPlayerIndex = (LastPlayerIndex == AllPlayers!.Count - 1) ? 0 : LastPlayerIndex + 1;
        GameController Controller { get; set; }
        public List<Player>? AllPlayers { get; set; }
        public string? Legend { get; set; }
        public List<string>? ValidChoices { get; set; }
        public List<Card>? PlayAbleCards( ) => CompareHand( true );
        public List<Card>? UnPlayableCards( ) => CompareHand( false );
        public string PlayableCardsString( )
        {
                StringBuilder sb = new( );
                StringBuilder leg = new( );
                ValidChoices = [];
                int i = 0;
                foreach(Card c in PlayAbleCards( )!)
                {
                        sb.Append(
                                        "[".ForegroundColor( "15,200,60" )
                                        +
                                        $"{c}".ForegroundColor( "green" )
                                        +
                                        "]".ForegroundColor( "15,200,60" )
                                );
                        ValidChoices.Add( i.ToString( ) );
                        leg.Append( $"[{i++}]" );
                }
                Legend = leg.ToString( );
                return sb.ToString( );
        }
        public string UnPlayableCardsString( )
        {
                StringBuilder sb = new( );

                foreach(Card c in UnPlayableCards( )!)
                        sb.Append(
                                        "[".ForegroundColor( "0,0,0" ).BackgroundColor( "50,50,50" )
                                        +
                                        $"{c}".ForegroundColor( "40,0,0" )
                                        +
                                        "]".ForegroundColor( "0,0,0" ).BackgroundColor( "50,50,50" )
                                );
                return sb.ToString( );
        }
        private List<Card> CompareHand( bool comp )
        {
                List<Card> result = [];

                foreach(Card c in AllPlayers![ LastPlayerIndex ].Hand!)

                        if(Compare( c , comp ))

                                result.Add( c );

                return result;
        }
        public void DrawCard( )
        {
                CurrentPlayer!.HasPlayedACard = false;
                Card? c = Controller.Deck!.Deck.Cards!.ElementAtOrDefault( 1 );
                CurrentPlayer!.AddToHand( c! );
                Controller.Deck!.Deck.Cards!.RemoveAt( 1 );
        }
        public void PlayCard( Card c )
        {
                Controller.Deck!.Deck.Revealed = c;
                CurrentPlayer!.PlayCard( c );
                CurrentPlayer.HasPlayedACard = true;
        }
        private bool Compare( Card c , bool x = true )
                => x is true
                        ?
                        c.C == Controller.Deck!.Deck!.Revealed!.C
                                ||
                        c.N == Controller.Deck!.Deck!.Revealed!.N
                        :
                        c.C != Controller.Deck!.Deck!.Revealed!.C
                                &&
                        c.N != Controller.Deck!.Deck!.Revealed!.N
                        ;
        public int LastPlayerIndex { get; set; }
        public override string ToString( )
        {
                StringBuilder sb = new( " Spielerreihenfolge:\n -------------------------------\n".ForegroundColor( "green" ) );
                foreach(Player p in AllPlayers!)
                        sb.Append( $"{p}\n" );
                return sb.ToString( );
        }
}
class PrintController
{
        public PrintController( GameController gc )
        {
                Controller = gc;

                gc.Updated += GameControllerUpdate!;

                this.Index = 0;
                ToPrint = GetCurrent( );
        }
        void GameControllerUpdate( object controller , EventArgs args )
        {
                if(args is PrintArg p && p.Index != this.Index)
                {
                        Index = p.Index;

                        GetCurrent( );
                        DisplayCurrent( );
                }
        }
        GameController Controller { get; set; }
        private Print? GetCurrent( ) => Prints[ Index ];
        public void DisplayCurrent( )
        {
                if(ToPrint != null)
                        switch(ToPrint)
                        {
                                case Print.Greeting:
                                        PrintGreeting( );
                                        break;
                                case Print.GameOver:
                                        PrintGameOver( );
                                        break;
                                case Print.Screen:
                                        PrintScreen( );
                                        break;
                        }
                Updated?.Invoke( this , PrintArgs( ) );
        }
        private void PrintScreen( ) => StringToPrint = ConstructScreen( );
        private string? ConstructScreen( )
        {
                string result = "";

                var state = Controller!.State!.LastState;
                var c = Controller!;
                if(state != null)
                {
                        switch(state)
                        {
                                case GameState.PrintScreen:
                                        result = state.Value.ToString( );
                                        break;
                                case GameState.PrintGreeting:
                                        result = state.Value.ToString( );
                                        break;
                                case GameState.PrintGameOver:
                                        result = state.Value.ToString( );
                                        break;

                        }
                }
                return result;
        }
        private void PrintGameOver( )
        {
                throw new NotImplementedException( );
        }
        private void PrintGreeting( )
        {
                StringToPrint = "\n\t Hello \n\n";
        }
        string? StringToPrint { get; set; }
        public uint Index { get => _index; set => _index = value > 12 ? 0 : value; }
        private uint _index;
        public event EventHandler<PrintArg>? Updated;
        private static readonly Print[] Prints =
                [
                        Print.Greeting,
                        Print.Screen,
                        Print.GameOver,
                ];
        public PrintArg PrintArgs( ) => new( Index , ToPrint , StringToPrint );
        public Print? ToPrint { get; set; }
}
class PrintArg( uint index , Print? state , string? toPrint ) : EventArgs
{
        public uint Index { get; set; } = index;
        public Print? PrintState { get; } = state;
        public string? ToPrint { get; set; } = toPrint;
        public override string ToString( ) => $"PrintArg[{Index,2}]:{PrintState}";
}
class StateController
{
        public static StateController? Instance { get; }
        static StateController( ) => Instance = new( );
        private StateController( )
        {
                Index = 0;
                LastState = GetCurrent( );
        }
        public void AdvanceState( )
        {
                this.Index++;
                SetState( GetCurrent( ) );
        }
        GameState? GetCurrent( ) => GameStates[ Index ];
        void SetState( GameState? state )
        {
                LastState = state;
                Updated?.Invoke( this , StateArgs( ) );
        }
        public event EventHandler<StateArg>? Updated;
        public GameState? LastState { get; set; }
        public uint Index { get => _index; set => _index = value > 12 ? 0 : value; }
        private uint _index;
        public static readonly GameState[] GameStates =
                [
                        GameState.PrintGreeting,
                        GameState.GetDeck,
                        GameState.ShuffleDeck,
                        GameState.GetPlayers,
                        GameState.RandomizePlayerChain,
                        GameState.HandOutCards,
                        GameState.RevealCard,
                        GameState.Loop,
                        GameState.PrintScreen,
                        GameState.PlayerPlaysCard,
                        GameState.PlayerDrawsCard,
                        GameState.NextPlayer,
                        GameState.PrintGameOver,
                ];
        public StateArg StateArgs( ) => new( Index , LastState );
}
class StateArg( uint index , GameState? state ) : EventArgs
{
        public uint Index { get; set; } = index;
        public GameState? GameState { get; } = state;
        public override string ToString( ) => $"PrintState[{Index,2}]:{GameState}";
}

enum Print
{
        Greeting = 0,
        Screen = 1,
        GameOver = 2,
}
enum GameState
{
        PrintGreeting,
        GetDeck,
        ShuffleDeck,
        GetPlayers,
        RandomizePlayerChain,
        HandOutCards,
        RevealCard,
        Loop,
        PrintScreen,
        PlayerPlaysCard,
        PlayerDrawsCard,
        NextPlayer,
        PrintGameOver,
}