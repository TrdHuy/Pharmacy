using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace Pharmacy.Base.Utils.ViewModelHelper
{
    [MarkupExtensionReturnType(typeof(object))]
    public class VMManagerMarkupExtension : MarkupExtension
    {
        private static Dictionary<Type, object> DataContextInstanceCache;

        [ConstructorArgument("dataContextType")]
        public Type DataContextType { get; set; }

        static VMManagerMarkupExtension()
        {
            DataContextInstanceCache = new Dictionary<Type, object>();
        }

        public VMManagerMarkupExtension(Type dataContextType)
        {
            DataContextType = dataContextType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DataContextInstanceCache.ContainsKey(DataContextType))
            {
                return DataContextInstanceCache[DataContextType];
            }
            var dataContext = Activator.CreateInstance(DataContextType);
            DataContextInstanceCache.Add(DataContextType, dataContext);
            return dataContext;
        }
    }
}
