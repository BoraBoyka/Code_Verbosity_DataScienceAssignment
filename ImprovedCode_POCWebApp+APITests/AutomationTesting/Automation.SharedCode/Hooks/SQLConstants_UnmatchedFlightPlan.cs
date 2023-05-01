using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_UnmatchedFlightPlan
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchSiteIdForIata = (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string aircraftTypeId = (scenarioContext.ContainsKey("outputSQLResponse") ? scenarioContext.Get<string>("outputSQLResponse") : string.Empty);
            string siteIata = (scenarioContext.ContainsKey("valueSiteId") ? scenarioContext.Get<string>("valueSiteId") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchUnmatchedFlightPlanData":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.EstimatedOffBlockDate, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id";
                    break;
                case "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.EstimatedOffBlockDate, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistUnmatchedFlightPlan":
                    sqlQueryToRun = "select max(Id+1) as unmatchedFlightPlanId from flight.UnmatchedFlightPlan";
                    break;
                case "fetchUnmatchedFlightPlanDetailsWithDesSiteId":
                    sqlQueryToRun = "select top 1 * from flight.UnmatchedFlightPlan where SiteId in (select top 1 Id as Id from dbo.Site where Iata='TXL')";
                    break;
                case "updateOperDateUnmatchedFlightPlanTable":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set OperationDate='2023-03-08' where Id in (select top 1 Id from flight.UnmatchedFlightPlan)";
                    break;
                case "updateOperDateFlightPlan":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set OperationDate='2023-03-07' where Id in (select top 1 Id from flight.UnmatchedFlightPlan)";
                    break;
                case "fetchRecordDetailsForOpDateUnmatchedFlightPlan":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, a.EstimatedOffBlockDate, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-03-08' order by Id";
                    break;
                case "fetchUnmatchedFlightPlanDataForAllSiteClaims":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, a.EstimatedOffBlockDate, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id";
                    break;
                case "fetchRecentAddedUnmatchedFlightPlanDataWithSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, a.EstimatedOffBlockDate, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select top 1 Id as Id from dbo.Site where Iata='MGL') order by Id";
                    break;
                case "fetchAllUnmatchedFlightPlanDataWithFlightPlanId":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.EstimatedOffBlockDate, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate,ISNULL(a.FlightRule,'Unknown') as FlightRule, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') and OperationDate = '2023-03-08' and a.FlightPlanId IS NOT NULL order by Id";
                    break;
                case "fetchRecentDeletedUnmatchedFlightPlan":
                    sqlQueryToRun = "select * from flight.UnmatchedFlightPlan where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecentAddedUnmatchedFlightPlanData":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule,'Unknown') as FlightRule, a.EstimatedOffBlockDate, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[UnmatchedFlightPlan] a left outer join [flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId order by Id desc";
                    break;
                case "fetchUnmatchedFlightPlanDataWithFlightPlanId":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.EstimatedOffBlockDate, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate,ISNULL(a.FlightRule, 'Unknown') as FlightRule, b.FlightPlanId as FlightPlanValueId, b.Field, b.Instance, b.Value from[flight].[UnmatchedFlightPlan] a left outer join[flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId where a.SiteId in (select Id from dbo.Site where Iata = 'ZRH') and OperationDate = '2023-03-08' and a.FlightPlanId = 'AT0894970' order by Id";
                    break;
                case "fetchRecordDetailsForUnmatchedDepFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select top 1 Id as Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-01-26' and (a.CallSign IS NULL or a.CallSign ='') order by Id";
                    break;
                case "addDataFlightPlanTable":
                    sqlQueryToRun = SQLQueries.AddFlightPlanTestData;
                    sqlQueryToRun = sqlQueryToRun.Replace("{siteId}", fetchSiteIdForIata);
                    break;
                case "fetchSiteZRHForTestData":
                    sqlQueryToRun = "select Id from dbo.Site where Iata='" + siteIata + "'";
                    break;
                case "updateMGLSiteForUnmatchedFlightPlan":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set SiteId = (select Id from dbo.Site where Iata = '" + siteIata + "') where FlightPlanId in ('AT08949730','AT08949731','AT08949732','AT08949733','AT08949734','AT08949735')";
                    break;
                case "updateTXLSiteForUnmatchedFlightPlan":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set SiteId = (select Id from dbo.Site where Iata = '" + siteIata + "') where FlightPlanId in ('AT08949770')";
                    break;
                case "insertNewRecordUnmatchedFlightPlan":
                    sqlQueryToRun = "insert into flight.UnmatchedFlightPlan (SiteId, FlightPlanId, CallSign, ArcId, Carrier, FlightNumber, Origin, Destination, OperationDate, EstimatedOffBlockDate, AircraftRegistrationId, EstimatedOffBlockTime, AircraftType) Values('" + Int32.Parse(fetchSiteIdValue) + "','AT0894977','Test77',1,'DL', 'DL111', 'DAY', 'CVG', '2023-03-08',  '2023-03-08',1, GETDATE(),'" + Int32.Parse(aircraftTypeId) + "')";
                    break;
                case "updateZRHSiteForUnmatchedFlightPlan":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set SiteId = (select Id from dbo.Site where Iata = '" + siteIata + "') where FlightPlanId in ('AT08949770')";
                    break;
                case "fetchRecentAddedUnmatchedFlightPlanDataWithoutFieldValue":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.FlightPlanId, a.CallSign, a.Origin, a.Destination, a.EstimatedOffBlockTime, a.AircraftRegistrationId, a.ArcId, a.Carrier, a.FlightNumber,  a.AircraftType, a.OperationDate, ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.EstimatedOffBlockDate,b.FlightPlanId as FlightPlanValueId,REPLACE(b.Field, 'Field         0', null) as Field, b.Instance, b.Value from[flight].[UnmatchedFlightPlan] a left outer join[flight].[UnmatchedFlightPlanValue] b on a.Id = b.FlightPlanId order by Id desc";
                    break;
                case "updateFlightNumber":
                    sqlQueryToRun = "update flight.UnmatchedFlightPlan set FlightNumber = 'DL99' where Id in (select top 1 Id from flight.UnmatchedFlightPlan order by Id desc)";
                    break;
                case "fetchAuditInsertUnmatchedFlightPlanDetails":
                    sqlQueryToRun = "select top 1 * from Flight.UnmatchedFlightPlanAudit where  ChangeType = 'INSERT' order by HistoryId desc";
                    break;
                case "fetchAuditUpdateUnmatchedFlightPlanDetails":
                    sqlQueryToRun = "select top 1 * from Flight.UnmatchedFlightPlanAudit where  ChangeType = 'UPDATE' order by HistoryId desc";
                    break;
                case "fetchAuditDeleteUnmatchedFlightPlanDetails":
                    sqlQueryToRun = "select top 1 * from Flight.UnmatchedFlightPlanAudit where  ChangeType = 'DELETE' order by HistoryId desc";
                    break;
                case "replaceSpaceinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",' ','%20') as ChangeTime from flight.UnmatchedFlightPlanAudit where HistoryId =" + fetchSiteIdValue + "";
                    break;
                case "replaceColoninTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",':','%3A') as ChangeTime from flight.UnmatchedFlightPlanAudit where HistoryId =" + fetchSiteIdValue + "";
                    break;
                case "replaceAddinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",'+','%2B') as ChangeTime from flight.UnmatchedFlightPlanAudit where HistoryId =" + fetchSiteIdValue + "";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
