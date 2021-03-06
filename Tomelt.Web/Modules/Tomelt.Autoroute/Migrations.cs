﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Autoroute.Models;
using Tomelt.Autoroute.Settings;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;
using Tomelt.Localization.Services;

namespace Tomelt.Autoroute {
    public class Migrations : DataMigrationImpl {
        private readonly ICultureManager _cultureManager;

        public Migrations(ICultureManager cultureManager) {
            _cultureManager = cultureManager;
        }

        public int Create() {
            SchemaBuilder.CreateTable("AutoroutePartRecord",
                table => table
                    .ContentPartVersionRecord()
                            .Column<string>("CustomPattern", c => c.WithLength(2048))
                            .Column<bool>("UseCustomPattern", c => c.WithDefault(false))
                            .Column<bool>("UseCulturePattern", c => c.WithDefault(false))
                            .Column<string>("DisplayAlias", c => c.WithLength(2048)));

            ContentDefinitionManager.AlterPartDefinition("AutoroutePart", part => part
                .Attachable()
                .WithDescription("将高级URL配置选项添加到内容类型中，以完全自定义内容项的URL模式"));

            SchemaBuilder.AlterTable("AutoroutePartRecord", table => table
                .CreateIndex("IDX_AutoroutePartRecord_DisplayAlias", "DisplayAlias")
            );

            return 4;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("AutoroutePart", part => part
                .WithDescription("将高级URL配置选项添加到内容类型中，以完全自定义内容项的URL模式"));
            return 2;
        }

        public int UpdateFrom2() {

            SchemaBuilder.AlterTable("AutoroutePartRecord", table => table
                .CreateIndex("IDX_AutoroutePartRecord_DisplayAlias", "DisplayAlias")
            );

            return 3;
        }

        public int UpdateFrom3() {

            SchemaBuilder.AlterTable("AutoroutePartRecord", table => table
                .AddColumn<bool>("UseCulturePattern", c => c.WithDefault(false))
            );

            return 4;
        }

        public int UpdateFrom4() {
            // Adding some culture neutral patterns if they don't exist
            var autoroutePartDefinitions = ContentDefinitionManager.ListTypeDefinitions()
                                            .Where(t => t.Parts.Any(p => p.PartDefinition.Name.Equals(typeof(AutoroutePart).Name)))
                                            .Select(s => new { contentTypeName = s.Name, autoroutePart = s.Parts.First(x => x.PartDefinition.Name == "AutoroutePart") });

            foreach (var partDefinition in autoroutePartDefinitions) {
                var settingsDictionary = partDefinition.autoroutePart.Settings;
                var settings = settingsDictionary.GetModel<AutorouteSettings>();

                if (!settings.Patterns.Any(x => String.IsNullOrWhiteSpace(x.Culture))) {
                    string siteCulture = _cultureManager.GetSiteCulture();
                    List<string> newPatterns = new List<string>();

                    if (settings.Patterns.Any(x => String.Equals(x.Culture, siteCulture, StringComparison.OrdinalIgnoreCase))) {
                        var siteCulturePatterns = settings.Patterns.Where(x => String.Equals(x.Culture, siteCulture, StringComparison.OrdinalIgnoreCase)).ToList();

                        foreach (RoutePattern pattern in siteCulturePatterns) {
                            newPatterns.Add(String.Format("{{\"Name\":\"{0}\",\"Pattern\":\"{1}\",\"Description\":\"{2}\"}}", pattern.Name, pattern.Pattern, pattern.Description));
                        }
                    }
                    else {
                        newPatterns.Add(String.Format("{{\"Name\":\"{0}\",\"Pattern\":\"{1}\",\"Description\":\"{2}\"}}", "Title", "{Content.Slug}", "my-title"));
                    }

                    if (settingsDictionary.ContainsKey("AutorouteSettings.PatternDefinitions")) {
                        string oldPatterns = settingsDictionary["AutorouteSettings.PatternDefinitions"];
                        if (oldPatterns.StartsWith("[") && oldPatterns.EndsWith("]"))
                            newPatterns.Add(oldPatterns.Substring(1, oldPatterns.Length - 2));
                    }

                    ContentDefinitionManager.AlterTypeDefinition(partDefinition.contentTypeName, cfg => cfg
                    .WithPart("AutoroutePart", builder => builder
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[" + String.Join(",", newPatterns) + "]")
                    ));
                }
            }

            return 5;
        }
    }
}