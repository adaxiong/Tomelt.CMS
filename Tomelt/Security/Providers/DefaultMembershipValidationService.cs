namespace Tomelt.Security {
    public class DefaultMembershipValidationService : IMembershipValidationService {
        public bool CanAuthenticateWithCookie(IUser user) {
            return true;
        }
    }
}
