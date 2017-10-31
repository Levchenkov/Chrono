using System;
using Chrono.Client;
using System.Collections.Generic;

namespace Chrono.Host.Services
{
    public class SessionModeService : ISessionModeService
    {
        private static IDictionary<string, ChronoSessionMode> modes = new Dictionary<string, ChronoSessionMode>();

        public ChronoSessionMode GetSessionMode(string sessionId)
        {
            if (!modes.ContainsKey(sessionId))
            {
                modes.Add(sessionId, ChronoSessionMode.Record);
            }

            return modes[sessionId];
        }

        public void PlaySession(string sessionId)
        {
            modes.Add(sessionId, ChronoSessionMode.Play);
        }

        public void RecordSession(string sessionId)
        {
            modes.Add(sessionId, ChronoSessionMode.Record);
        }
    }
}
