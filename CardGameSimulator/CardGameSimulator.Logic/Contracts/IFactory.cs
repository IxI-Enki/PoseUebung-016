namespace CardGameSimulator.Logic.Contracts;

interface IFactory<T> where T : class
{
        List<T> Create_N( int n );
}
