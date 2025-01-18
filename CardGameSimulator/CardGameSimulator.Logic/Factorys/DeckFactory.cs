
namespace CardGameSimulator.Logic.Factorys;

class DeckFactory : IFactory<Deck>
{
        internal static Deck? CreateDeck( )
        {
                Deck? deck = new( )
                {
                        Cards = CardFactory.CreateListOfCards( )
                };
                deck.Shuffle( );
                return deck;
        }

        public List<Deck> Create_N( int n )
        {
                throw new NotImplementedException( );
        }
}
