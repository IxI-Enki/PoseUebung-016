namespace CardGameSimulator.Logic.Factorys;

class CardFactory : IFactory<Card>
{
        public static CardFactory Instance { get; }
        static CardFactory( ) => Instance = new( );
        private CardFactory( ) { }

        public static List<Color> Colors { get { return [ Color.Blue , Color.Green , Color.Yellow , Color.Red ]; } }

        public static List<Card> CreateListOfCards( )
        {
                List<Card> result = [];

                Colors.ForEach( color =>
                {
                        for(int i = 0 ; i < 10 ; i++)
                        {
                                Card? card = new( ( uint )i , color );
                                result.Add( card );
                        }
                } );
                return result;
        }

        public List<Card> Create_N( int n )
        {
                List<Card> result = [];
                while(n > 1)
                {
                        Colors.ForEach( color =>
                        {
                                for(int i = 0 ; i < 10 ; i++)
                                {
                                        Card? card = new( ( uint )i , color );
                                        result.Add( card );
                                        n--;
                                }
                        } );
                }
                return result;
        }

}