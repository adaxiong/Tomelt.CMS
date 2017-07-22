using System;
using System.Linq;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data;
using Tomelt.Data.Migration;

namespace Tomelt.Core.Common {
    public class Migrations : DataMigrationImpl {
        private readonly IRepository<IdentityPartRecord> _identityPartRepository;

        public Migrations(IRepository<IdentityPartRecord> identityPartRepository) {
            _identityPartRepository = identityPartRepository;
        }

        public int Create() {
            SchemaBuilder.CreateTable("BodyPartRecord",
                table => table
                    .ContentPartVersionRecord()
                    .Column<string>("Text", column => column.Unlimited())
                    .Column<string>("Format")
                );

            SchemaBuilder.CreateTable("CommonPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("OwnerId")
                    .Column<DateTime>("CreatedUtc")
                    .Column<DateTime>("PublishedUtc")
                    .Column<DateTime>("ModifiedUtc")
                    .Column<int>("Container_id")
                );

            SchemaBuilder.CreateTable("CommonPartVersionRecord",
                table => table
                    .ContentPartVersionRecord()
                    .Column<DateTime>("CreatedUtc")
                    .Column<DateTime>("PublishedUtc")
                    .Column<DateTime>("ModifiedUtc")
                    .Column<string>("ModifiedBy")
                );

            SchemaBuilder.CreateTable("IdentityPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Identifier", column => column.WithLength(255))
                );

            ContentDefinitionManager.AlterPartDefinition("BodyPart", builder => builder
                .Attachable()
                .WithDescription("允许编辑器可用适用富文本编辑器（如：HTML,TEXT,MarkDown）"));

            ContentDefinitionManager.AlterPartDefinition("CommonPart", builder => builder
                .Attachable()
                .WithDescription("提供有关内容项的公共信息，如所有者、创建日期、发布日期和日期修改"));

            ContentDefinitionManager.AlterPartDefinition("IdentityPart", builder => builder
                .Attachable()
                .WithDescription("自动为内容项生成唯一标识，在导入/导出场景中，其中一个内容项引用另一个内容。"));

            return 5;
        }

        public int UpdateFrom1() {
            SchemaBuilder.CreateTable("IdentityPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Identifier", column => column.Unlimited())
                );
            ContentDefinitionManager.AlterPartDefinition("IdentityPart", builder => builder.Attachable());

            return 2;
        }

        public int UpdateFrom2() {
            ContentDefinitionManager.AlterPartDefinition("BodyPart", builder => builder
                .WithDescription("允许编辑器可用适用富文本编辑器（如：HTML,TEXT,MarkDown）"));

            ContentDefinitionManager.AlterPartDefinition("CommonPart", builder => builder
                .WithDescription("提供有关内容项的公共信息，如所有者、创建日期、发布日期和日期修改"));

            ContentDefinitionManager.AlterPartDefinition("IdentityPart", builder => builder
                .WithDescription("自动为内容项生成唯一标识，在导入/导出场景中，其中一个内容项引用另一个内容。"));

            return 3;
        }

        public int UpdateFrom3() {
            var existingIdentityParts = _identityPartRepository.Table.ToArray();

            foreach (var existingIdentityPart in existingIdentityParts) {
                if (existingIdentityPart.Identifier.Length > 255) {
                    throw new ArgumentException("Identifier '" + existingIdentityPart + "' is over 255 characters");
                }
            }

            SchemaBuilder.AlterTable("IdentityPartRecord", table => table.DropColumn("Identifier"));
            SchemaBuilder.AlterTable("IdentityPartRecord", table => table.AddColumn<string>("Identifier", command => command.WithLength(255)));

            foreach (var existingIdentityPart in existingIdentityParts) {
                var updateIdentityPartRecord = _identityPartRepository.Get(existingIdentityPart.Id);

                updateIdentityPartRecord.Identifier = existingIdentityPart.Identifier;

                _identityPartRepository.Update(updateIdentityPartRecord);
            }

            return 4;
        }
        public int UpdateFrom4() {
            SchemaBuilder.AlterTable("CommonPartVersionRecord", table => table.AddColumn<string>("ModifiedBy", command => command.Nullable()));
            return 5;
        }
    }
}