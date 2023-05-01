using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_AircraftTypeAndAirline
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);
            string IcaoNewValue = "LR2TT" + (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string fetchRecentAddedIdValue = (scenarioContext.ContainsKey("addedIdValue") ? scenarioContext.Get<string>("addedIdValue") : string.Empty);

            switch (sqlQueryName)
            {
                case "selectAircraftType":
                    sqlQueryToRun = "select * from resources.AircraftType";
                    break;
                case "tableAircraftWeightCategory":
                    sqlQueryToRun = "select * from [resources].[AircraftWeightCategory]";
                    break;
                case "fetchRecentAddedAircraftTypeDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "countNoOfColumnsTableAircraftType":
                    sqlQueryToRun = "select count(column_name) as Column_Count from information_schema.columns where table_name = 'AircraftType'";
                    break;
                case "fetchMaxIDAircraftTypeTable":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.AircraftType";
                    break;
                case "fetchRandomNumberGenerator":
                    sqlQueryToRun = "select distinct floor(rand() * 10000 + 1) as Random_Number_Generator";
                    break;
                case "fetchRandomNumberGenerator_UI":
                    sqlQueryToRun = "select distinct floor(rand() * 50 + 1) as Random_Number_Generator";
                    break;
                case "fetchTotalRowsAircraftTable":
                    sqlQueryToRun = "select count(*) as Row_Count from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata = 'ZRH')";
                    break;
                case "fetchAirlineData":
                    sqlQueryToRun = "select * from resources.Airline where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchRecentAddedAirlineData":
                    sqlQueryToRun = "select top 1 * from resources.Airline order by Id desc";
                    break;
                case "fetchIdDoesntExistAirline":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.Airline";
                    break;
                case "fetchRecentDeletedAirlineData":
                    sqlQueryToRun = "select * from resources.Airline where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecordDetailsForNameAirline":
                    sqlQueryToRun = "select * from resources.Airline where Name='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForIataAirline":
                    sqlQueryToRun = "select * from resources.Airline where Iata='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForIcaoAirline":
                    sqlQueryToRun = "select * from resources.Airline where Icao='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchAircraftTypeData":
                    sqlQueryToRun = "select * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchAircraftTypeDataForTXLSite":
                    sqlQueryToRun = "select * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='TXL') order by Id asc";
                    break;
                case "fetchAircraftTypeDataForAllSiteClaims":
                    sqlQueryToRun = "select * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchAirlineDataForAllSiteClaims":
                    sqlQueryToRun = "select * from resources.Airline where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchRecentAddedAircraftTypeData":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType order by Id desc";
                    break;
                case "fetchIdDoesntExistAircraftType":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.AircraftType";
                    break;
                case "fetchIdExistAircraftType":
                    sqlQueryToRun = "select top 1 Id from resources.AircraftType";
                    break;
                case "fetchRecentDeletedAircraftTypeData":
                    sqlQueryToRun = "select * from resources.AircraftType where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecordDetailsForTypeNameAircraftType":
                    sqlQueryToRun = "select * from resources.AircraftType where TypeName='" + fetchedNameValueAPI + "' and SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchRecordDetailsForIATAAircraftType":
                    sqlQueryToRun = "select * from resources.AircraftType where Iata='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForICAOAircraftType":
                    sqlQueryToRun = "select * from resources.AircraftType where Icao='" + fetchedNameValueAPI + "'";
                    break;
                case "insertNewRecordAircraftTable":
                    sqlQueryToRun = "insert into resources.AircraftType ([ICAO],[IATA],[Engine],[TypeName],[Width],[NumberOfEngines],[SizeCode],[WaketurbulenceCategoryId],[SpeedClass],[SiteId]) values('" + IcaoNewValue + "', '" + IcaoNewValue + "', 'NIL', 'PIPER, Clipper', 0, 2, 'NIL', 1, 'NIL', '" + Int32.Parse(fetchSiteIdValue) + "'); ";
                    break;
                case "fetchAircraftDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='TXL') order by Id desc";
                    break;
                case "UpdateAircraftDetailsTypeName":
                    sqlQueryToRun = "Update resources.AircraftType set ICAO = 'A111', IATA = 'A111', SiteId in (select Id from dbo.Site where Iata='TXL') where TypeName = 'Antonov Ruslan'";
                    break;
                case "insertNewRecordAirlineTable":
                    sqlQueryToRun = "insert into resources.Airline ([SiteId],[Iata],[Icao],[Name]) values('" + Int32.Parse(fetchSiteIdValue) + "','" + IcaoNewValue + "', '" + IcaoNewValue + "', 'Test Airlines'); ";
                    break;
                case "fetchAirlineDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.Airline where SiteId in (select Id from dbo.Site where Iata='TXL') order by Id desc";
                    break;
                case "fetchAircraftDetailsWithAscendingSiteId":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchAirlineDetailsWithAscendingSiteId":
                    sqlQueryToRun = "select top 1 * from resources.Airline where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchRecentAddedAircraftTypeDataForSiteIdDesc":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='TXL') order by Id desc";
                    break;
                case "fetchAircraftDetailsWithAscendingSiteIdForIATA":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where IATA is NOT NULL and SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchTop10AircraftTypeData":
                    sqlQueryToRun = "select top 10 a.Id, ISNULL(a.IATA,'') as IATA, a.ICAO, a.Engine, a.TypeName, a.NumberOfEngines, a.SizeCode, a.SpeedClass, a.Width, b.CategoryName from resources.AircraftType a INNER JOIN resources.WakeTurbulenceCategory b ON a.WakeTurbulenceCategoryId = b.Id where a.SiteId in (select Id from dbo.Site where Iata='ZRH') ORDER BY TypeName";
                    break;
                case "fetchTop1AircraftTypeData":
                    sqlQueryToRun = "select top 1 a.Id, ISNULL(a.IATA,'') as IATA, a.ICAO, a.Engine, a.TypeName, a.NumberOfEngines, a.SizeCode, a.SpeedClass, a.Width, b.CategoryName from resources.AircraftType a INNER JOIN resources.WakeTurbulenceCategory b ON a.WakeTurbulenceCategoryId = b.Id where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchTop1DescAircraftTypeData":
                    sqlQueryToRun = "select top 1 a.Id, ISNULL(a.IATA,'') as IATA, a.ICAO, a.Engine, a.TypeName, a.SizeCode, a.SpeedClass, b.CategoryName from resources.AircraftType a INNER JOIN resources.WakeTurbulenceCategory b ON a.WakeTurbulenceCategoryId = b.Id where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by TypeName desc";
                    break;
                case "fetchTop10DescAircraftTypeData":
                    sqlQueryToRun = "select q.* from (select top 10 a.Id, ISNULL(a.IATA,'') as IATA, a.ICAO, a.Engine, a.TypeName, a.NumberOfEngines, a.SizeCode, a.SpeedClass, a.Width, b.CategoryName from resources.AircraftType a INNER JOIN resources.WakeTurbulenceCategory b ON a.WakeTurbulenceCategoryId = b.Id order by Id desc) q order by q.Id asc";
                    break;
                case "deleteRecentAddedRowAircraftTable":
                    sqlQueryToRun = "delete from resources.AircraftType where Id =" + fetchSiteIdValue + ""; 
                    break;
                case "deleteRecentAddedRowAirlineTable":
                    sqlQueryToRun = "delete from resources.Airline where Id =" + fetchSiteIdValue + "";
                    break;
                case "deleteRecentAddedRowsAircraftTable":
                    sqlQueryToRun = "delete from resources.AircraftType where Id>2644";
                    break;
                case "fetchRecentAddedAircraftTypeDataWithDynamicId":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where Id=" + fetchRecentAddedIdValue + "";
                    break;
                case "updateTypeNameAircraftTable":
                    sqlQueryToRun = "update resources.AircraftType set TypeName = 'AAC, SeaStar' where TypeName in ('','A-41, VNS-41')";
                    break;
                case "fetchAircraftTypeDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 * from resources.AircraftType where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
