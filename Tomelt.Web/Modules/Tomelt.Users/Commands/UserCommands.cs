﻿using Tomelt.Commands;
using Tomelt.Security;
using Tomelt.Users.Services;

namespace Tomelt.Users.Commands {
    public class UserCommands : DefaultTomeltCommandHandler {
        private readonly IMembershipService _membershipService;
        private readonly IUserService _userService;

        public UserCommands(
            IMembershipService membershipService,
            IUserService userService) {
            _membershipService = membershipService;
            _userService = userService;
        }

        [TomeltSwitch]
        public string UserName { get; set; }

        [TomeltSwitch]
        public string Password { get; set; }

        [TomeltSwitch]
        public string Email { get; set; }

        [TomeltSwitch]
        public string FileName { get; set; }

        [CommandName("user create")]
        [CommandHelp("user create /UserName:<username> /Password:<password> /Email:<email>\r\n\t" + "Creates a new User")]
        [TomeltSwitches("UserName,Password,Email")]
        public void Create() {
	        if (string.IsNullOrWhiteSpace(UserName)) {
		        Context.Output.WriteLine(T("Username cannot be empty."));
		        return;
	        }

            if (!_userService.VerifyUserUnicity(UserName, Email)) {
                Context.Output.WriteLine(T("User with that username and/or email already exists."));
                return;
            }

            if (Password == null || Password.Length < MinPasswordLength) {
                Context.Output.WriteLine(T("You must specify a password of {0} or more characters.", MinPasswordLength));
                return;
            }

            var user = _membershipService.CreateUser(new CreateUserParams(UserName, Password, Email, null, null, true));
            if (user == null) {
                Context.Output.WriteLine(T("Could not create user {0}. The authentication provider returned an error", UserName));
                return;
            }

            Context.Output.WriteLine(T("User created successfully"));
        }

        int MinPasswordLength {
            get {
                return _membershipService.GetSettings().MinRequiredPasswordLength;
            }
        }
    }
}