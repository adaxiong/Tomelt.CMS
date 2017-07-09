using Tomelt.ContentManagement;

namespace Tomelt.Security {
    /// <summary>
    /// Interface provided by the "User" model. 
    /// </summary>
    public interface IUser : IContent {
        string UserName { get; }
        string Email { get; }
    }
}
