using CardGameSimulator.Logic.Args;

namespace CardGameSimulator.Logic.Contracts;

interface IDeck
{
        List<Card>? Cards { get; set; }

         void Shuffle( );
         void DrawCard( object controller , CardEventArgs card );

}
