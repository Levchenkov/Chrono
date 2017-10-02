namespace Chrono.Client.DataMappers
{
    public class SessionDataMapper : IDataMapper<Session, ChronoSession>
    {
        public Session Map(ChronoSession second)
        {
            if (second == null)
            {
                return null;
            }

            var result = new Session
            {
                Id = second.Id,
                Begin = second.StartDate,
                End = second.StopDate
            };

            return result;
        }

        public ChronoSession Map(Session first)
        {
            if (first == null)
            {
                return null;
            }

            var result = new ChronoSession
            {
                Id = first.Id,
                StartDate = first.Begin,
                StopDate = first.End
            };

            return result;
        }
    }
}
