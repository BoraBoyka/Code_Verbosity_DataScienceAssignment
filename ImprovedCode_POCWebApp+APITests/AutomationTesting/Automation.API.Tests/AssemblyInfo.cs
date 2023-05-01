using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.


//[assembly: AssemblyTitle("APVeAutomation")]
//[assembly: AssemblyDescription("APVeAutomation")]
//[assembly: AssemblyCopyright("Copyright ©  2016")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]
[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.ClassLevel)]

[assembly: ComVisible(false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("fe62837f-5f79-4140-829a-c415b5c4c00a")]