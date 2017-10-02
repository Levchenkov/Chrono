namespace Chrono.Client.DataMappers
{
    public interface IDataMapper<TFirst, TSecond>
    {
        TSecond Map(TFirst first);

        TFirst Map(TSecond second);
    }
}
