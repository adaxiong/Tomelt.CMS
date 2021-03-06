﻿using NHibernate;

namespace Tomelt.Data {
    /// <summary>
    /// Describes an NHibernate session interceptor, instantiated per-session.
    /// </summary>
    public interface ISessionInterceptor : IInterceptor, IDependency {
    }
}