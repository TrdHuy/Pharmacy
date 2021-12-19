using Pharmacy.Base.Utils.Attributes;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Pharmacy")]


[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("HPSoftwareSolution")]
[assembly: AssemblyProduct("PMC")]
[assembly: AssemblyCopyright("Copyright ©  2020 Project - All Right Reserved")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//================================================================================
// Check release detail when package the product
[assembly: AssemblyReleaseDetail("Stable version 1.1.0.2")]

// Update the date when package the product
// date format dd-MM-yyyy
[assembly: AssemblyReleaseDate("20-12-2021")]

// Update the product ID when version up as guide below
// product id naming rule
// <Prod_name(short name)>_<Release_date(ddmm)>_<other_code(A-Z)>_<Version>
// Pharmacy package code: <PMC>_<Release_date(ddmm)>_<XXU>_<Version>
[assembly: AssemblyProductID("PMC2012XXU1102")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

//================================================================================

[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.0.2")]
[assembly: AssemblyFileVersion("1.1.0.2")]
