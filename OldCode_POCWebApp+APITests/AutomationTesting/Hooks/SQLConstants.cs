using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACDM.Bindings.Hooks
{
    public class SQLConstants
    {
        static string sqlQueryToRun;
        public static string SQLQuery(string sqlQueryName)
        {
            string dynamicValueId = ACDMAutomation.Steps.AircraftTypeSteps.textElementUI;
            string deletedRecordId = ACDMAutomation.Steps.CrudAPIBaseMethods<ConfigurationDto>.idValueDB;

            string fetchedSystemValueConfiguration = ACDMAutomation.Steps.CrudAPIBaseMethods<ConfigurationDto>.systemValueDB;
            string fetchedGroupValueConfiguration = ACDMAutomation.Steps.CrudAPIBaseMethods<ConfigurationDto>.typeNameValueDB;
            string fetchedNameValueAirline = ACDMAutomation.Steps.CrudAPIBaseMethods<AirlineDto>.typeNameValueDB;
            string fetchedIataValueAirline = ACDMAutomation.Steps.CrudAPIBaseMethods<AirlineDto>.typeNameValueDB;
            string fetchedIcaoValueAirline = ACDMAutomation.Steps.CrudAPIBaseMethods<AirlineDto>.typeNameValueDB;
            string fetchedNameValueConfiguration = ACDMAutomation.Steps.CrudAPIBaseMethods<ConfigurationDto>.typeNameValueDB;
            string deletedRecordAirlineId = ACDMAutomation.Steps.CrudAPIBaseMethods<AirlineDto>.idValueDB;
            string deletedRecordAircraftTypeId = ACDMAutomation.Steps.CrudAPIBaseMethods<AircraftTypeDto>.idValueDB;
            string fetchedTypeNameValueAircraftType = ACDMAutomation.Steps.CrudAPIBaseMethods<AircraftTypeDto>.typeNameValueDB;
            string fetchedIATAValueAircraftType = ACDMAutomation.Steps.CrudAPIBaseMethods<AircraftTypeDto>.typeNameValueDB;
            string fetchedICAOValueAircraftType = ACDMAutomation.Steps.CrudAPIBaseMethods<AircraftTypeDto>.typeNameValueDB;
            string deletedRecordFlightId = ACDMAutomation.Steps.CrudAPIFlightSteps.IdValueDeletedDB;
            string fetchedFlightNumber = ACDMAutomation.Steps.CrudAPIFlightSteps.ColNameValueDB;
            string fetchedCallSignNumber = ACDMAutomation.Steps.CrudAPIFlightSteps.ColNameValueDB;
            string IcaoNewValue = "LR2T" + ACDMAutomation.Steps.AircraftTypeSteps.valueRandomNumber;
            string siteId = ACDMAutomation.Steps.CrudAPIAircraftTypeSteps.valueSQLResponse;
            string NameNewValue = "DummyName" + ACDMAutomation.Steps.AircraftTypeSteps.valueRandomNumber;

            switch (sqlQueryName)
            {
                case "selectAircraftType":
                    sqlQueryToRun = "select * from [ACDMData].dbo.AircraftType";
                    break;
                case "selectTableAircraftType":
                    sqlQueryToRun = "select * from [ACDMData].dbo.AircraftType where Id=" + dynamicValueId + "";
                    break;
                case "tableAircraftWeightCategory":
                    sqlQueryToRun = "select * from [ACDMData].[dbo].[AircraftWeightCategory]";
                    break;
                case "countNoOfColumnsTableAircraftType":
                    sqlQueryToRun = "select count(column_name) as Column_Count from information_schema.columns where table_name = 'AircraftType'";
                    break;
                case "fetchMaxIDAircraftTypeTable":
                    sqlQueryToRun = "select max(Id+1) as Id from [ACDMData].dbo.AircraftType";
                    break;
                case "fetchRandomNumberGenerator":
                    sqlQueryToRun = "select distinct floor(rand() * 1000 + 1) as Random_Number_Generator";
                    break;
                case "fetchTotalRows":
                    sqlQueryToRun = "select count(*) as totalRows from [ACDMData].dbo.AircraftType";
                    break;
                case "fetchConfigurationData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Configuration where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site)";
                    break;
                case "fetchRecentAddedConfigurationData":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.Configuration order by Id desc";
                    break;
                case "fetchRecentDeletedConfigurationData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Configuration where Id=" + deletedRecordId + "";
                    break;
                case "fetchIdDoesntExist":
                    sqlQueryToRun = "select max(Id+1) as Id from [acdm-data].dbo.Configuration";
                    break;
                case "fetchAirlineData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Airline where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site)";
                    break;
                case "fetchRecentAddedAirlineData":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.Airline order by Id desc";
                    break;
                case "fetchIdDoesntExistAirline":
                    sqlQueryToRun = "select max(Id+1) as Id from [acdm-data].dbo.Airline";
                    break;
                case "fetchRecentDeletedAirlineData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Airline where Id=" + deletedRecordAirlineId + "";
                    break;
                case "fetchRecordDetailsForNameAirline":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Airline where Name='" + fetchedNameValueAirline + "'";
                    break;
                case "fetchRecordDetailsForIataAirline":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Airline where Iata='" + fetchedIataValueAirline + "'";
                    break;
                case "fetchRecordDetailsForIcaoAirline":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Airline where Icao='" + fetchedIcaoValueAirline + "'";
                    break;
                case "fetchAircraftTypeData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.AircraftType where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site)";
                    break;
                case "fetchRecentAddedAircraftTypeData":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.AircraftType order by Id desc";
                    break;
                case "fetchIdDoesntExistAircraftType":
                    sqlQueryToRun = "select max(Id+1) as Id from [acdm-data].dbo.AircraftType";
                    break;
                case "fetchIdExistAircraftType":
                    sqlQueryToRun = "select top 1 Id from [acdm-data].dbo.AircraftType";
                    break;
                case "fetchRecentDeletedAircraftTypeData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.AircraftType where Id=" + deletedRecordAircraftTypeId + "";
                    break;
                case "fetchRecordDetailsForTypeNameAircraftType":
                    sqlQueryToRun = "select * from [acdm-data].dbo.AircraftType where TypeName='" + fetchedTypeNameValueAircraftType + "' and SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site)";
                    break;
                case "fetchRecordDetailsForIATAAircraftType":
                    sqlQueryToRun = "select * from [acdm-data].dbo.AircraftType where Iata='" + fetchedIATAValueAircraftType + "'";
                    break;
                case "fetchRecordDetailsForICAOAircraftType":
                    sqlQueryToRun = "select * from [acdm-data].dbo.AircraftType where Icao='" + fetchedICAOValueAircraftType + "'";
                    break;
                case "fetchRecentAddedFlightData":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.Flight order by FlightId desc";
                    break;
                case "fetchRecentAddedTopTwoFlightData":
                    sqlQueryToRun = "select * from Flight F1 where 2 > (select count(*) from [acdm-data].dbo.Flight F2 where F2.FlightId > F1.FlightId)";
                    break;
                case "fetchFlightData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Flight";
                    break;
                case "fetchIdDoesntExistFlight":
                    sqlQueryToRun = "select max(FlightId+1) as FlightId from [acdm-data].dbo.Flight";
                    break;
                case "fetchRecentDeletedFlightData":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Flight where FlightId=" + deletedRecordFlightId + "";
                    break;
                case "fetchRecordDetailsForFlightNumber":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Flight where FlightNumber='" + fetchedFlightNumber + "'";
                    break;
                case "fetchRecordDetailsForCallSign":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Flight where CallSign='" + fetchedCallSignNumber + "'";
                    break;
                case "fetchRecordDetailsForTargetOffBlockTime":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Flight where TargetOffBlockTime BETWEEN '2022-05-01' AND '2022-05-08'";
                    break;
                case "fetchSiteName":
                    sqlQueryToRun = "select top 1 Name as Name from [acdm-data].dbo.Site where Id = 20";
                    break;
                case "insertNewRecordAircraftTable":
                    sqlQueryToRun = "insert into [acdm-data].dbo.AircraftType ([ICAO],[IATA],[Engine],[TypeName],[Width],[NumberOfEngines],[SizeCode],[Wvc],[SpeedClass],[SiteId]) values('" + IcaoNewValue + "', '" + IcaoNewValue + "', 'NIL', 'PIPER, Clipper', 0, 2, 'NIL', 'L', 'NIL', '" + Int32.Parse(siteId) + "'); ";
                    break;
                case "fetchIdDoesntExistSite":
                    sqlQueryToRun = "select max(Id+1) as Id from [acdm-data].dbo.Site";
                    break;
                case "fetchAircraftDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.AircraftType where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site order by Id desc)";
                    break;
                case "fetchSiteIdAscending":
                    sqlQueryToRun = "select top 1 Id as Id from [acdm-data].dbo.Site";
                    break;
                case "fetchSiteIdDescending":
                    sqlQueryToRun = "select top 1 Id as Id from [acdm-data].dbo.Site order by Id desc";
                    break;
                case "insertNewRecordAirlineTable":
                    sqlQueryToRun = "insert into [acdm-data].dbo.Airline ([SiteId],[Iata],[Icao],[Name]) values('" + Int32.Parse(siteId) + "','" + IcaoNewValue + "', '" + IcaoNewValue + "', 'Test Airlines'); ";
                    break;
                case "fetchAirlineDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.Airline where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site order by Id desc)";
                    break;
                case "insertNewRecordConfigurationTable":
                    sqlQueryToRun = "insert into [acdm-data].dbo.Configuration ([SiteId],[Name],[Value],[Description],[System],[Group]) values('" + Int32.Parse(siteId) + "', '" + NameNewValue + "', 'DummyValue', 'This is dummy description', 'DMAN', 'Test'); ";
                    break;
                case "fetchRecordDetailsForNameConfiguration":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Configuration where Name='" + fetchedNameValueConfiguration + "'";
                    break;
                case "fetchRecordDetailsForSystemConfiguration":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Configuration where System='" + fetchedGroupValueConfiguration + "' and SiteId in (select top 1 Id as Id from[acdm-data].dbo.Site order by Id asc)";
                    break;
                case "fetchRecordDetailsForSystemAndGroup":
                    sqlQueryToRun = "select * from [acdm-data].dbo.Configuration where System='" + fetchedSystemValueConfiguration + "' and [Group]='" + fetchedGroupValueConfiguration + "' and SiteId in (select top 1 Id as Id from[acdm-data].dbo.Site order by Id asc)";
                    break;
                case "fetchConfigurationDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from [acdm-data].dbo.Configuration where SiteId in (select top 1 Id as Id from [acdm-data].dbo.Site order by Id desc)";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
