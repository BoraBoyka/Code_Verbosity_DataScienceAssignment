using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_ArrivalFlight
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchSiteIdForIata = (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string siteId = (scenarioContext.ContainsKey("valueSiteId") ? scenarioContext.Get<string>("valueSiteId") : string.Empty);
            string fetchIdForArrival = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchArrivalFlightData":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber,ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId,b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata='MGL') order by Id";
                    break;
                case "fetchRecentAddedArrivalFlightDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime,a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistArrivalFlight":
                    sqlQueryToRun = "select max(Id+1) as arrivalFlightId from flight.Arrival";
                    break;
                case "fetchArrivalFlightDetailsWithDesSiteId":
                    sqlQueryToRun = "select top 1 * from flight.Arrival where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "updateOperDateArrFlightTable":
                    sqlQueryToRun = "update flight.Arrival set OperationDate='2023-01-28' where Id in (select top 1 Id from flight.Arrival order by Id)";
                    break;
                case "updateOperDateArrFlight":
                    sqlQueryToRun = "update flight.Arrival set OperationDate='2023-01-26' where Id in (select top 1 Id from flight.Arrival order by Id)";
                    break;
                case "fetchRecordDetailsForOpDateArrFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime,a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-01-28' order by Id";
                    break;
                case "deleteRecentAddedArrivalFlightData":
                    sqlQueryToRun = "delete from flight.Arrival where Id > (select top 1 Id from flight.Arrival where SiteId = 30 order by Id desc)";
                    break;
                case "fetchArrFlightDataForAllSiteClaims":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId,ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id";
                    break;
                case "fetchRecentAddedArrivalFlightDataWithSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign, ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata='MGL') order by Id";
                    break;
                case "fetchRecentAddedArrivalFlightDataWithAscSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign,ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id";
                    break;
                case "fetchRecentDeletedArrivalFlightData":
                    sqlQueryToRun = "select * from flight.Arrival where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecordDetailsForUnmatchedFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway,ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-01-26' and (a.CallSign IS NULL or a.CallSign ='') order by Id";
                    break;
                case "updateCallSignArrFlightTable":
                    sqlQueryToRun = "update flight.Arrival set CallSign='AirIndia' where Id in (select top 1 Id from flight.Arrival where SiteId in (select top 1 Id as Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id asc)";
                    break;
                case "fetchRecentAddedArrivalFlightData":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId order by Id desc";
                    break;
                case "fetchRecentAddedTopTwoArrivalFlightData":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin, a.OperationDate, a.AircraftRegistrationId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId, a.EstimatedOffBlockTime,b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where 2 > (select count(*) from flight.Arrival F2 where F2.Id > a.Id)";
                    break;
                case "deletedArrivalFlightDataForTop2Rows":
                    sqlQueryToRun = "delete from flight.Arrival where Id in (select top 2 Id from flight.Arrival order by Id desc)";
                    break;
                case "addDataArrivalFlightTable":
                    sqlQueryToRun = SQLQueries.AddArrFlightTestData;
                    sqlQueryToRun = sqlQueryToRun.Replace("{siteId}", fetchSiteIdForIata);
                    break;
                case "fetchSiteZRHForTestData":
                    sqlQueryToRun = "select Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "fetchSiteMGLForTestData":
                    sqlQueryToRun = "select Id as Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "fetchSiteTXLForTestData":
                    sqlQueryToRun = "select Id as Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "updateFlightPlanArrFlightTable":
                    sqlQueryToRun = "update flight.arrival set FlightPlanId = 1, OperationDate = '2022-08-22', Runway = 'null' where Id in (select top 1 Id from flight.Arrival)";
                    break;
                case "updateFlightPlanArrFlight":
                    sqlQueryToRun = "update flight.arrival set FlightPlanId = NULL, OperationDate = '2023-01-26', Runway = 'NULL' where Id in (select top 1 Id from flight.Arrival)";
                    break;
                case "fetchRecordDetailsForFlightPlanArrival":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.FlightPlanId, ISNULL(a.FlightRule,'Unknown') as FlightRule,a.Origin,a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedOffBlockTime, a.ChangedBy, a.ChangedTime, b.ArrivalId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH', 'MGL')) and a.OperationDate = '2022-08-22' and a.FlightPlanId = 1 order by Id";
                    break;
                case "fetchRecordDetailsForArrivalWithNoFlightPlan":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin,ISNULL(a.FlightRule, 'Unknown') as FlightRule, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedTaxiInTimeSeconds, a.EstimatedOffBlockTime, a.Destination, a.ChangedBy, a.ChangedTime,a.FlightPlanId,  b.ArrivalId, b.Field, b.Instance, b.Value from [flight].[Arrival] a left outer join [flight].[ArrivalValue] b on a.Id = b.ArrivalId where a.OperationDate = '2023-01-26' and a.FlightPlanId IS NULL and a.SiteId in (select Id from dbo.Site where Iata in ('ZRH', 'MGL')) order by Id";
                    break;
                case "fetchArrivalFlightDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 Id from flight.Arrival where SiteId in (select Id from dbo.Site where Iata = 'ZRH')";
                    break;
                case "updateArrivalFlightDetailsForFlightPlanId":
                    sqlQueryToRun = "update flight.Arrival set FlightPlanId = 'AT0894977', OperationDate = '2023-03-08' where Id =" + fetchSiteIdForIata + "";
                    break;
                case "updateArrivalFlightDetailsOperationDate":
                    sqlQueryToRun = "update flight.Arrival set FlightPlanId = NULL, OperationDate = '2023-01-26', AircraftRegistrationId = NULL, CallSign = NULL, EstimatedOffBlockTime = '2023-03-29 18:46:30 +00:00', Destination = NULL, FlightRule = NULL where Id =" + fetchSiteIdForIata + "";
                    break;
                case "fetchFlightPlanIdArrivalTable":
                    sqlQueryToRun = "select FlightPlanId from flight.Arrival where Id =" + fetchSiteIdForIata + "";
                    break;
                case "fetchAllArrivalFlightDetailsWithZRHSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.FlightPlanId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.Origin,a.Destination, a.OperationDate,a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedOffBlockTime, a.ChangedBy, a.ChangedTime, b.ArrivalId as FlightPlanValueId, b.Field, b.Instance, b.Value from[flight].[Arrival] a left outer join[flight].[ArrivalValue] b on a.Id = b.ArrivalId where SiteId in (select Id from dbo.Site where Iata = 'ZRH') order by Id desc";
                    break;
                case "updateFlightPlanIdOperationDateArrivalTable":
                    sqlQueryToRun = "update flight.arrival set FlightPlanId = 'AT089497177', OperationDate = '2022-09-06' where Id =" + fetchSiteIdForIata + "";
                    break;
                case "updateFlightPlanIdOperationDateArrivalTableWithUnmatchedData":
                    sqlQueryToRun = "update flight.arrival set FlightPlanId = 'AT08998877', OperationDate = '2022-05-06' where Id =" + fetchSiteIdForIata + "";
                    break;
                case "fetchAllArrivalFlightDetailsWithZRHSiteIdWithFieldValueNull":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.FlightPlanId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.Origin,a.Destination, a.OperationDate,a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedOffBlockTime, a.ChangedBy, a.ChangedTime, b.ArrivalId as FlightPlanValueId, REPLACE(b.Field,'Field         0',null) as Field, b.Instance, b.Value from[flight].[Arrival] a left outer join[flight].[ArrivalValue] b on a.Id = b.ArrivalId where SiteId in (select Id from dbo.Site where Iata = 'ZRH') order by Id desc";
                    break;
                case "updateCarrierFlightNumOpdateOriginArrivalTableWithUnmatchedData":
                    sqlQueryToRun = "update flight.arrival set Origin = 'JFK', Carrier = 'DL', FlightNumber = 'AI101', OperationDate = '2022-05-06' where Id =" + fetchIdForArrival + "";
                    break;
                case "updateArrivalFlightDetailsFieldValues":
                    sqlQueryToRun = "update flight.Arrival set FlightPlanId = NULL, FlightNumber = 'DL99', OperationDate = '2023-01-26', AircraftRegistrationId = NULL, CallSign = NULL, EstimatedOffBlockTime = '2023-03-29 18:46:30 +00:00', Destination = NULL, FlightRule = NULL where Id =" + fetchIdForArrival + "";
                    break;
                case "updateFlightPlanIdOpDateCallsignNullArrivalTableWithUnmatchedData":
                    sqlQueryToRun = "update flight.arrival set FlightPlanId = 'AT08998877', OperationDate = '2022-05-06', CallSign = NULL where Id =" + fetchSiteIdForIata + "";
                    break;
                case "fetchAuditInsertArrivalDetails":
                    sqlQueryToRun = "select top 1 * from Flight.ArrivalAudit where  ChangeType = 'INSERT' order by HistoryId desc";
                    break;
                case "fetchAuditUpdateArrivalDetails":
                    sqlQueryToRun = "select top 1 * from Flight.ArrivalAudit where  ChangeType = 'UPDATE' order by HistoryId desc";
                    break;
                case "fetchAuditDeleteArrivalDetails":
                    sqlQueryToRun = "select top 1 * from Flight.ArrivalAudit where  ChangeType = 'DELETE' order by HistoryId desc";
                    break;
                case "replaceSpaceinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",' ','%20') as ChangeTime from flight.ArrivalAudit where HistoryId =" + fetchIdForArrival + "";
                    break;
                case "replaceColoninTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",':','%3A') as ChangeTime from flight.ArrivalAudit where HistoryId =" + fetchIdForArrival + "";
                    break;
                case "replaceAddinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",'+','%2B') as ChangeTime from flight.ArrivalAudit where HistoryId =" + fetchIdForArrival + "";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
