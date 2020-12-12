using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pharmacy.Config
{
    public static class RUNE
    {

        public static readonly bool IS_SUPPORT_NOTIFY_ICON = FeaturesParser
            .FeatureOrders["HPS_FT_IS_SUPPORT_NOTIFY_ICON"];

        public static readonly bool IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL = FeaturesParser
            .FeatureOrders["HPS_FT_IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL"];


        private sealed class FeaturesParser
        {
            private static string _titleGroup = @"(?<Title>\S+)";
            private static string _valueGroup = @"(?<Value>TRUE|FALSE)";
            private static string _pattern;

            public static Dictionary<string, bool> FeatureOrders = new Dictionary<string, bool>();

            static FeaturesParser()
            {
                FileParsing();
            }

            public static void FileParsing()
            {
                _pattern = _titleGroup + "=" + _valueGroup;

                string t = Pharmacy.Properties.Resources.AppRunConfig;
                string[] t2 = t.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(line => !line.StartsWith("#")).ToArray();
                for (int i = 0; i < t2.Length; i++)
                {
                    Match match = Regex.Match(t2[i], _pattern);
                    if (match.Success)
                    {
                        FeatureOrders.Add(match.Groups["Title"].ToString(),
                            Convert.ToBoolean(match.Groups["Value"].ToString()));
                    }
                }

            }
        }
    }

}
