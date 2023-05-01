using Acdm.InformationServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ACDMAutomation.Shared.Hooks
{
    public class SQLConstants_Flight
    {
        public static string SQLQuery(string sqlQueryName, ScenarioContext scenarioContext)
        {
            string sqlQueryToRun = "";
            string fetchDeletedRecordIdValueAPI = (scenarioContext.ContainsKey("idValueDB") ? scenarioContext.Get<string>("idValueDB") : string.Empty);
            string fetchedNameValueAPI = (scenarioContext.ContainsKey("typeNameValueDB") ? scenarioContext.Get<string>("typeNameValueDB") : string.Empty);
            string fetchSiteIdValue = (scenarioContext.ContainsKey("valueSQLResponse") ? scenarioContext.Get<string>("valueSQLResponse") : string.Empty);            
            string aircraftTypeId = (scenarioContext.ContainsKey("outputSQLResponse") ? scenarioContext.Get<string>("outputSQLResponse") : string.Empty);
            string fetchRecentAddedIdValue = (scenarioContext.ContainsKey("addedIdValue") ? scenarioContext.Get<string>("addedIdValue") : string.Empty);

            switch (sqlQueryName)
            {
                case "fetchRecentAddedFlightData":
                    sqlQueryToRun = "select top 1 * from flight.Flight order by FlightId desc";
                    break;
                case "fetchRecentAddedFlightDataWithDynamicId":
                    sqlQueryToRun = "select top 1 * from flight.Flight where FlightId=" + fetchRecentAddedIdValue + "";
                    break;
                case "fetchRecentAddedFlightDataForSiteidAsc":
                    sqlQueryToRun = "select top 1 * from flight.Flight where SiteId in (select Id from dbo.Site where Iata = 'ZRH')";
                    break;
                case "fetchRecentAddedTopTwoFlightData":
                    sqlQueryToRun = "select * from flight.Flight F1 where 2 > (select count(*) from flight.Flight F2 where F2.FlightId > F1.FlightId)";
                    break;
                case "fetchFlightData":
                    sqlQueryToRun = "select * from flight.Flight where SiteId = (select Id from dbo.Site where Iata = 'MGL')";
                    break;
                case "fetchIdDoesntExistFlight":
                    sqlQueryToRun = "select max(FlightId+1) as FlightId from flight.Flight";
                    break;
                case "fetchRecentDeletedFlightData":
                    sqlQueryToRun = "select * from flight.Flight where FlightId=" + fetchDeletedRecordIdValueAPI + "";
                    break;
                case "fetchRecordDetailsForFlightNumber":
                    sqlQueryToRun = "select * from flight.Flight where FlightNumber='" + fetchedNameValueAPI + "' and SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchRecordDetailsForCallSign":
                    sqlQueryToRun = "select * from flight.Flight where CallSign='" + fetchedNameValueAPI + "' and SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "fetchRecordDetailsForTargetOffBlockTime":
                    sqlQueryToRun = "select * from flight.Flight where TargetOffBlockTime BETWEEN '2022-05-01' AND '2022-05-08' and SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "UpdateParkPositionFlightTable":
                    sqlQueryToRun = "update flight.Flight set ParkPosition=0 where FlightId>=1";
                    break;
                case "UpdateCommentFlightTable":
                    sqlQueryToRun = "update flight.Flight set Comment='Test Comment' where FlightId>=1";
                    break;
                case "UpdateTaxiTimetoPadFlightTable":
                    sqlQueryToRun = "update flight.Flight set EstimatedTaxiTimeToPadInSeconds=0 where FlightId>=1";
                    break;
                case "insertNewRecordFlightTable":
                    sqlQueryToRun = "insert into flight.Flight([SiteId],[FlightNumber],[CallSign],[FlightRule],[DeicingRequest],[Runway],[Stand],[AirlinePriority],[EstimatedOffBlockTime],[ScheduledOffBlockTime],[TargetOffBlockTime],[TargetTakeOffTime],[TargetStartUpApprovalTime],[EstimatedTaxiOutTimeInSeconds],[EstimatedRunwayWaitTimeInSeconds],[EstimatedDeicingTimeInSeconds],[StartupDurationInSeconds],[PushbackDurationInSeconds],[CoordinatedDepartureWindowBegin],[CoordinatedDepartureWindowEnd],[CoordinatedDepartureTime],[AtcSlotBegin],[AtcSlotEnd],[TargetTakeOffTimeRequested],[IsManualFixed],[ParkPosition],[Comment],[AircraftType],[EstimatedTaxiTimeToPadInSeconds]) values('" + Int32.Parse(fetchSiteIdValue) + "', 'AI101', 'AirIndia', 'Vfr', 'Pad', '06R', 'A22', 0, '2022-05-06 18:15:06 +00:00','2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00',2, 4, 5, 7, 8, '2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00','2022-05-06 18:15:06 +00:00', '2022-05-06 18:15:06 +00:00', 1, 0, 'Test comment',  '" + Int32.Parse(aircraftTypeId) + "',0) ";
                    break;
                case "fetchFlightDetailsWithSiteId":
                    sqlQueryToRun = "select top 1 * from flight.Flight where SiteId in (select Id from dbo.Site where Iata='MGL')";
                    break;
                case "fetchFlightDetailsWithDesSiteId":
                    sqlQueryToRun = "select top 1 * from flight.Flight where SiteId in (select Id from dbo.Site where Iata='TXL')";
                    break;
                case "deletedFlightDataForTop2Rows":
                    sqlQueryToRun = "delete from flight.Flight where FlightId in (select top 2 FlightId from flight.Flight order by FlightId desc)";
                    break;
                case "fetchFlightDataForAllSiteClaims":
                    sqlQueryToRun = "select * from flight.Flight where SiteId in (select Id from dbo.Site where Iata in ('ZRH','MGL'))";
                    break;
                case "UpdateSiteIdFlightTable":
                    sqlQueryToRun = "update flight.Flight set SiteId = (select top 1 Id as Id from dbo.Site where Iata='TDDT') where FlightId in (select top 1 flightId from flight.Flight)";
                    break;
                default:
                    break;
            }
            return sqlQueryToRun;
        }
    }
}
