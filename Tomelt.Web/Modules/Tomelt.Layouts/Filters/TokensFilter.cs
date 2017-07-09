using System;
using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Services;
using Tomelt.Tokens;
using System.Collections.Generic;

namespace Tomelt.Layouts.Filters {
    [TomeltFeature("Tomelt.Layouts.Tokens")]
    public class TokensFilter : IElementFilter {

        private readonly ITokenizer _tokenizer;
 
        public TokensFilter(ITokenizer tokenizer) {
            _tokenizer = tokenizer;
        }

        public string ProcessContent(string text, string flavor) {
            return ProcessContent(text, flavor, new Dictionary<string, object>());
        }

        public string ProcessContent(string text, string flavor, IDictionary<string, object> context) {
            if (String.IsNullOrEmpty(text))
                return "";

            if (!text.Contains("#{")) {
                return text;
            }
            
            text = _tokenizer.Replace(text, context, new ReplaceOptions { Encoding = ReplaceOptions.NoEncode });

            return text;
        }
    }
}