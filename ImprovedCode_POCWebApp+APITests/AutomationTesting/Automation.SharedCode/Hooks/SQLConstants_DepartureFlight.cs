using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_DepartureFlight
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchSiteIdForIata = (scenarioContext.ContainsKey("valueSQLData") ? scenarioContext.Get<string>("valueSQLData") : string.Empty);
            string fetchIdForDepartureFlight = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);
            string siteId = (scenarioContext.ContainsKey("valueSiteId") ? scenarioContext.Get<string>("valueSiteId") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchDepartureFlightData":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata='MGL') order by Id";
                    break;
                case "fetchRecentAddedDepartureFlightDataForSiteIdAsc":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id desc";
                    break;
                case "fetchIdDoesntExistDepartureFlight":
                    sqlQueryToRun = "select max(Id+1) as departureFlightId from flight.Departure";
                    break;
                case "fetchDepartureFlightDetailsWithDesSiteId":
                    sqlQueryToRun = "select top 1 * from flight.Departure where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "updateTobtTimeDepartureFlightTable":
                    sqlQueryToRun = "update flight.Departure set TargetOffBlockTime='2023-01-29 16:45:29 +00:00' where Id in (select top 1 Id from flight.Departure)";
                    break;
                case "fetchRecordDetailsForTobtDepFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.TargetOffBlockTime BETWEEN '2023-01-28' AND '2023-01-30'order by Id";
                    break;
                case "updateOperDateDepFlightTable":
                    sqlQueryToRun = "update flight.Departure set OperationDate='2023-01-28' where Id in (select top 1 Id from flight.Departure order by Id)";
                    break;
                case "updateOperDateDepFlight":
                    sqlQueryToRun = "update flight.Departure set OperationDate='2023-01-26' where Id in (select top 1 Id from flight.Departure order by Id)";
                    break;
                case "fetchRecordDetailsForOpDateDepFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-01-28' order by Id";
                    break;
                case "fetchDepFlightDataForAllSiteClaims":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id";
                    break;
                case "fetchRecentAddedDepartureFlightDataWithSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata='MGL') order by Id";
                    break;
                case "fetchRecentAddedDepartureFlightDataWithAscSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata='ZRH') order by Id";
                    break;
                case "fetchRecentDeletedDepartureFlightData":
                    sqlQueryToRun = "select * from flight.Departure where Id=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecentAddedDepartureFlightData":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, a.ChangedBy, a.ChangedTime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId order by Id desc";
                    break;
                case "deleteRecentAddedDepartureFlightData":
                    sqlQueryToRun = "delete from flight.departure where Id > (select top 1 Id from flight.Departure where SiteId = 30 order by Id desc)";
                    break;
                case "fetchRecentAddedTopTwoDepartureFlightData":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime,b.DepartureId, b.Field, b.Instance, b.Value from[flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where 2 > (select count(*) from flight.Departure F2 where F2.Id > a.Id)";
                    break;
                case "deletedDepartureFlightDataForTop2Rows":
                    sqlQueryToRun = "delete from flight.Departure where Id in (select top 2 Id from flight.Departure order by Id desc)";
                    break;
                case "fetchRecordDetailsForUnmatchedDepFlight":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL')) and a.OperationDate='2023-01-26' and (a.CallSign IS NULL or a.CallSign ='') order by Id";
                    break;
                case "updateCallSignDepFlightTable":
                    sqlQueryToRun = "update flight.Departure set CallSign='AirIndia' where Id in (select top 1 Id from flight.Departure where SiteId in (select top 1 Id as Id from dbo.Site where Iata in ('ZRH','MGL')) order by Id asc)";
                    break;
                case "addDataDepartureFlightTable":
                    sqlQueryToRun = SQLQueries.AddDepFlightTestData;
                    sqlQueryToRun = sqlQueryToRun.Replace("{siteId}", fetchSiteIdForIata);
                    break;
                case "fetchSiteZRHForTestData":
                    sqlQueryToRun = "select Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "fetchSiteMGLForTestData":
                    sqlQueryToRun = "select Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "fetchSiteTXLForTestData":
                    sqlQueryToRun = "select Id from dbo.Site where Iata='" + siteId + "'";
                    break;
                case "fetchRecordDetailsForDepartureWithNoFlightPlan":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime, a.TargetOffBlockTime, a.ActualOffBlocktime, b.DepartureId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.OperationDate = '2023-01-26' and a.FlightPlanId IS NULL and a.SiteId in (select Id from dbo.Site where Iata in ('ZRH', 'MGL')) order by Id";
                    break;
                case "fetchDepartureFlightDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 Id from flight.Departure where SiteId in (select Id from dbo.Site where Iata = 'ZRH')";
                    break;
                case "updateDepartureFlightDetailsForFlightPlanId":
                    sqlQueryToRun = "update flight.Departure set FlightPlanId = 'AT0894977', OperationDate = '2023-03-08' where Id =" + fetchIdForDepartureFlight + ""; 
                    break;
                case "updateDepartureFlightDetailsOperationDate":
                    sqlQueryToRun = "update flight.Departure set FlightPlanId = 'NULL', OperationDate = '2023-01-26' where Id =" + fetchIdForDepartureFlight + "";
                    break;
                case "fetchFlightPlanIdDepartureTable":
                    sqlQueryToRun = "select FlightPlanId from flight.Departure where Id =" + fetchIdForDepartureFlight + "";
                    break;
                case "updateFlightPlanDepFlightTable":
                    sqlQueryToRun = "update flight.Departure set FlightPlanId = 2, OperationDate = '2022-08-25', Runway = 'null' where Id in (select top 1 Id from flight.Departure)";
                    break;
                case "updateFlightPlanDepFlight":
                    sqlQueryToRun = "update flight.Departure set FlightPlanId = NULL, OperationDate = '2023-01-26', Runway = 'NULL' where Id in (select top 1 Id from flight.Departure)";
                    break;
                case "fetchRecordDetailsForFlightPlanDeparture":
                    sqlQueryToRun = "select a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.FlightPlanId, ISNULL(a.FlightRule,'Unknown') as FlightRule,a.Origin,a.Destination, a.OperationDate, a.AircraftRegistrationId, a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.EstimatedOffBlockTime,b.DepartureId as FlightPlanValueId, b.Field, b.Instance, b.Value from [flight].[Departure] a left outer join [flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata in ('ZRH', 'MGL')) and a.OperationDate = '2022-08-25' and a.FlightPlanId = 2 order by Id";
                    break;
                case "updateFlightPlanIdOperationDateDepartureTable":
                    sqlQueryToRun = "update flight.departure set FlightPlanId = 'AT089497177', OperationDate = '2022-09-06' where Id =" + fetchSiteIdForIata + "";
                    break;
                case "updateDepFlightDetailsOperationDate":
                    sqlQueryToRun = "update flight.Departure set OperationDate = '2023-01-26',FlightRule = 'NULL',CallSign = 'NULL',Destination = 'NULL', Origin = 'NULL' where Id = " + fetchSiteIdForIata + "";
                    break;
                case "fetchAllDepartureFlightDetailsWithZRHSiteId":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin,a.Destination, a.OperationDate, a.FlightPlanId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.AircraftRegistrationId,a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, a.ChangedBy, a.ChangedTime,b.DepartureId as FlightPlanValueId, b.Field, b.Instance, b.Value from[flight].[Departure] a left outer join[flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata = 'ZRH') order by Id desc";
                    break;
                case "updateFlightPlanIdOperationDateDepartureTableWithUnmatchedData":
                    sqlQueryToRun = "update flight.departure set FlightPlanId = 'AT08998877', OperationDate = '2022-05-06' where Id =" + fetchSiteIdForIata + "";
                    break;
                case "fetchAllDepartureFlightDetailsWithZRHSiteIdWithFieldValueNull":
                    sqlQueryToRun = "select top 1 a.Id, a.SiteId, a.Carrier, a.FlightNumber, a.Origin,a.Destination, a.OperationDate, a.FlightPlanId, ISNULL(a.FlightRule, 'Unknown') as FlightRule,a.CallSign, a.ArcId, a.Runway, a.AircraftType,a.Stand, a.DeicingRequest, a.ActualTakeOffTime, a.EstimatedTakeOffTime, a.TargetTakeOffTime, a.AircraftRegistrationId,a.TargetTakeOffTimeRequested, a.EstimatedOffBlockTime,  a.TargetOffBlockTime, a.ActualOffBlocktime, a.ChangedBy, a.ChangedTime,b.DepartureId as FlightPlanValueId, REPLACE(b.Field,'Field         0',null) as Field, b.Instance, b.Value from[flight].[Departure] a left outer join[flight].[DepartureValue] b on a.Id = b.DepartureId where a.SiteId in (select Id from dbo.Site where Iata = 'ZRH') order by Id desc";
                    break;
                case "updateDepartureFlights":
                    sqlQueryToRun= "update flight.Departure set OperationDate = '2023-01-26',AircraftRegistrationId = 'NULL', CallSign = 'NULL', Destination = 'NULL', FlightRule = 'NULL', Origin = 'NULL' where Id = " + fetchSiteIdForIata + "";
                    break;
                case "updateDepartureFlightDetails":
                    sqlQueryToRun = "update flight.Departure set OperationDate = '2023-01-26', AircraftRegistrationId = 'NULL', CallSign = 'NULL', Origin = NULL, Destination = 'NULL', FlightRule = NULL where Id =" + fetchSiteIdForIata + "";
                    break;
                case "updateCarrierFlightNumOpdateDestinationDepartureTableWithUnmatchedData":
                    sqlQueryToRun = "update flight.Departure set Destination = 'CVG', Carrier = 'DL', FlightNumber = 'AI101', OperationDate = '2022-05-06' where Id =" + fetchIdForDepartureFlight + "";
                    break;
                case "updateDepartureFlightDetailsFieldValues":
                    sqlQueryToRun = "update flight.Departure set FlightPlanId = NULL, FlightNumber = 'DL99', OperationDate = '2023-01-26', AircraftRegistrationId = NULL, CallSign = NULL, EstimatedOffBlockTime = '2023-03-29 18:46:30 +00:00', FlightRule = NULL, Origin = NULL where Id =" + fetchIdForDepartureFlight + "";
                    break;
                case "fetchAuditInsertDepartureDetails":
                    sqlQueryToRun = "select top 1 * from Flight.DepartureAudit where  ChangeType = 'INSERT' order by HistoryId desc";
                    break;
                case "fetchAuditUpdateDepartureDetails":
                    sqlQueryToRun = "select top 1 * from Flight.DepartureAudit where  ChangeType = 'UPDATE' order by HistoryId desc";
                    break;
                case "fetchAuditDeleteDepartureDetails":
                    sqlQueryToRun = "select top 1 * from Flight.DepartureAudit where  ChangeType = 'DELETE' order by HistoryId desc";
                    break;
                case "replaceSpaceinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" +"'" + fetchSiteIdForIata + "'"+",' ','%20') as ChangeTime from flight.DepartureAudit where HistoryId =" + fetchIdForDepartureFlight + "";
                    break;
                case "replaceColoninTimestampForAudit":
                    sqlQueryToRun = "select REPLACE("+"'" + fetchSiteIdForIata + "'"+",':','%3A') as ChangeTime from flight.DepartureAudit where HistoryId =" + fetchIdForDepartureFlight + "";
                    break;
                case "replaceAddinTimestampForAudit":
                    sqlQueryToRun = "select REPLACE(" + "'" + fetchSiteIdForIata + "'" + ",'+','%2B') as ChangeTime from flight.DepartureAudit where HistoryId =" + fetchIdForDepartureFlight + "";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
