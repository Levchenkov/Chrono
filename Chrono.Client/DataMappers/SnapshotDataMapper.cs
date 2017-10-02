namespace Chrono.Client.DataMappers
{
    public class SnapshotDataMapper : IDataMapper<Snapshot, ChronoSnapshot>
    {
        public Snapshot Map(ChronoSnapshot second)
        {
            if(second == null)
            {
                return null;
            }

            var result = new Snapshot
            {
                Id = second.Id,
                Begin = second.Begin,
                End = second.End,
                SessionId = second.SessionId,
                Key = second.Key,
                Value = second.Value
            };

            return result;
        }

        public ChronoSnapshot Map(Snapshot first)
        {
            if (first == null)
            {
                return null;
            }

            var result = new ChronoSnapshot
            {
                Id = first.Id,
                Begin = first.Begin,
                End = first.End,
                SessionId = first.SessionId,
                Key = first.Key,
                Value = first.Value
            };

            return result;
        }
    }
}
