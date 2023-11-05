﻿namespace ESP.Request
{
    public class HttpPatternRequest
    {
        public int Id { get; set; }

        public string Script { get; set; } = string.Empty;
        public string TestInfo { get; set; } = string.Empty;
        public string Cases { get; set; } = string.Empty;
        public string ConnectionInformationToProduction { get; set; } = string.Empty;
    }
}
