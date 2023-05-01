using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_WakeTurbulenceAndSeparation
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);           
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchWakeTurbulenceCategoryData":
                    sqlQueryToRun = "select * from resources.WakeTurbulenceCategory where Id=6";
                    break;
                case "fetchWakeTurbulenceCategoryOutput":
                    sqlQueryToRun = "select * from resources.WakeTurbulenceCategory where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "fetchRecentAddedWakeTurbulenceCategoryData":
                    sqlQueryToRun = "select top 1 * from resources.WakeTurbulenceCategory order by Id desc";
                    break;
                case "fetchIdDoesntExistWakeTurbulenceCategory":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.WakeTurbulenceCategory";
                    break;
                case "fetchRecentDeletedWakeTurbulenceCategoryData":
                    sqlQueryToRun = "select * from resources.WakeTurbulenceCategory where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecentAddedTopTwoWakeTurbulenceCategoryData":
                    sqlQueryToRun = "select * from resources.WakeTurbulenceCategory W1 where 2 > (select count(*) from resources.WakeTurbulenceCategory W2 where W2.Id > W1.Id)";
                    break;
                case "deletedWakeTurbulenceCategoryDataForTop2Rows":
                    sqlQueryToRun = "Delete from resources.WakeTurbulenceCategory where Category in ('G','H')";
                    break;
                case "insertNewRecordWakeTurbulenceCategoryTable":
                    sqlQueryToRun = "insert into resources.WakeTurbulenceCategory ([SiteId],[Category],[CategoryName]) values('" + Int32.Parse(fetchSiteIdValue) + "', 'G', 'Test')";
                    break;
                case "fetchWakeTurbulenceCategoryDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.WakeTurbulenceCategory where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "deleteWakeTurbulenceCategoryDetails":
                    sqlQueryToRun = "Delete from resources.WakeTurbulenceCategory where Category in ('G','H')";
                    break;
                case "fetchWakeSeparationTimeData":
                    sqlQueryToRun = "select * from resources.WakeSeparationTimes where SiteId in (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "deleteWakeSeparationDetails":
                    sqlQueryToRun = "Delete from resources.WakeSeparationTimes where LeaderWakeTurbulenceCategoryId in (1,2) and FollowerWakeTurbulenceCategoryId in (1,2)";
                    break;
                case "fetchRecentAddedWakeSeparationData":
                    sqlQueryToRun = "select top 1 * from resources.WakeSeparationTimes order by Id desc";
                    break;
                case "fetchIdDoesntExistWakeSeparation":
                    sqlQueryToRun = "select max(Id+1) as Id from resources.WakeSeparationTimes";
                    break;
                case "fetchRecentDeletedWakeSeparationData":
                    sqlQueryToRun = "select * from resources.WakeSeparationTimes where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchSeparationTimeForRecentAddedWakeSeparationData":
                    sqlQueryToRun = "select top 1 [SeparationSeconds] from [resources].[WakeSeparationTimes] order by Id desc";
                    break;
                case "fetchRecentAddedTopTwoWakeSeparationData":
                    sqlQueryToRun = "select * from [resources].WakeSeparationTimes W1 where 2 > (select count(*) from resources.WakeSeparationTimes W2 where W2.Id > W1.Id)";
                    break;
                case "deletedWakeSeparationDataForTop2Rows":
                    sqlQueryToRun = "delete from resources.WakeSeparationTimes where Id in (select top 2 Id from resources.WakeSeparationTimes order by Id desc)";
                    break;
                case "insertNewRecordWakeSeparationTimesTable":
                    sqlQueryToRun = "insert into resources.WakeSeparationTimes ([SiteId],[LeaderWakeTurbulenceCategoryId],[FollowerWakeTurbulenceCategoryId],[SeparationSeconds]) values('" + Int32.Parse(fetchSiteIdValue) + "', '1', '1','60')";
                    break;
                case "fetchWakeSeparationTimesDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.WakeSeparationTimes where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
