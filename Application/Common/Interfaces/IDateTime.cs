﻿using System;

namespace Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}