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

        public static readonly bool IS_SUPPORT_ADMIN_CHANGE_USER_IMAGE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_ADMIN_CHANGE_USER_IMAGE"];

        public static readonly bool IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME"];

        public static readonly bool IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE"];

        public static readonly bool IS_SUPPORT_SEARCH_CUSTOMER_BY_EMAIL = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_CUSTOMER_BY_EMAIL"];

        public static readonly bool IS_SUPPORT_SEARCH_CUSTOMER_BY_ADDRESS = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_CUSTOMER_BY_ADRESS"];

        public static readonly bool IS_SUPPORT_SEARCH_MEDICINE_BY_ID = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_MEDICINE_BY_ID"];

        public static readonly bool IS_SUPPORT_SEARCH_MEDICINE_BY_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_MEDICINE_BY_NAME"];

        public static readonly bool IS_SUPPORT_SEARCH_MEDICINE_BY_SUPPLIER_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_MEDICINE_BY_SUPPLIER_NAME"];

        public static readonly bool IS_SUPPORT_FILTER_MEDICINE_BY_TYPE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_MEDICINE_BY_TYPE"];

        public static readonly bool IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_ID = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_ID"];

        public static readonly bool IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_NAME"];

        public static readonly bool IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_SUPPLIER_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_SUPPLIER_NAME"];

        public static readonly bool IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_START_DATE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_START_DATE"];

        public static readonly bool IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_END_DATE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_END_DATE"];

        public static readonly bool IS_SUPPORT_CHANGE_PERSONAL_AVATAR_LOCATION = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_CHANGE_PERSONAL_AVATAR_LOCATION"];

        public static readonly bool IS_SUPPORT_CHANGE_PERSONAL_AVATAR_ZOOM = FeaturesParser
                   .FeatureOrders["HPS_FT_IS_SUPPORT_CHANGE_PERSONAL_AVATAR_ZOOM"];

        public static readonly bool IS_SUPPORT_LOCATOR_WINDOW_FOR_PERSONAL_AVATAR = FeaturesParser
                   .FeatureOrders["HPS_FT_IS_SUPPORT_LOCATOR_WINDOW_FOR_PERSONAL_AVATAR"];

        public static readonly bool IS_SUPPORT_FILTER_SUPPLIER_BY_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_SUPPLIER_BY_NAME"];

        public static readonly bool IS_SUPPORT_FILTER_SUPPLIER_BY_PHONE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_SUPPLIER_BY_PHONE"];

        public static readonly bool IS_SUPPORT_SEARCH_OTHER_PAYMENT_BY_TYPE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_OTHER_PAYMENT_BY_TYPE"];

        public static readonly bool IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_START_DATE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_START_DATE"];

        public static readonly bool IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_END_DATE = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_END_DATE"];

        public static readonly bool IS_SUPPORT_SEARCH_ORDER_BY_CUSTOMER_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_ORDER_BY_CUSTOMER_NAME"];

        public static readonly bool IS_SUPPORT_SEARCH_ORDER_BY_MEDICINE_NAME = FeaturesParser
           .FeatureOrders["HPS_FT_IS_SUPPORT_SEARCH_ORDER_BY_MEDICINE_NAME"];

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
