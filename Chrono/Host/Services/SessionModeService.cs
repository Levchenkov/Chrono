using System;
using Chrono.Client;
using System.Collections.Generic;

namespace Chrono.Host.Services
{
    public class SessionModeService : ISessionModeService
    {
        private static readonly IDictionary<string, ChronoSessionMode> modes = new Dictionary<string, ChronoSessionMode>();

        private object lockObject = new object();

        public ChronoSessionMode GetSessionMode(string sessionId)
        {
            lock (lockObject)
            {
                if (modes.ContainsKey(sessionId))
                {
                    return modes[sessionId];
                }
                else
                {
                    modes.Add(sessionId, ChronoSessionMode.Record);
                    return ChronoSessionMode.Record;
                }
            }
        }

        public void PlaySession(string sessionId)
        {
            lock(lockObject)
            {
                if (modes.ContainsKey(sessionId))
                {
                    modes[sessionId] = ChronoSessionMode.Play;
                }
                else
                {
                    modes.Add(sessionId, ChronoSessionMode.Play);
                }
            }
        }

        public void RecordSession(string sessionId)
        {
            lock (lockObject)
            {
                if (modes.ContainsKey(sessionId))
                {
                    modes[sessionId] = ChronoSessionMode.Record;
                }
                else
                {
                    modes.Add(sessionId, ChronoSessionMode.Record);
                }
            }
        }
    }
}
