
namespace CardGameSimulator.Logic.Models;

class Player : IPlayer<Card>
{
        private List<Card>? _hand = [];
        private bool _hasPlayedACard = false;

        public Player( )
        {
        }

        public bool HasPlayedACard
        {
                get => _hasPlayedACard;
                set => _hasPlayedACard = value;
        }
        public List<Card>? Hand
        {
                get => _hand;
                set => _hand
                        = value is not null
                        ? value is List<Card>
                        ? value
                        : [] : [];
        }

        public void DrawCard( )
        {
                throw new NotImplementedException( );
        }

        public void PlayCard( )
        {
                throw new NotImplementedException( );
        }

        public void Update( object gameController , EventArgs args )
        {
                throw new NotImplementedException( );
        }
}
