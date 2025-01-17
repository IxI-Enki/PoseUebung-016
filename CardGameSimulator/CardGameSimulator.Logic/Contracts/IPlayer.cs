using CardGameSimulator.Logic.Args;

namespace CardGameSimulator.Logic.Contracts;

interface IPlayer<T> where T : ICard
{
        bool HasPlayedACard { get; set; }
        List<T>? Hand { get; set; }

        void DrawCard( );
        void PlayCard( );
        void Update( object gameController , EventArgs args );
}
