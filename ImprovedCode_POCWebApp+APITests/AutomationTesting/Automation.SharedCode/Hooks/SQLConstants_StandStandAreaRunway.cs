using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_StandStandAreaRunway
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);           
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string fetchNameNewValue = "Dummy" + (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string aircraftTypeId = (scenarioContext.ContainsKey("outputSQLResponse") ? scenarioContext.Get<string>("outputSQLResponse") : string.Empty);
            
            switch (sqlQueryName)
            {
                case "fetchStandDataForAllSiteClaims":
                    sqlQueryToRun = "select * from resources.Stand where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and StandAreaId in (select Id from resources.StandArea where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')))";
                    break;
                case "fetchRunwayDataForAllSiteClaims":
                    sqlQueryToRun = "select * from resources.Runway where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchStandData":
                    sqlQueryToRun = "select * from resources.Stand where SiteId in (select Id from dbo.Site where Iata='ZRH') and StandareaId in (select Id from resources.StandArea where siteId in (select Id from dbo.Site where Iata='ZRH'))";
                    break;
                case "fetchRecentAddedStandData":
                    sqlQueryToRun = "select top 1 * from resources.Stand order by Id desc";
                    break;
                case "fetchRecentAddedStandDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 * from resources.Stand where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistStand":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.Stand";
                    break;
                case "fetchRecordDetailsForStandName":
                    sqlQueryToRun = "select * from resources.Stand where Name='" + fetchedNameValueAPI + "' and SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "insertNewRecordStandTable":
                    sqlQueryToRun = "insert into resources.Stand ([SiteId],[Name],[StandAreaId]) values('" + Int32.Parse(fetchSiteIdValue) + "', '" + fetchNameNewValue + "', '" + Int32.Parse(aircraftTypeId) + "'); ";
                    break;
                case "fetchStandAreaId":
                    sqlQueryToRun = "select top 1 Id from resources.StandArea where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchRecentDeletedStandData":
                    sqlQueryToRun = "select * from resources.Stand where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "insertNewRecordStandAreaTable":
                    sqlQueryToRun = "insert into resources.StandArea ([SiteId],[Name]) values('" + Int32.Parse(fetchSiteIdValue) + "', '" + fetchNameNewValue + "'); ";
                    break;
                case "fetchRecentAddedStandAreaData":
                    sqlQueryToRun = "select top 1 * from resources.StandArea where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchStandAreaDetailsWithAscendingSiteId":
                    sqlQueryToRun = "select top 1 * from resources.StandArea where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchRecentAddedTopTwoStandData":
                    sqlQueryToRun = "select * from resources.Stand S1 where 2 > (select count(*) from resources.Stand S2 where S2.Id > S1.Id)";
                    break;
                case "fetchStandDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.Stand where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchStandAreaDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.StandArea where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchRecentAddedStandAreaDataForSiteIdDesc":
                    sqlQueryToRun = "select top 1 * from resources.StandArea where SiteId in (select Id from dbo.Site where Iata='TXL') order by Id desc";
                    break;
                case "fetchRunwayData":
                    sqlQueryToRun = "select * from resources.Runway where SiteId in (select Id from dbo.Site where Iata='MGL')";
                    break;
                case "fetchRecentAddedRunwayDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 * from resources.Runway where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistRunway":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.Runway";
                    break;
                case "fetchRecordDetailsForNameRunway":
                    sqlQueryToRun = "select * from resources.Runway where Name='" + fetchedNameValueAPI + "'";
                    break;
                case "insertNewRecordRunwayTable":
                    sqlQueryToRun = "insert into resources.Runway ([SiteId],[Name],[DependencyGroup]) values('" + Int32.Parse(fetchSiteIdValue) + "','" + fetchNameNewValue + "', 1); ";
                    break;
                case "fetchRecentDeletedRunwayData":
                    sqlQueryToRun = "select * from resources.Runway where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecentAddedRunwayData":
                    sqlQueryToRun = "select top 1 * from resources.Runway order by Id desc";
                    break;
                case "fetchRunwayDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.Runway where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchRunwayNameForTaxiSequence":
                    sqlQueryToRun = "select top 1 Name as RunwayName,* from resources.Runway where Id in (select RunwayId from resources.TaxiSequence where Id in (select TaxiSequenceId from resources.TaxiStands) and SiteId in (select Id from dbo.Site where Iata='ZRH'))";
                    break;
                case "fetchStandNameForTaxiSequence":
                    sqlQueryToRun = "select Name as StandName,* from [resources].[Stand] where Id in (select StandId from[resources].TaxiStands where TaxiSequenceId in (select Id from[resources].TaxiSequence where SiteId in (select Id from dbo.Site where Iata='ZRH')))";
                    break;
                case "fetchRunwayId":
                    sqlQueryToRun = "select top 1 * from resources.Runway";
                    break;
                case "fetchDistinctStandData":
                    sqlQueryToRun = "select top 1 * from resources.Stand where SiteId in (select Id from dbo.Site where Iata='ZRH') and StandAreaId = 1 order by Id desc";
                    break;
                case "fetchRunwayDetails":
                    sqlQueryToRun = "select * from resources.Runway where Name like 'Test'";
                    break;
                case "fetchRecentAddedStandDataForStandAreaId":
                    sqlQueryToRun = "select top 1 * from resources.Stand where StandAreaId=1 order by Id desc";
                    break;
                case "fetchStandIdForTaxiSequence":
                    sqlQueryToRun = "select Id as StandId,* from [resources].[Stand] where Id in (select StandId from [resources].TaxiStands where TaxiSequenceId in (select Id from [resources].TaxiSequence where SiteId in (select Id from dbo.Site where Iata='ZRH')))";
                    break;
                case "deletedStandDataForTop2Rows":
                    sqlQueryToRun = "delete from resources.Stand where Id in (select top 2 Id from resources.Stand order by Id desc)";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
