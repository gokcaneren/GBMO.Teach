﻿namespace GBMO.Teach.Core.Configurations.Logging
{
    public class ErrorLogFormat : MainLogFormat
    {
        public string? ExceptionMessage { get; set; }
        public string? ExceptionStackTrace { get; set; }
    }
}
