﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACDMAutomation.Shared.Hooks {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SQLQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SQLQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ACDMAutomation.Shared.Hooks.SQLQueries", typeof(SQLQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///DECLARE @Counter int
        ///DECLARE @ValueCounter int
        ///DECLARE @arrivalId int
        ///DECLARE @SiteId int
        ///DECLARE @OperationDate datetime
        ///SET @SiteId = {siteId}
        ///SET @OperationDate = &apos;2023-01-26&apos;
        ///SET @Counter = 0
        ///
        ///WHILE @Counter &lt; 100
        ///BEGIN
        ///	
        ///	IF NOT EXISTS (Select 1 from flight.Arrival where SiteId =@SiteId and Carrier = &apos;DL&apos; and FlightNumber = &apos;DL&apos; + LTRIM(Str(@Counter)) and OperationDate = @OperationDate) 
        ///	BEGIN 
        ///
        ///		INSERT INTO flight.Arrival (SiteId, Carrier, FlightNumber, Origin, OperationDate, Aircra [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddArrFlightTestData {
            get {
                return ResourceManager.GetString("AddArrFlightTestData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///DECLARE @Counter int
        ///DECLARE @ValueCounter int
        ///DECLARE @departureId int
        ///DECLARE @SiteId int
        ///DECLARE @OperationDate datetime
        ///SET @SiteId = {siteId}
        ///SET @OperationDate = &apos;2023-01-26&apos;
        ///SET @Counter = 0
        ///
        ///WHILE @Counter &lt; 100
        ///BEGIN
        ///	
        ///	IF NOT EXISTS (Select 1 from flight.Departure where SiteId =@SiteId and Carrier = &apos;DL&apos; and FlightNumber = &apos;DL&apos; + LTRIM(Str(@Counter)) and OperationDate = @OperationDate) 
        ///	BEGIN 
        ///
        ///		INSERT INTO flight.Departure (SiteId, Carrier, FlightNumber, Destination, OperationD [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddDepFlightTestData {
            get {
                return ResourceManager.GetString("AddDepFlightTestData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///DECLARE @Counter int
        ///DECLARE @ValueCounter int
        ///DECLARE @flightPlanId int
        ///DECLARE @SiteId int
        ///DECLARE @OperationDate datetime
        ///SET @SiteId = {siteId}
        ///SET @OperationDate = &apos;2023-03-07&apos;
        ///SET @Counter = 0
        ///
        ///WHILE @Counter &lt; 100
        ///BEGIN
        ///	
        ///	IF NOT EXISTS (Select 1 from flight.UnmatchedFlightPlan where SiteId =@SiteId and Carrier = &apos;DL&apos; and FlightNumber = &apos;DL&apos; + LTRIM(Str(@Counter)) and OperationDate = @OperationDate) 
        ///	BEGIN 	
        ///		INSERT INTO flight.UnmatchedFlightPlan (SiteId, Carrier, FlightNumber, Ori [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddFlightPlanTestData {
            get {
                return ResourceManager.GetString("AddFlightPlanTestData", resourceCulture);
            }
        }
    }
}
