
namespace CardGameSimulator.Logic.Models;

internal class Card( uint val , Color col ) : ICard
{


        public uint Value => val;

        public Color Color => col;
}
