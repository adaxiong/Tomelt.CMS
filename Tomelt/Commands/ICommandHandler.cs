﻿namespace Tomelt.Commands {
    public interface ICommandHandler : IDependency {
        void Execute(CommandContext context);
    }
}
