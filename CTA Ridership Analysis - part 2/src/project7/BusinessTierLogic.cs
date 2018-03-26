//
// BusinessTier:  business logic, acting as interface between UI and data store.
//

using System;
using System.Collections.Generic;
using System.Data;




namespace BusinessTier
{

  //
  // Business:
  //
  public class Business
  {
    //
    // Fields:
    //
    private string _DBFile;
    private DataAccessTier.Data dataTier;

    // list to hold all of the stations in the database
    private List<CTAStation> stations;

    // list to hold the total ridership of the top stations
    private List<int> topValues;


    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Constructs a new instance of the business tier.  The format
    /// of the filename should be either |DataDirectory|\filename.mdf,
    /// or a complete Windows pathname.
    /// </summary>
    /// <param name="DatabaseFilename">Name of database file</param>
    /// 
    public Business(string DatabaseFilename)
    {
      _DBFile = DatabaseFilename;

      dataTier = new DataAccessTier.Data(DatabaseFilename);

      stations = new List<CTAStation>();
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    ///  Opens and closes a connection to the database, e.g. to
    ///  startup the server and make sure all is well.
    /// </summary>
    /// <returns>true if successful, false if not</returns>
    /// 
    public bool TestConnection()
    {
      return dataTier.OpenCloseConnection();
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns all the CTA Stations, ordered by name.
    /// </summary>
    /// <returns>Read-only list of CTAStation objects</returns>
    /// 
    public IReadOnlyList<CTAStation> GetStations()
    {
      try
      {
        // string to hold the sql query
        string sql = "SELECT * FROM Stations ORDER BY Name ASC;";

        // execute the query
        DataSet ds = dataTier.ExecuteNonScalarQuery(sql);

        // convert the table form into type CTAStation and add to list
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          // parse the data
          int id      = Convert.ToInt32(row["StationID"]);
          String name = Convert.ToString(row["Name"]);

          // add the data to the list
          stations.Add(new CTAStation(id, name));
        }
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.GetStations: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }

      // return the list of stations
      return stations;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns the CTA Stops associated with a given station,
    /// ordered by name.
    /// </summary>
    /// <returns>Read-only list of CTAStop objects</returns>
    ///
    public IReadOnlyList<CTAStop> GetStops(int stationID)
    {
      List<CTAStop> stops = new List<CTAStop>();

      try
      {
        // string to hold the sql query to get all stops associated
        // with the station that has stationID
        string sql2 = String.Format(@"
                          SELECT *
                          FROM Stops, Stations
                          WHERE 
                              Stations.StationID = Stops.StationID
                              AND
                              Stations.StationID = {0}
                              ORDER BY Stops.Name ASC;", stationID);

        // execute the sql query
        DataSet ds = dataTier.ExecuteNonScalarQuery(sql2);

        // fill in the read-only list of stops 
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          // parse all the data
          int    stopid     = Convert.ToInt32(row["StopID"]);
          int    stationid  = Convert.ToInt32(row["StationID"]);
          string name       = Convert.ToString(row["Name"]);
          string direction  = Convert.ToString(row["Direction"]);
          bool   ada        = Convert.ToBoolean(row["ADA"]);
          double lat        = Convert.ToDouble(row["Latitude"]);
          double lon        = Convert.ToDouble(row["Longitude"]);

          // add the stop to the list
          stops.Add(new CTAStop(stopid, name, stationid, direction, ada, lat, lon));
        }
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.GetStops: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }

      return stops;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns the CTA line colors associated with a given stop,
    /// ordered by name.
    ///
    public IReadOnlyList<string> getLines(string name)
    {
      List<string> lines = new List<string>();

      try
      {
        // string to hold the sql query
        string sql = String.Format(@"
                      SELECT Lines.Color
                      FROM Lines, Stops, StopDetails
                      WHERE
                          Lines.LineID = StopDetails.LineID
                          AND
                          StopDetails.StopID = Stops.StopID
                          AND 
                          Stops.Name = '{0}'
                      ORDER BY Lines.Color ASC;", name);

        // execute the query
        DataSet ds = dataTier.ExecuteNonScalarQuery(sql);

        // parse and add the lines to the list
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          lines.Add(Convert.ToString(row["Color"]));
        }
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.getLines: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }

      return lines;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns the top N CTA Stations total ridership, 
    /// ordered by name.
    /// </summary>
    /// <returns>list of integers</returns>
    /// 
    public List<int> getTop10values()
    {
      return topValues;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns read only list of stations that have similar
    /// names as stationName ordered by name.
    /// </summary>
    /// <returns>list of CTAStations</returns>
    /// 
    public IReadOnlyList<CTAStation> getSimilarStations(string stationName)
    {

      // create a list of stations to hold the data
      List<CTAStation> similar = new List<CTAStation>();

      try
      {
        // string to hold the sql query
        string sql = String.Format(@"
                          SELECT Name, StationID 
                          FROM Stations
                          WHERE Name like '%{0}%'
                          order by Name ASC", stationName);

        // execute the query
        DataSet ds = dataTier.ExecuteNonScalarQuery(sql);


        // fill the lists with the data
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          // parse the data
          string name = Convert.ToString(row["Name"]);
          int id = Convert.ToInt32(row["StationID"]);

          // add the data to the list
          similar.Add(new CTAStation(id, name));
        }
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.getSimilarStations: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }

      // return the list
      return similar;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns the top N CTA Stations by ridership, 
    /// ordered by name.
    /// </summary>
    /// <returns>Read-only list of CTAStation objects</returns>
    /// 
    public IReadOnlyList<CTAStation> GetTopStations(int N)
    {
      if (N < 1)
        throw new ArgumentException("GetTopStations: N must be positive");

      List<CTAStation> topStations = new List<CTAStation>();

      topValues = new List<int>();

      // use lambda expression to find the desired station
      // var theStation = stationsList.Find(x => x.Name == stationName);

      try
      {
        // string that will hold the sql query
        string sql = String.Format(@"
                       SELECT TOP {0} Stations.Name, Stations.StationID, SUM(DailyTotal) as Total FROM Stations, Riderships
                        WHERE Stations.StationID = Riderships.StationID
                        GROUP BY Name, Stations.StationID
                        ORDER BY Total DESC", N);

        // execute the query
        DataSet ds = dataTier.ExecuteNonScalarQuery(sql);

        // fill the lists with the data
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          // parse the data
          string name = Convert.ToString(row["Name"]);
          int id      = Convert.ToInt32(row["StationID"]);
          int total   = Convert.ToInt32(row["Total"]);

          // add the total to a different list to retrieve it
          // later since this function must return a list of stations
          topValues.Add(total);
          topStations.Add(new CTAStation(id, name));
        }
        
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.GetTopStations: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }

      return topStations;
    }
    //---------------------------------------------------------------------
    ///
    /// <summary>
    /// Returns a RidershipStats object that contains
    /// all of the necessary riderhsip data needed 
    /// 
    public RidershipStats getRidership(string name)
    {
      try
      {
        // nonscalar
        // string that holds the sql query that retrieves the total ridership for the station with name,
        // and also the amount of days rides occured to obtain the average ridership
        string sql1 = String.Format(@"SELECT SUM(convert(bigint, Riderships.DailyTotal)) as xTotal, COUNT(TheDate) as Avg 
                                      FROM Riderships, Stations
                                      WHERE Riderships.StationID = Stations.StationID
                                      AND
                                      Stations.Name = '{0}';", name);
        // scalar
        // string that holds the sql query that retrieves the total overall ridership
        string sql2 = String.Format(@"SELECT SUM(convert(bigint, Riderships.DailyTotal)) as allRides 
                                      FROM Riderships, Stations
                                      WHERE Riderships.StationID = Stations.StationID");
        // nonscalar
        // string that holds the sql query that retrieves the weekly ridership data
        string sql3 = String.Format(@"SELECT Riderships.TypeOfDay, SUM(Riderships.DailyTotal) AS Total
                                      FROM Stations
                                      INNER JOIN Riderships
                                      ON Stations.StationID = Riderships.StationID 
                                      WHERE Name = '{0}'
                                      GROUP BY Riderships.TypeOfDay
                                      ORDER BY Riderships.TypeOfDay;", name);

        
        // execute the queries
        DataSet ds1 = dataTier.ExecuteNonScalarQuery(sql1);
        object  ds2 = dataTier.ExecuteScalarQuery(sql2);
        DataSet ds3 = dataTier.ExecuteNonScalarQuery(sql3);

        
        // parse all the data

        int xTotal = 0, avgHelp = 0, average = 0, wkdy = 0, sat = 0, sunHol = 0;
        double percent = 0.00;

        // get the total for the station and the number of days ridership
        foreach (DataRow row in ds1.Tables["TABLE"].Rows)
        {
          xTotal = Convert.ToInt32(row["xTotal"]);
          avgHelp = Convert.ToInt32(row["Avg"]);
        }


        // get the week stats for the station
        char tmp;
        foreach (DataRow row in ds3.Tables["TABLE"].Rows)
        {
          tmp = Convert.ToChar(row["TypeOfDay"]);
          
          if (tmp == 'A')
            sat = Convert.ToInt32(row["Total"]);
          else if (tmp == 'W')
            wkdy = Convert.ToInt32(row["Total"]);
          else
            sunHol = Convert.ToInt32(row["Total"]);
         
        }

        // calculate the average ridership for station x
        average = xTotal / avgHelp;

        // calculate the percent ridership
        // if the total ridership overall is actually an understandable value, use it
        if (ds2 != null && ds2 != DBNull.Value)
        {
          percent = (xTotal / Convert.ToDouble(ds2)) * 100;
        }
        
        // done! return the data
        return (new RidershipStats(name, xTotal, average, percent, wkdy, sat, sunHol));
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in Business.getRidership: '{0}'", ex.Message);
        throw new ApplicationException(msg);
      }
    }
    //---------------------------------------------------------------------
    //
    // updateADA:
    //
    // updates the stop's ada in the database that is passed to it
    //
    public int updateADA(string stopName, string curADA)
    {
	 // determine what we want to update the ada to
	 // by figuring out what the ada currently is
	 int val = -1;

	 if (curADA == "Yes")
	   val = 0;
	 else
	   val = 1;

	 try
	 {
	   // string to hold the sql query
	   string sql = String.Format(@"UPDATE Stops
							   SET ADA = {0}
							   WHERE Name = '{1}'", val, stopName);

	   // execute the query and return the number of rows modified
	   return (dataTier.ExecuteActionQuery(sql));
	 }
	 catch (Exception ex)
	 {
	   string msg = string.Format("Error in Business.updateADA: '{0}'", ex.Message);
	   throw new ApplicationException(msg);
	 }
    }
    //---------------------------------------------------------------------

  }//class
}//namespace
