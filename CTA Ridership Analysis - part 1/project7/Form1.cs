//
// CTA Ridership analysis.
//
// Ahmed Khan, 652469935, akhan227
// U. of Illinois, Chicago
// CS341, Fall2017
// Project #07
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace project7
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    //---------------------------------------------------------------------
    //
    // Form1_Load:
    //
    // nice trick to make a more responsive application is to make
    // sure SQL Server is up and running in the background
    //
    private void Form1_Load(object sender, EventArgs e)
    {
      // open-close connect to get SQL Server started
      SqlConnection db = null;

      try
      {
        string connectionInfo = "...";
        db = new SqlConnection(connectionInfo);
        db.Open();
      }
      catch
      {
        // ignore any exception that occurs, goal is just to startup
      }
      finally
      {
        // close connection
        if (db != null && db.State == ConnectionState.Open)
          db.Close();
      }
    }
    //---------------------------------------------------------------------
    //
    // executeScalarSQL:
    //
    // opens the data base
    // executes the query passed to it
    // closes the data base
    // returns the result
    //
    private object executeScalarSQL(string sql)
    {
      // begin the code for the sql query
      string version, filename, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CTA.mdf";

      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      cmd.CommandText = sql;

      // execute the query
      object result = cmd.ExecuteScalar();

      db.Close();

      return result;
    }
    //---------------------------------------------------------------------
    //
    // executeAdapterSQL:
    //
    // opens the data base
    // executes the query
    // closes the data base
    // returns the result
    //
    private DataSet executeAdapaterSQL(string sql)
    {
      // begin the code for the sql query
      string version, filename, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CTA.mdf";

      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);

      db.Close();
      // end the code that executes the sql query
      return ds;
    }
    //---------------------------------------------------------------------
    //
    // totalRidership:
    //
    // total number of rides for the specified station
    // part 2a
    //
    private int totalRidership(string name)
    {
      // query for total ridership
      // the total ridership may overflow SQL’s default integer datatype,
      // convert to 'bigint' 
      string sql = string.Format(@"
                          SELECT SUM(convert(bigint, Riderships.DailyTotal)) FROM Riderships, Stations
                          WHERE Riderships.StationID = Stations.StationID
                          AND
                          Stations.Name = '{0}';", name);

      // execute the query for total ridership
      object result = executeScalarSQL(sql);

      // total number of rides for this station
      int totalRides = 0;

      // if the result is actually an understandable value, use it
      if (result != null && result != DBNull.Value)
      {
        totalRides = Convert.ToInt32(result);
      }

      return totalRides;
    }
    //---------------------------------------------------------------------
    //
    // averageRidership:
    //
    // average number of rides for the specific station
    // 2b
    //
    private int averageRidership(string name, int totalRides)
    {
      // query for total number of days the specific train had rides
      string sql = string.Format(@"
                          SELECT COUNT(TheDate) FROM Riderships, Stations
                          WHERE Riderships.StationID = Stations.StationID
                          AND
                          Stations.Name = '{0}';", name);

      // execute the query for total ridership
      object result2 = executeScalarSQL(sql);

      // average number of rides for the specific station
      int avgRides = 0;

      // if the result is actually an understandable value, use it
      if (result2 != null && result2 != DBNull.Value)
      {
        avgRides = totalRides / Convert.ToInt32(result2);
      }

      return avgRides;
    }
    //---------------------------------------------------------------------
    //
    // percentRidership:
    //
    // overall percentage of rides at the specific station
    // 2c
    //
    private double percentRidership(int totalRides)
    {
      // query for total number of rides total
      string sql = string.Format(@"
                    SELECT SUM(convert(bigint, Riderships.DailyTotal)) FROM Riderships, Stations
                    WHERE Riderships.StationID = Stations.StationID;");

      // execute the query for total ridership
      object result3 = executeScalarSQL(sql);

      // overall percentage of rides at this station
      double percent = 0.00;

      // if the result is actually an understandable value, use it
      if (result3 != null && result3 != DBNull.Value)
      {
        percent = (totalRides / Convert.ToDouble(result3)) * 100;
      }

      return percent;
    }
    //---------------------------------------------------------------------
    //
    // top10ToolStripMenuItem_Click:
    //
    // user clicked the 'Top 10' option
    //
    private void top10ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      
      // string that will hold the sql query
      string sql = String.Format(@"
                        SELECT TOP 10 Stations.Name, Total = Sum(DailyTotal) FROM Stations, Riderships
                        WHERE Stations.StationID = Riderships.StationID
                        GROUP BY Name
                        ORDER BY Total DESC");

        // execute the query
        DataSet ds = executeAdapaterSQL(sql);

        // create a string builder to create the string with all 10 stations
        StringBuilder sb = new StringBuilder();

        sb.Append("\t\tTop-10 stations in terms of ridership\n\n");
        int num = 1;
        // fill the list box with the appropriate data
        foreach (DataRow row in ds.Tables["TABLE"].Rows)
        {
          string name  = Convert.ToString(row["Name"]);

          // add commas 
          string total = string.Format("{0:n0}", row["Total"]);

         sb.Append(num + ".  " + name + ": " + total + "\n\n");

          num++;
        }

        // show the string that was built to the user
        MessageBox.Show(sb.ToString());
      }
    //---------------------------------------------------------------------
    //
    // loadToolStripMenuItem_Click:
    //
    // the user clicked the 'Load' option
    //
    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // this is the string that has the sql query in it
      string sql = string.Format(@"
                                  SELECT * FROM Stations
                                   ORDER BY Name ASC;");

      // call the function that executes the sql query
      DataSet ds = executeAdapaterSQL(sql);

      int numStations = 0;

      // fill the list box with the appropriate data
      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        string station = Convert.ToString(row["Name"]);
        this.loadStationsListBox.Items.Add(station);

        numStations++;
      }

      // pre-select the first element in the list box
      if (this.loadStationsListBox.Items.Count > 0)
        this.loadStationsListBox.SelectedIndex = 0;

      // change the label that tells the user how many stations are in the database
      string msg = string.Format("Number of stations: {0}", numStations);

      this.numStationsLabel.Text = msg;
    }
    //---------------------------------------------------------------------
    // 
    // loadStationsListBox_SelectedIndexChanged:
    //
    // The user clicked an item in the station names list box
    //
    private void loadStationsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      // replace the single quotes in the name with double quotes
      // so that sql query can be executed
      string name = this.loadStationsListBox.Text;
      name = name.Replace("'", "''");

      // 2a
      // total number of rides for this station
      int totalRides = totalRidership(name);
      // change the label, and insert commas at appropriate indeces
      this.totalValueLbl.Text = string.Format("{0:n0}", totalRides);

      // 2b
      // average number of rides for the specific station
      int avgRides = averageRidership(name, totalRides);
      // change the label, and insert commas at appropriate indeces
      this.avgValueLbl.Text = string.Format("{0:n0}/day", avgRides);

      // 2c
      // overall percentage of rides at this station
      double percent = percentRidership(totalRides);
      // change the label, and also only show decimals up to the hundredth position
      this.percentValueLbl.Text = String.Format("{0:0.00}%", percent);

      // 2d - 2f
      // ‘W’ denotes a weekday, 
      // ‘A’ denotes Saturday, 
      // and ‘U’ denotes Sunday or Holiday.

      // string to  hold the sql query
      string sql = String.Format(@"
                    SELECT Riderships.TypeOfDay, SUM(Riderships.DailyTotal) AS Total
                    FROM Stations
                    INNER JOIN Riderships
                    ON Stations.StationID = Riderships.StationID 
                    WHERE Name = '{0}'
                    GROUP BY Riderships.TypeOfDay
                    ORDER BY Riderships.TypeOfDay;", name);

      // execute the query
      DataSet ds4 = executeAdapaterSQL(sql);

      int wkdy = 0, sat = 0, sun = 0;
      char tmp = 'x';

      // get the data from the execution
      foreach (DataRow row in ds4.Tables["TABLE"].Rows)
      {
        tmp = Convert.ToChar(row["TypeOfDay"]);

        if (tmp == 'A')
          sat = Convert.ToInt32(row["Total"]);
        else if (tmp == 'W')
          wkdy = Convert.ToInt32(row["Total"]);
        else
          sun = Convert.ToInt32(row["Total"]);
      }

      // change the labels, and insert commas at appropriate indeces
      this.wkdyValueLbl.Text   = String.Format("{0:n0}", wkdy);
      this.satValueLbl.Text    = String.Format("{0:n0}", sat);
      this.sunHolValueLbl.Text = String.Format("{0:n0}", sun);

      // 2g
      // clear the stops list box each time a new station is clicked so data is accurate
      this.stopsListBox.Items.Clear();
      this.stopsListBox.Refresh();

      // the sql string query to get the stops at the specific station
      string sql2 = String.Format(@"
                          SELECT Stops.Name 
                          FROM Stations, Stops
                          WHERE 
                              Stations.StationID = Stops.StationID
                              AND
                              Stations.Name = '{0}'
                              ORDER BY Stops.Name ASC;", name);

      // execute the sql query
      DataSet ds = executeAdapaterSQL(sql2);

      int numStops = 0;

      // fill the list box with the appropriate data
      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        string stop = Convert.ToString(row["Name"]);
        this.stopsListBox.Items.Add(stop);

        numStops++;
      }

      // pre-select the first index of the stops, or clear the data
      if (this.stopsListBox.Items.Count > 0)
        this.stopsListBox.SelectedIndex = 0;
      else
      {
        this.directionValueLbl.Text = "N/A";
        this.handicapValueLbl.Text  = "N/A";
        this.locationValueLbl.Text  = "N/A";
        this.linesLbl.Text          = "Stops: 0";
        this.linesListBox.Items.Clear();
        this.linesListBox.Refresh();
      }

      // show the user the number of stops at the station they chose
      this.stopsLabel.Text = String.Format("Stops at this station: {0}", numStops);
    }
    //---------------------------------------------------------------------
    //
    // stopsListBox_SelectedIndexChanged:
    //
    // the user clicked a stop from the stop list box to see it's info
    //
    private void stopsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      // replace the single quotes in the name with double quotes
      // so that sql query can be executed
      string name = this.stopsListBox.Text;
      name = name.Replace("'", "''");

      // string to hold the sql query
      string sql6 = String.Format(@"
                    SELECT * 
                    FROM Stops
                    WHERE 
                        Stops.Name = '{0}';", name);

      // execute the query
      DataSet ds = executeAdapaterSQL(sql6);

      // Stop’s direction(N, E, S, W)
      // -ADA —a boolean denoting whether stop is handicap accessible
      string direction = null;
      int handicap   = -1;
      double latitude = -999.00, longitude = -999.00;

      // get the required data
      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        direction = Convert.ToString(row["Direction"]);
        handicap  = Convert.ToInt32(row["ADA"]);
        latitude  = Convert.ToDouble(row["Latitude"]);
        longitude = Convert.ToDouble(row["Longitude"]);
      }

      // change the label that tells the direction of the line
      this.directionValueLbl.Text = direction;

      // change the label that tells whether the station is ADA or not
      if (handicap == 1)
        this.handicapValueLbl.Text = "Yes";
      else
        this.handicapValueLbl.Text = "No";

      // change the label to show the latitude and longitude of the station
      this.locationValueLbl.Text = string.Format("({0}, {1})", latitude, longitude);

      // clear the stops list box each time a new stop is clicked so data is accurate
      this.linesListBox.Items.Clear();
      this.linesListBox.Refresh();

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
      DataSet ds2 = executeAdapaterSQL(sql);

      int numLines = 0;

      // fill the list box with the appropriate data
      foreach (DataRow row in ds2.Tables["TABLE"].Rows)
      {
        string line = Convert.ToString(row["Color"]);
        this.linesListBox.Items.Add(line);

        numLines++;
      }

      this.linesLbl.Text = String.Format("Lines: {0}", numLines);
    }
    //---------------------------------------------------------------------
  }
}
