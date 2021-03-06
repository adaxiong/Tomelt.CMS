﻿namespace Tomelt.Environment {
    /// <summary>
    /// Interface implemented by shims for ASP.NET singleton services that
    /// need access to the HostContainer instance.
    /// </summary>
    public interface IShim {
        ITomeltHostContainer HostContainer { get; set;}
    }
}