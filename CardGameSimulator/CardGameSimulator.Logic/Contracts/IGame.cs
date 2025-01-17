using CardGameSimulator.Logic.Args;

namespace CardGameSimulator.Logic.Contracts;

interface IGame
{
        void Start( );
        void Run( );
        void Stop( object controller , GameEventArgs state );
}