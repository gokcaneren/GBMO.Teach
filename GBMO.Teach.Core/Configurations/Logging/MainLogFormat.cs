﻿namespace GBMO.Teach.Core.Configurations.Logging
{
    public abstract class MainLogFormat
    {
        public string RequestPath { get; set; }
        public string HttpMethod { get; set; }
        public string RequestBody { get; set; }
    }
}
