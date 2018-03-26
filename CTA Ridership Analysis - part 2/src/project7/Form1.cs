//
// N-tier C# and SQL program to analyze CTA Ridership data.
//
// Ahmed Khan, 652469935, akhan227
// U. of Illinois, Chicago
// CS341, Fall2017
// Project #08
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

namespace project7
{
  public partial class Form1 : Form
  {
    List<BusinessTier.CTAStation> stationsList;
    List<BusinessTier.CTAStop> stopsList;

    public Form1()
    {
      stationsList = new List<BusinessTier.CTAStation>();
      stopsList = new List<BusinessTier.CTAStop>();
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
      try
      {
        string filename = this.txtboxDBFilename.Text;
        BusinessTier.Business bizTier;
        bizTier = new BusinessTier.Business(filename);

        bizTier.TestConnection();
      }
      catch
      {
        // ignore exceptions
      }
    }
    //---------------------------------------------------------------------
    //
    // txtboxDBFilename_TextChanged:
    //
    // user changed the database to be used in the program
    //
    private void txtboxDBFilename_TextChanged(object sender, EventArgs e)
    {

    }
    //---------------------------------------------------------------------
    //
    // updateADA_Click:
    //
    // user clicked the update button
    //
    private void updateADA_Click(object sender, EventArgs e)
    {
      // create a BusinessTier variable to use its functions
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);

	 // get the current ADA. It'll be either Yes or No
      string currentADA = this.handicapValueLbl.Text;

	 // get the name of the stop the user wants to update
	 string stopName = this.stopsListBox.Text;

	 // replace the single quotes in the name with double quotes
	 // so that sql query can be executed
	 stopName = stopName.Replace("'", "''");

	 int rowsModified = biztier.updateADA(stopName, currentADA);

	 if (rowsModified != 1)
	   MessageBox.Show("Update Failed");
	 else
	   MessageBox.Show("Update Successful");

