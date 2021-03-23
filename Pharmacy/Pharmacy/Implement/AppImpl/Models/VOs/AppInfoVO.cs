using Pharmacy.Base.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.AppImpl.Models.VOs
{
    public class AppInfoVO
    {
        public static string ProductName { get; private set; }
        public static string ProductID { get; private set; }
        public static string ProductVersion { get; private set; }
        public static string ReleaseDate { get; private set; }
        public static string Description { get; private set; }
        public static string Company { get; private set; }

        static AppInfoVO()
        {
            ProductName = Assembly.GetExecutingAssembly().GetName().Name;
            ProductID = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductIDAttribute>()?.ProductID;
            ProductVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ReleaseDate = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyReleaseDateAttribute>()?.ReleaseDate;
            Description = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyReleaseDetailAttribute>()?.ReleaseDetail;
            Company = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)
                    .OfType<AssemblyCompanyAttribute>()
                    .FirstOrDefault()?
                    .Company;
        }
    }
}
