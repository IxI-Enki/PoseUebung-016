
namespace CardGameSimulator.Logic.Factorys;

public static class PlayerFactory
{
        internal static List<Player>? SetPlayers( )
        {
                List<Player> result = [];
                int? playerIndex = default;


                Console.WriteLine( "Wie viele Spieler?   :" );




                for(int i = 0 ; i < playerIndex ; i++)
                        result.Add( new Player( ) );

                return result;
        }
}