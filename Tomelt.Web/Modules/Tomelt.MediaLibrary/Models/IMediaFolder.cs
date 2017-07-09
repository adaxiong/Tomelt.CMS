﻿using System;

namespace Tomelt.MediaLibrary.Models {
    public interface IMediaFolder {
        string Name { get; }
        string MediaPath { get; }
        string User { get; }
        DateTime LastUpdated { get; }
        long Size { get; }
    }
}