	 // update the gui
	 if (currentADA == "Yes")
	   this.handicapValueLbl.Text = "No";
	 else
	   this.handicapValueLbl.Text = "Yes";

    }
    //---------------------------------------------------------------------
    //
    // findToolStripMenuItem_Click:
    //
    // user clicked the find option
    //
    private void findToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // create a BusinessTier variable to use its functions
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);

      string patternName = this.findTxtBox.Text;

      // replace the single quotes in the name with double quotes
      // so that sql query can be executed
      patternName = patternName.Replace("'", "''");

      // get the list of stations
      var similarStations = biztier.getSimilarStations(patternName);

      this.loadStationsListBox.Items.Clear();
      this.loadStationsListBox.Refresh();

      int numStations = 0;
      foreach (var s in similarStations)
      {
        this.loadStationsListBox.Items.Add(s.Name);

        numStations++;
      }

      // show the user how many stations were found
      this.numStationsLabel.Text = String.Format("Number of stations: {0}", numStations);

      // preselect the first index
      if (this.loadStationsListBox.Items.Count > 0)
        this.loadStationsListBox.SelectedIndex = 0;
    }
    //---------------------------------------------------------------------
    //
    // top10ToolStripMenuItem_Click:
    //
    // user clicked the 'Top 10' option
    //
    private void top10ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // create a BusinessTier variable to use its functions
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);

      // get the top 10 stations in order by ridership
      var top10 = biztier.GetTopStations(10);

      // get the total ridership associated with the top 10 stations in order
      var values = biztier.getTop10values();


      // use a string builder to create one string to display to the user
      StringBuilder sb = new StringBuilder();

      sb.Append("\t\tTop-10 stations in terms of ridership\n\n");
      int num = 1;

      foreach (var t in top10)
      {
        string total = string.Format("{0:n0}", values[num - 1]);

        sb.Append(num + ".  " + t.Name + ":  " + total + "\n\n");
        num++;
      }

	   
      // show the user the top 10 stations
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
     
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);

      // get a list of stations from the BusinessTier
      var stations = biztier.GetStations();

      // clear any contents that may have already been in the list
      stationsList.Clear();
      this.loadStationsListBox.Items.Clear();
      this.loadStationsListBox.Refresh();

      // add each name to the list box
      foreach (var s in stations)
      {
        this.stationsList.Add(s);
        this.loadStationsListBox.Items.Add(s.Name);
      }

      // show the user how many stations were found
      this.numStationsLabel.Text = String.Format("Number of stations: {0}", stationsList.Count());

      // preselect the first index
      if (this.loadStationsListBox.Items.Count > 0)
        this.loadStationsListBox.SelectedIndex = 0;
    }
    //---------------------------------------------------------------------
    // 
    // loadStationsListBox_SelectedIndexChanged:
    //
    // The user clicked an item in the station names list box
    //
    private void loadStationsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      
      // create a BusinessTier variable to use its functions
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);

      // get the station name that the user clicked on
      string stationName = this.loadStationsListBox.Text;

      // use lambda expression to find the desired station
      // before replacing any single quotes with double quotes
      var theStation = stationsList.Find(x => x.Name == stationName);


      // replace the single quotes in the name with double quotes
      // so that sql query can be executed
      stationName = stationName.Replace("'", "''");

      // get all the ridership info needed for this station
      var ridership = biztier.getRidership(stationName);
      
      // change the GUI texts to the appropriate values
      this.totalValueLbl.Text   = String.Format("{0:n0}", ridership.totalRidership);
      this.avgValueLbl.Text     = String.Format("{0:n0}/day", ridership.averageRidership);
      this.percentValueLbl.Text = String.Format("{0:0.00}%",ridership.percentRidership);
      this.wkdyValueLbl.Text    = String.Format("{0:n0}", ridership.weekdayRidership);
      this.satValueLbl.Text     = String.Format("{0:n0}", ridership.saturdayRidership);
      this.sunHolValueLbl.Text  = String.Format("{0:n0}", ridership.sundayHolidayRidership);

      
      // get the stops associated with this station
      var stops = biztier.GetStops(theStation.ID);
      
      // clear any data that may have already been there
      stopsList.Clear();
      this.stopsListBox.Items.Clear();
      this.stopsListBox.Refresh();

      // display stops to the user
      foreach (var s in stops)
      {
        this.stopsList.Add(s);
        this.stopsListBox.Items.Add(s.Name);
      }

      // preselect the first index
      if (this.stopsListBox.Items.Count > 0)
        this.stopsListBox.SelectedIndex = 0;
      else
      {
        this.directionValueLbl.Text = "N/A";
        this.handicapValueLbl.Text = "N/A";
        this.locationValueLbl.Text = "N/A";
        this.linesLbl.Text = "Stops: 0";
        this.linesListBox.Items.Clear();
        this.linesListBox.Refresh();
      }

      // show the user the number of stops at the station they chose
      this.stopsLabel.Text = String.Format("Stops at this station: {0}", stopsList.Count);
    }
    //---------------------------------------------------------------------
    //
    // stopsListBox_SelectedIndexChanged:
    //
    // the user clicked a stop from the stop list box to see it's info
    //
    private void stopsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      // create a BusinessTier variable to use its functions
      var biztier = new BusinessTier.Business(this.txtboxDBFilename.Text);


      // get the stop the user clicked
      string stopName = this.stopsListBox.Text;

      // get the stop details for the stop that the user clicked
      var stop = stopsList.Find(x => x.Name == stopName);

      // replace any single quotes so that the sql query does not crash
      stopName = stopName.Replace("'", "''");

      // change labels
      if (stop.ADA)
        this.handicapValueLbl.Text = "Yes";
      else
        this.handicapValueLbl.Text = "No";

      this.directionValueLbl.Text = stop.Direction;
      this.locationValueLbl.Text = String.Format("({0}, {1})", stop.Latitude, stop.Longitude);

      // clear any data that is in the list box before adding new data
      this.linesListBox.Items.Clear();
      this.linesListBox.Refresh();

      var lines = biztier.getLines(stopName);

      // keep track of how many lines are at this stop
      int numLines = 0;

      // add the data to the gui
      foreach (var s in lines)
      {
        this.linesListBox.Items.Add(s);
        numLines++;
      }

      this.linesLbl.Text = String.Format("Lines: {0}", numLines);

    }
    //---------------------------------------------------------------------

  }
}