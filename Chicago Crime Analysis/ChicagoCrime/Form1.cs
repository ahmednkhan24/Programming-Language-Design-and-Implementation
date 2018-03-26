//
// GUI app to analyze Chicago crime data, using SQL and ADO.NET
//
// Ahmed Khan, akhan227, 652469935
// U. of Illinois, Chicago
// CS341, Fall2017
// Project 06
//


using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ChicagoCrime
{
  public partial class Form1 : Form
  {
     public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.clearForm();
    }

    private bool fileExists(string filename)
    {
      if (!System.IO.File.Exists(filename))
      {
        string msg = string.Format("Input file not found: '{0}'",
          filename);

        MessageBox.Show(msg);
        return false;
      }

      // exists!
      return true;
    }

    private void clearForm()
    {
      this.chart.Series.Clear();
      this.chart.Titles.Clear();
      this.chart.Legends.Clear();
    }

    private void cmdByYear_Click(object sender, EventArgs e)
    {
      //
      // Check to make sure database filename in text box actually exists:
      //
      string filename = this.txtFilename.Text;

      if (!fileExists(filename))
        return;

      this.Cursor = Cursors.WaitCursor;

      clearForm();

      //
      // Retrieve data from database:
      //


//-----------------------------------------------------------------------------
      /*
          #1)  
          By Year:  total # of crimes reported each year.  
          Plot year on the X axis, and total on the Y axis.
          Code is provided to create the plot the (x, y) points.  
          You’ll execute a single SQL query to retrieve the data, 
          and then add the necessary values into two lists named X and Y.  
          See the provided code for more details.
    */

      string version, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CrimeDB.mdf";

      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      // execute the query that gets the number of crimes reported each year
      string sql = string.Format(@"
                                  SELECT Year, Total = Count(*)
                                  FROM Crimes
                                  GROUP BY Year
                                  ORDER BY Year Desc;");

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);


      //string msg = db.State.ToString();  // debugging:
      //MessageBox.Show(sql);              // open?

      db.Close();

//-----------------------------------------------------------------------------

      //
      // Build a set of (x,y) points for plotting:
      //
      List<int> X = new List<int>();
      List<int> Y = new List<int>();

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        X.Add(Convert.ToInt32(row["Year"]));
        Y.Add(Convert.ToInt32(row["Total"]));
      }

      //
      // now graph as a line chart:
      //
      this.chart.Titles.Add("Total # of Crimes Per Year");

      var series = this.chart.Series.Add("total # of crimes");

      series.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series.Points.AddXY(X[i], Y[i]);
      }

      var legend = new Legend();
      legend.Docking = Docking.Top;
      this.chart.Legends.Add(legend);

      // 
      // done:
      //
      this.Cursor = Cursors.Default;
    }

    private void cmdArrested_Click(object sender, EventArgs e)
    {
      //
      // Check to make sure database filename in text box actually exists:
      //
      string filename = this.txtFilename.Text;

      if (!fileExists(filename))
        return;

      this.Cursor = Cursors.WaitCursor;

      clearForm();

      //
      // Retrieve data from database:
      //

      // NOTE: you can do this with one SQL query by summing the
      // Arrested column.  Alternatively, you can execute 2 queries,
      // one to get the total counts, and then another to just 
      // count where an arrest was made.
      //

      //-----------------------------------------------------------------------------

      /*
          #2)  Arrested:  
                total # of crimes reported each year vs. # arrested each year.  
                Plot year on the X axis; plot total and # arrested on the Y axis.
                You can execute two queries to retrieve the data you need, 
                though it can be done in one query.
      */


      string version, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CrimeDB.mdf";

      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      // execute the query that gets the total number of crimes reported each year,
      // and also calculates how many arrests were made in each year
      string sql = string.Format(@"
                                 Select Year, Total = Count(*), Arrested = Sum(Convert(Float, Arrested))
                                 from Crimes
                                 group by Year
                                 Order by Year Desc;");

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);


      //string msg = db.State.ToString();  // debugging:
      //MessageBox.Show(sql);              // open?

      db.Close();






      //-----------------------------------------------------------------------------

      //
      // Build a set of (x,y) points for plotting:
      //
      List<int> X = new List<int>();
      List<int> Y1 = new List<int>();
      List<int> Y2 = new List<int>();

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        X.Add(Convert.ToInt32(row["Year"]));
        Y1.Add(Convert.ToInt32(row["Total"]));
        Y2.Add(Convert.ToInt32(row["Arrested"]));
      }

      //
      // now graph as a line chart:
      //
      this.chart.Titles.Add("Total # of Crimes Per Year vs. Number Arrested");

      var series = this.chart.Series.Add("total # of crimes");

      series.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series.Points.AddXY(X[i], Y1[i]);
      }

      var series2 = this.chart.Series.Add("# arrested");

      series2.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series2.Points.AddXY(X[i], Y2[i]);
      }

      var legend = new Legend();
      legend.Docking = Docking.Top;
      this.chart.Legends.Add(legend);

      //
      // done:
      //
      this.Cursor = Cursors.Default;
    }

    private void cmdOneArea_Click(object sender, EventArgs e)
    {
      //
      // Check to make sure database filename in text box actually exists:
      //
      string filename = this.txtFilename.Text;

      if (!fileExists(filename))
        return;

      this.Cursor = Cursors.WaitCursor;

      clearForm();

      //
      // Retrieve data from database:
      //


      // NOTE: you might be able to do this with one SQL query,
      // but probably easier to just execute 2 queries: one to
      // get the total counts, and then another to get the counts
      // for the area specified by the user.  You may assume the
      // area name entered by the user exists (though FYI using a 
      // different type of join yields the necessary counts of 0
      // for plotting, and then it always works no matter what the
      // user enters).
      //

      //-----------------------------------------------------------------------------

      /*
          #3)  One Area:  
                total # of crimes reported each year vs. crimes in a particular area of the city. 
                Plot year on the X axis; plot total and # in area on the Y axis.  
                You may assume the user will enter a validarea name in the textbox, 
                such as “Loop”, “Austin”, or “Bridgeport”.  
                A complete listof areasis available here.
                Retrieving the datayou need may require executing three separate queries, which is fine; 
                you can do this in two using a join
      */

      string version, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CrimeDB.mdf";


      
      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();


      // get the area name from the text box
      string txtBoxString = this.txtArea.Text;

      // execute same query from part 1, total number of crimes reported each year
      string sql = string.Format(@"
                                  SELECT Year, Total = Count(*)
                                  FROM Crimes
                                  GROUP BY Year
                                  ORDER BY Year Desc;");

      // execute the new query that gets the total number of crimes each year for the specific area
      string sql2 = string.Format(@"
                                Select Year, Total = Count(*)
                                From Crimes
                                Where Area IN (Select Area 
                                               From Areas 
                                               Where AreaName = '{0}')
                                Group by Year
                                Order by Year Desc;", txtBoxString);


      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();
      DataSet ds2 = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);

      cmd.CommandText = sql2;
      adapter.Fill(ds2);
     


      //string msg = String.Format("word: {0}", txtBoxString);  // debugging:
      //MessageBox.Show(msg);
     // MessageBox.Show(sql);              // open?

      db.Close();





      //-----------------------------------------------------------------------------



      //
      // Build a set of (x,y) points for plotting:
      //
      List<int> X = new List<int>();
      List<int> Y1 = new List<int>();
      List<int> Y2 = new List<int>();

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        X.Add(Convert.ToInt32(row["Year"]));
        Y1.Add(Convert.ToInt32(row["Total"]));
      }

      foreach (DataRow row in ds2.Tables["TABLE"].Rows)
      {
        Y2.Add(Convert.ToInt32(row["Total"]));
      }

      //
      // now graph as a line chart:
      //
      this.chart.Titles.Add("Total # of Crimes Per Year vs. Particular Area");

      var series = this.chart.Series.Add("total # of crimes");

      series.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series.Points.AddXY(X[i], Y1[i]);
      }

      var series2 = this.chart.Series.Add("# in this area");

      series2.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series2.Points.AddXY(X[i], Y2[i]);
      }

      var legend = new Legend();
      legend.Docking = Docking.Top;
      this.chart.Legends.Add(legend);

      //
      // done:
      //
      this.Cursor = Cursors.Default;
    }

    private void cmdChicagoAreas_Click(object sender, EventArgs e)
    {
      //
      // Check to make sure database filename in text box actually exists:
      //
      string filename = this.txtFilename.Text;

      if (!fileExists(filename))
        return;

      this.Cursor = Cursors.WaitCursor;

      clearForm();

      //
      // Retrieve data from database:

      //-----------------------------------------------------------------------------
      /*
          #4) ChicagoAreas: 
                total crimes for each area of the city.  
                This can be done using one SQL query.
                Note that areas range from 1..77, with 0 used as the “unknown” 
                area for crimes reported in which the area was not specified.  
                Ignore area 0 when you retrieve the data (“WHERE Area > 0”)
      */

      string version, connectionInfo;
      SqlConnection db;

      version = "MSSQLLocalDB";
      filename = "CrimeDB.mdf";

      connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;", version, filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      // execute the query that gets the number of crimes reported each year
      string sql = string.Format(@"
                                     Select Area, Total = Count(*)
                                     From Crimes
                                     Where Area > 0
                                     Group by Area
                                     order by Area asc;");

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);


      //string msg = db.State.ToString();  // debugging:
      //MessageBox.Show(sql);              // open?

      db.Close();





      //-----------------------------------------------------------------------------





      //
      // Build a set of (x,y) points for plotting:
      //
      List<int> X = new List<int>();
      List<int> Y = new List<int>();

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        X.Add(Convert.ToInt32(row["Area"]));
        Y.Add(Convert.ToInt32(row["Total"]));
      }

      //
      // now graph as a line chart:
      //
      this.chart.Titles.Add("Total # of Crimes in each Chicago Area");

      var series = this.chart.Series.Add("total # of crimes");

      series.ChartType = SeriesChartType.Line;

      for (int i = 0; i < X.Count; ++i)
      {
        series.Points.AddXY(X[i], Y[i]);
      }

      var legend = new Legend();
      legend.Docking = Docking.Top;
      this.chart.Legends.Add(legend);

      //
      // done:
      //
      this.Cursor = Cursors.Default;
    }

  }//class
}//namespace
