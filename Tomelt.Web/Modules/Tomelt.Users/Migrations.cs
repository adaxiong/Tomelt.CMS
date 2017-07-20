using System;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.Users {
    public class UsersDataMigration : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("UserPartRecord", 
                table => table
                    .ContentPartRecord()
                    .Column<string>("UserName")
                    .Column<string>("Email")
                    .Column<string>("NormalizedUserName")
                    .Column<string>("Password")
                    .Column<string>("PasswordFormat")
                    .Column<string>("HashAlgorithm")
                    .Column<string>("PasswordSalt")
                    .Column<string>("RegistrationStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailChallengeToken")
                    .Column<DateTime>("CreatedUtc")
                    .Column<DateTime>("LastLoginUtc")
                    .Column<DateTime>("LastLogoutUtc")
                );

            ContentDefinitionManager.AlterTypeDefinition("User", cfg => cfg.Creatable(false));

            return 4;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterTypeDefinition("User", cfg => cfg.Creatable(false));

            return 2;
        }

        public int UpdateFrom2() {
            SchemaBuilder.AlterTable("UserPartRecord",
                table => {
                    table.AddColumn<DateTime>("CreatedUtc");
                    table.AddColumn<DateTime>("LastLoginUtc");
                });

            return 3;
        }

        public int UpdateFrom3() {
            SchemaBuilder.AlterTable("UserPartRecord",
                table => {
                    table.AddColumn<DateTime>("LastLogoutUtc");
                });

            return 4;
        }
        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterTypeDefinition("User", cfg => cfg.DisplayedAs("用户"));

            return 5;
        }
    }
}