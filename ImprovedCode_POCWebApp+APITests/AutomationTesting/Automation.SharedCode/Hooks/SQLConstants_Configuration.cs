using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_Configuration
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string fetchNameNewValue = "Dummy" + (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            
            switch (sqlQueryName)
            {
                case "fetchConfigurationData":
                    sqlQueryToRun = "select * from config.Configuration where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id";
                    break;
                case "fetchRecentAddedConfigurationData":
                    sqlQueryToRun = "select top 1 * from config.Configuration where SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchRecentDeletedConfigurationData":
                    sqlQueryToRun = "select * from config.Configuration where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchConfigurationDataForAllSiteClaims":
                    sqlQueryToRun = "select * from config.Configuration where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "insertNewRecordConfigurationTable":
                    sqlQueryToRun = "insert into config.Configuration ([SiteId],[Name],[Value],[Description],[System],[Group]) values('" + Int32.Parse(fetchSiteIdValue) + "', '" + fetchNameNewValue + "', 'DummyValue', 'This is dummy description', 'DMAN', 'Test'); ";
                    break;
                case "fetchRecordDetailsForNameConfiguration":
                    sqlQueryToRun = "select * from config.Configuration where Name='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchRecordDetailsForSystemConfiguration":
                    sqlQueryToRun = "select * from config.Configuration where System='" + fetchedNameValueAPI + "' and SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id asc";
                    break;
                case "fetchRecordDetailsForSystemAndGroup":
                    sqlQueryToRun = "select * from config.Configuration where System='" + (scenarioContext.Get<string>("systemValueDB")) + "' and [Group]='" + (scenarioContext.Get<string>("typeNameValueDB")) + "' and SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchConfigurationDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from config.Configuration where SiteId = (select top 1 Id as Id from dbo.Site where Iata='TXL')";
                    break;
                case "fetchConfigurationDetailsWithDesSiteId":
                    sqlQueryToRun = "select top 1 * from config.Configuration order by Id desc";
                    break;
                case "countNoOfRowsConfigurationTable":
                    sqlQueryToRun = "select count(*) as Row_Count from config.Configuration where SiteId = (select Id from dbo.Site where Iata='ZRH')";
                    break;
                case "countNoOfColumnsConfigurationTable":
                    sqlQueryToRun = "select count(column_name) as Column_Count from information_schema.columns where table_name = 'Configuration'";
                    break;
                case "fetchMaxIDConfigurationTable":
                    sqlQueryToRun = "select max(Id+1) as Id from config.Configuration";
                    break;
                case "fetchConfigurationDataWithTrimSpaces":
                    sqlQueryToRun = "select [Id],TRIM(Name) as Name,TRIM(Value) as [Value],TRIM(Description) as [Description],TRIM(System) as [System],[Group] from config.Configuration where SiteId in (select Id from dbo.Site where Iata='ZRH') ORDER BY Name";
                    break;
                case "fetchTop10ConfigurationData":
                    sqlQueryToRun = "select q.* from (select top 10 Id,Name, Value,Description, System, [Group] from config.Configuration order by Id desc) q order by q.Id asc";
                    break;
                case "deleteRecentAddedRowsConfigurationTable":
                    sqlQueryToRun = "delete from [config].Configuration where Id>2786";
                    break;
                case "fetchIdDoesntExist":
                    sqlQueryToRun = "select max(Id+1) as Id from config.Configuration";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}

