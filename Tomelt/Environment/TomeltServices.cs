using System;
using Tomelt.Data;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement;
using Tomelt.Security;
using Tomelt.UI.Notify;

namespace Tomelt.Environment {
    public class TomeltServices : ITomeltServices {
        private readonly Lazy<IShapeFactory> _shapeFactory;
        private readonly IWorkContextAccessor _workContextAccessor;

        public TomeltServices(
            IContentManager contentManager,
            ITransactionManager transactionManager,
            IAuthorizer authorizer,
            INotifier notifier,
            Lazy<IShapeFactory> shapeFactory,
            IWorkContextAccessor workContextAccessor) {
            _shapeFactory = shapeFactory;
            _workContextAccessor = workContextAccessor;
            ContentManager = contentManager;
            TransactionManager = transactionManager;
            Authorizer = authorizer;
            Notifier = notifier;
        }

        public IContentManager ContentManager { get; private set; }
        public ITransactionManager TransactionManager { get; private set; }
        public IAuthorizer Authorizer { get; private set; }
        public INotifier Notifier { get; private set; }
        public dynamic New { get { return _shapeFactory.Value; } }
        public WorkContext WorkContext { get { return _workContextAccessor.GetContext(); } }
    }
}
