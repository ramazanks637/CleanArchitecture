﻿using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Entities
{
    public sealed class ErrorLog : BaseEntity
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string RequestPath { get; set; }
        public string RequestMethod { get; set; }
        public DateTime Timestap { get; set; }
    }
}
