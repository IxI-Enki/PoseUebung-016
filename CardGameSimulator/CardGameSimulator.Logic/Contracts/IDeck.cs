using CardGameSimulator.Logic.Args;

namespace CardGameSimulator.Logic.Contracts;

interface IDeck
{


        List<ICard>? Cards { get; set; }

        abstract void Shuffle( );
        abstract void DrawCard( object controller , CardEventArgs card );


        // void DrawCards( );
        // ICard? RevealedCard { get; }

}
