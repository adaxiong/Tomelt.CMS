using System;
using NHibernate;

namespace Tomelt.Data {
    public interface ISessionLocator : IDependency {

        [Obsolete("Use ITransactionManager.GetSession() instead.")]
        ISession For(Type entityType);
    }
}