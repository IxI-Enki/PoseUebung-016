


namespace CardGameSimulator.Logic.Models;

class Deck : IDeck
{
        public List<Card>? Cards { get; set; }

        public void DrawCard( object controller , CardEventArgs card )
        {
                throw new NotImplementedException( );
        }

        public void Shuffle( )
        {
                throw new NotImplementedException( );
        }
}
