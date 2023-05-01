using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_TaxiSequenceAndSite
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);           
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string fetchNameNewValue = "Dummy" + (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string aircraftTypeId = (scenarioContext.ContainsKey("outputSQLResponse") ? scenarioContext.Get<string>("outputSQLResponse") : string.Empty);
            string taxiSequenceId = (scenarioContext.ContainsKey("outputSQLResponseData") ? scenarioContext.Get<string>("outputSQLResponseData") : string.Empty);

            switch (sqlQueryName)
            { 
                case "fetchTaxiSequenceDataForAllSiteClaims":
                    sqlQueryToRun = "select * from resources.TaxiSequence where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchSiteName":
                    sqlQueryToRun = "select top 1 Name as Name from dbo.Site where Id in (select Id from dbo.Site where Iata = 'ZRH')";
                    break;
                case "fetchIdDoesntExistSite":
                    sqlQueryToRun = "select max(Id+1) as Id from dbo.Site";
                    break;
                case "fetchSiteIdAscending":
                    sqlQueryToRun = "select top 1 Id as Id from dbo.Site where Iata='ZRH'";
                    break;
                case "fetchSiteIdDescending":
                    sqlQueryToRun = "select top 1 Id as Id from dbo.Site where Id in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchSiteIdSpecificValue":
                    sqlQueryToRun = "select top 1 Id as Id from dbo.Site where Id in (select Id from dbo.Site where Iata='MGL')";
                    break;
                case "fetchSiteData":
                    sqlQueryToRun = "select * from dbo.Site";
                    break;
                case "fetchRecentAddedSiteData":
                    sqlQueryToRun = "select top 1 * from dbo.Site order by Id desc";
                    break;
                case "fetchRecordDetailsForNameSite":
                    sqlQueryToRun = "select * from dbo.Site where Name='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForIataSite":
                    sqlQueryToRun = "select * from dbo.Site where Iata='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForIcaoSite":
                    sqlQueryToRun = "select * from dbo.Site where Icao='" + fetchedNameValueAPI + "'";
                    break;
                case "insertNewRecordSiteTable":
                    sqlQueryToRun = "insert into dbo.Site ([Iata],[Icao],[Name],[IanaTimezone],[MsTimezone]) values('MGL', 'EDLN', 'Mönchengladbach Airport', 'Europe/Berlin', 'W. Europe Standard Time'); ";
                    break;
                case "fetchRecentDeletedSiteData":
                    sqlQueryToRun = "select * from dbo.Site where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchTaxiSequenceData":
                    sqlQueryToRun = "select * from resources.TaxiSequence where SiteId in (select Id from dbo.Site where Iata='MGL') order by Id asc";
                    break;
                case "fetchRecentAddedTaxiSequenceDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 * from resources.TaxiSequence where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistTaxiSequence":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.TaxiSequence";
                    break;
                case "fetchTaxiSequenceId":
                    sqlQueryToRun = "select top 1 * from resources.TaxiSequence";
                    break;
                case "insertNewRecordTaxiSequenceTable":
                    sqlQueryToRun = "insert into resources.TaxiSequence ([SiteId],[SequenceName],[RunwayId],[LineUpId]) values('" + Int32.Parse(fetchSiteIdValue) + "', '" + fetchNameNewValue + "', '" + Int32.Parse(aircraftTypeId) + "','" + Int32.Parse(taxiSequenceId) + "'); ";
                    break;
                case "fetchRecentDeletedTaxiSequenceData":
                    sqlQueryToRun = "select * from resources.TaxiSequence where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchTaxiSequenceDataWithRunwayAndStandId":
                    sqlQueryToRun = "select * from resources.taxisequence where Id in (select TaxiSequenceId from resources.TaxiStands where StandId =" + fetchSiteIdValue + ")";
                    break;
                case "fetchTaxiSequenceDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.TaxiSequence where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchRecentAddedTaxiSequenceData":
                    sqlQueryToRun = "select top 1 * from resources.TaxiSequence order by Id desc";
                    break;
                case "insertNewRecordTaxiSequenceTableForNullLineUpId":
                    sqlQueryToRun = "insert into resources.TaxiSequence ([SiteId],[SequenceName],[RunwayId],[LineUpId]) values('" + Int32.Parse(fetchSiteIdValue) + "', '" + fetchNameNewValue + "', '" + Int32.Parse(aircraftTypeId) + "',NULL); ";
                    break;
                case "fetchTaxiSequenceIdForRunwayAndSite":
                    sqlQueryToRun = "select top 1 Id from resources.TaxiSequence where RunwayId in (select Id from resources.Runway where Name like 'test') and SiteId=(select top 1 Id as Id from dbo.Site where Iata='ZRH')";
                    break;
                case "insertNewRecordTaxiStandsTable":
                    sqlQueryToRun = "insert into resources.TaxiStands ([TaxiSequenceId],[StandId]) values('" + fetchSiteIdValue + "', '21')";
                    break;
                case "deleteTaxiStandsData":
                    sqlQueryToRun = "delete from resources.TaxiStands";
                    break;
                case "updateNameForSiteData":
                    sqlQueryToRun = "Update dbo.Site set Name = 'Test Munich International Airport' where Id=" + fetchSiteIdValue + "";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
