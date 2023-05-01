using Acdm.InformationServices.Dto;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_SidAndSidSeparation
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            //string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);
            //string IcaoNewValue = "LR2TT" + (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
           // string fetchRecentAddedIdValue = (scenarioContext.ContainsKey("addedIdValue") ? scenarioContext.Get<string>("addedIdValue") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchSidDataForDynamicSite":
                    sqlQueryToRun = "select * from resources.Sid where SiteId = " + fetchSiteIdValue + " order by SidFullName";
                    break;
                case "insertSidDataForTXLSite":
                    sqlQueryToRun = "insert into resources.Sid values (30,'AAA','" + fetchSiteIdValue + "','ABC')";
                    break;
                case "fetchTop1SidShortNameForDynamicSite":
                    sqlQueryToRun = "select top 1 SidShortName as SidShortName from resources.Sid where SiteId = " + fetchSiteIdValue + " order by SidFullName";
                    break;
                case "fetchRecordDetailsForShortNameSid":
                    sqlQueryToRun = "select * from resources.Sid where SidShortName='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchSidDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from resources.Sid where SiteId in (select Id from dbo.Site where Iata='TXL') order by SidFullName";
                    break;
                case "deleteRecentAddedSidShortName":
                    sqlQueryToRun = "delete from resources.Sid where SidShortName = 'AAA'";
                    break;
                case "fetchTop1SidFullNameForDynamicSite":
                    sqlQueryToRun = "select top 1 SidFullName as SidFullName from resources.Sid where SiteId = " + fetchSiteIdValue + " order by SidFullName";
                    break;
                case "fetchRecordDetailsForFullNameSid":
                    sqlQueryToRun = "select * from resources.Sid where SidFullName='" + fetchedNameValueAPI + "'";
                    break;
                case "fetchSidSeparationDataForDynamicSite":
                    sqlQueryToRun = "select a.SiteId, a.LeaderSid, a.FollowerSid, a.Condition, a.ExtraTimeInMinToBeAddedToSpeedMatrix, b.SidShortName as LeaderSidSidShortName, b.SidFullName as LeaderSidSidFullName, c.SidShortName as FollowerSidSidShortName, c.SidFullName as FollowerSidSidFullName from resources.SidSeparationMatrix a join resources.Sid b on a.LeaderSid = b.SidShortName join resources.Sid c on a.FollowerSid = c.SidShortName";
                    break;
                case "fetchSpeedSeparationDataForDynamicSite":
                    sqlQueryToRun = "select * from resources.SpeedMatrix where SiteId = " + fetchSiteIdValue + "";
                    break;
                case "fetchRecentAddedSidShortName":
                    sqlQueryToRun = "select * from resources.Sid where SidShortName = 'AAA'";
                    break;
                case "fetchTop1SidDetailsWithRunway":
                    sqlQueryToRun = "select a.SiteId, a.SidShortName, a.SidFullName, b.Id, b.Name, b.DependencyGroup, b.OppositeRunway from resources.Sid a join resources.Runway b on a.Runway = b.Id and a.SidShortName = (select top 1 SidShortName from resources.Sid)";
                    break;
                case "fetchTop1SidDetailsWithExistingRunway":
                    sqlQueryToRun = "select a.SiteId, a.SidShortName, a.SidFullName, b.Id, b.Name, b.DependencyGroup, b.OppositeRunway from resources.Sid a join resources.Runway b on a.Runway = b.Id and a.SidShortName = 'AAA'";
                    break;
                case "fetchRecentAddedSidSeparationData":
                    sqlQueryToRun = "select a.SiteId, a.LeaderSid, a.FollowerSid, a.Condition, a.ExtraTimeInMinToBeAddedToSpeedMatrix, b.SidShortName as LeaderSidSidShortName, b.SidFullName as LeaderSidSidFullName, c.SidShortName as FollowerSidSidShortName, c.SidFullName as FollowerSidSidFullName from resources.SidSeparationMatrix a join resources.Sid b on a.LeaderSid = b.SidShortName join resources.Sid c on a.FollowerSid = c.SidShortName where a.LeaderSid = 'AAA'";
                    break;
                case "deleteRecentAddedSidSeparation":
                    sqlQueryToRun = "delete from resources.SidSeparationMatrix where LeaderSid = 'AAA'";
                    break;
                case "fetchTop1SpeedMatrix":
                    sqlQueryToRun = "select top 1 * from resources.SpeedMatrix where SiteId = 1";
                    break;
                case "fetchRecentAddedSpeedSeparationData":
                    sqlQueryToRun = "select top 1 * from resources.SpeedMatrix where LeaderSpeedClass = 'V_C172' and SeparationSeconds = 150";
                    break;
                case "deleteRecentAddedSpeedSeparationData":
                    sqlQueryToRun = "delete from resources.SpeedMatrix where LeaderSpeedClass = 'V_C172' and SeparationSeconds = 150";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
