namespace project7
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.top10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadStationsListBox = new System.Windows.Forms.ListBox();
      this.numStationsLabel = new System.Windows.Forms.Label();
      this.totalRdshpLbl = new System.Windows.Forms.Label();
      this.totalValueLbl = new System.Windows.Forms.Label();
      this.avgRdshpLbl = new System.Windows.Forms.Label();
      this.avgValueLbl = new System.Windows.Forms.Label();
      this.percentRdshpLbl = new System.Windows.Forms.Label();
      this.percentValueLbl = new System.Windows.Forms.Label();
      this.satLbl = new System.Windows.Forms.Label();
      this.sunHolLbl = new System.Windows.Forms.Label();
      this.wkdyLbl = new System.Windows.Forms.Label();
      this.wkdyValueLbl = new System.Windows.Forms.Label();
      this.satValueLbl = new System.Windows.Forms.Label();
      this.sunHolValueLbl = new System.Windows.Forms.Label();
      this.stopsLabel = new System.Windows.Forms.Label();
      this.stopsListBox = new System.Windows.Forms.ListBox();
      this.handicapLbl = new System.Windows.Forms.Label();
      this.directionLbl = new System.Windows.Forms.Label();
      this.handicapValueLbl = new System.Windows.Forms.Label();
      this.directionValueLbl = new System.Windows.Forms.Label();
      this.locationLbl = new System.Windows.Forms.Label();
      this.locationValueLbl = new System.Windows.Forms.Label();
      this.linesLbl = new System.Windows.Forms.Label();
      this.linesListBox = new System.Windows.Forms.ListBox();
      this.txtboxDBFilename = new System.Windows.Forms.TextBox();
      this.findLbl = new System.Windows.Forms.Label();
      this.findTxtBox = new System.Windows.Forms.TextBox();
      this.updateADA = new System.Windows.Forms.Button();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.top10ToolStripMenuItem,
            this.findToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(961, 28);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fIleToolStripMenuItem
      // 
      this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
      this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
      this.fIleToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
      this.fIleToolStripMenuItem.Text = "File";
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      this.loadToolStripMenuItem.Size = new System.Drawing.Size(117, 26);
      this.loadToolStripMenuItem.Text = "Load";
      this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
      // 
      // top10ToolStripMenuItem
      // 
      this.top10ToolStripMenuItem.Name = "top10ToolStripMenuItem";
      this.top10ToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
      this.top10ToolStripMenuItem.Text = "Top-10";
      this.top10ToolStripMenuItem.Click += new System.EventHandler(this.top10ToolStripMenuItem_Click);
      // 
      // findToolStripMenuItem
      // 
      this.findToolStripMenuItem.Name = "findToolStripMenuItem";
      this.findToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
      this.findToolStripMenuItem.Text = "Find";
      this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
      // 
      // loadStationsListBox
      // 
      this.loadStationsListBox.FormattingEnabled = true;
      this.loadStationsListBox.ItemHeight = 16;
      this.loadStationsListBox.Location = new System.Drawing.Point(12, 42);
      this.loadStationsListBox.Name = "loadStationsListBox";
      this.loadStationsListBox.Size = new System.Drawing.Size(246, 420);
      this.loadStationsListBox.TabIndex = 1;
      this.loadStationsListBox.SelectedIndexChanged += new System.EventHandler(this.loadStationsListBox_SelectedIndexChanged);
      // 
      // numStationsLabel
      // 
      this.numStationsLabel.AutoSize = true;
      this.numStationsLabel.BackColor = System.Drawing.SystemColors.Info;
      this.numStationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numStationsLabel.Location = new System.Drawing.Point(12, 471);
      this.numStationsLabel.Name = "numStationsLabel";
      this.numStationsLabel.Size = new System.Drawing.Size(140, 24);
      this.numStationsLabel.TabIndex = 2;
      this.numStationsLabel.Text = "No file loaded";
      // 
      // totalRdshpLbl
      // 
      this.totalRdshpLbl.AutoSize = true;
      this.totalRdshpLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.totalRdshpLbl.Location = new System.Drawing.Point(289, 99);
      this.totalRdshpLbl.Name = "totalRdshpLbl";
      this.totalRdshpLbl.Size = new System.Drawing.Size(157, 24);
      this.totalRdshpLbl.TabIndex = 3;
      this.totalRdshpLbl.Text = "Total Ridership:";
      // 
      // totalValueLbl
      // 
      this.totalValueLbl.AutoSize = true;
      this.totalValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.totalValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.totalValueLbl.Location = new System.Drawing.Point(460, 101);
      this.totalValueLbl.Name = "totalValueLbl";
      this.totalValueLbl.Size = new System.Drawing.Size(133, 20);
      this.totalValueLbl.TabIndex = 4;
      this.totalValueLbl.Text = "0                   ";
      // 
      // avgRdshpLbl
      // 
      this.avgRdshpLbl.AutoSize = true;
      this.avgRdshpLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.avgRdshpLbl.Location = new System.Drawing.Point(289, 131);
      this.avgRdshpLbl.Name = "avgRdshpLbl";
      this.avgRdshpLbl.Size = new System.Drawing.Size(147, 24);
      this.avgRdshpLbl.TabIndex = 5;
      this.avgRdshpLbl.Text = "Avg Ridership:";
      // 
      // avgValueLbl
      // 
      this.avgValueLbl.AutoSize = true;
      this.avgValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.avgValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.avgValueLbl.Location = new System.Drawing.Point(461, 133);
      this.avgValueLbl.Name = "avgValueLbl";
      this.avgValueLbl.Size = new System.Drawing.Size(133, 20);
      this.avgValueLbl.TabIndex = 6;
      this.avgValueLbl.Text = "0                   ";
      // 
      // percentRdshpLbl
      // 
      this.percentRdshpLbl.AutoSize = true;
      this.percentRdshpLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.percentRdshpLbl.Location = new System.Drawing.Point(289, 160);
      this.percentRdshpLbl.Name = "percentRdshpLbl";
      this.percentRdshpLbl.Size = new System.Drawing.Size(127, 24);
      this.percentRdshpLbl.TabIndex = 7;
      this.percentRdshpLbl.Text = "% Ridership:";
      // 
      // percentValueLbl
      // 
      this.percentValueLbl.AutoSize = true;
      this.percentValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.percentValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.percentValueLbl.Location = new System.Drawing.Point(461, 164);
      this.percentValueLbl.Name = "percentValueLbl";
      this.percentValueLbl.Size = new System.Drawing.Size(132, 20);
      this.percentValueLbl.TabIndex = 8;
      this.percentValueLbl.Text = "0.00%            ";
      // 
      // satLbl
      // 
      this.satLbl.AutoSize = true;
      this.satLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.satLbl.Location = new System.Drawing.Point(650, 134);
      this.satLbl.Name = "satLbl";
      this.satLbl.Size = new System.Drawing.Size(70, 18);
      this.satLbl.TabIndex = 11;
      this.satLbl.Text = "Saturday:";
      // 
      // sunHolLbl
      // 
      this.sunHolLbl.AutoSize = true;
      this.sunHolLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.sunHolLbl.Location = new System.Drawing.Point(650, 163);
      this.sunHolLbl.Name = "sunHolLbl";
      this.sunHolLbl.Size = new System.Drawing.Size(91, 18);
      this.sunHolLbl.TabIndex = 12;
      this.sunHolLbl.Text = "Sun/Holiday:";
      // 
      // wkdyLbl
      // 
      this.wkdyLbl.AutoSize = true;
      this.wkdyLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wkdyLbl.Location = new System.Drawing.Point(650, 101);
      this.wkdyLbl.Name = "wkdyLbl";
      this.wkdyLbl.Size = new System.Drawing.Size(74, 18);
      this.wkdyLbl.TabIndex = 13;
      this.wkdyLbl.Text = "Weekday:";
      // 
      // wkdyValueLbl
      // 
      this.wkdyValueLbl.AutoSize = true;
      this.wkdyValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.wkdyValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.wkdyValueLbl.Location = new System.Drawing.Point(784, 101);
      this.wkdyValueLbl.Name = "wkdyValueLbl";
      this.wkdyValueLbl.Size = new System.Drawing.Size(16, 18);
      this.wkdyValueLbl.TabIndex = 14;
      this.wkdyValueLbl.Text = "0";
      // 
      // satValueLbl
      // 
      this.satValueLbl.AutoSize = true;
      this.satValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.satValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.satValueLbl.Location = new System.Drawing.Point(784, 134);
      this.satValueLbl.Name = "satValueLbl";
      this.satValueLbl.Size = new System.Drawing.Size(16, 18);
      this.satValueLbl.TabIndex = 15;
      this.satValueLbl.Text = "0";
      // 
      // sunHolValueLbl
      // 
      this.sunHolValueLbl.AutoSize = true;
      this.sunHolValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.sunHolValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.sunHolValueLbl.Location = new System.Drawing.Point(784, 165);
      this.sunHolValueLbl.Name = "sunHolValueLbl";
      this.sunHolValueLbl.Size = new System.Drawing.Size(16, 18);
      this.sunHolValueLbl.TabIndex = 16;
      this.sunHolValueLbl.Text = "0";
      // 
      // stopsLabel
      // 
      this.stopsLabel.AutoSize = true;
      this.stopsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stopsLabel.Location = new System.Drawing.Point(289, 199);
      this.stopsLabel.Name = "stopsLabel";
      this.stopsLabel.Size = new System.Drawing.Size(194, 24);
      this.stopsLabel.TabIndex = 17;
      this.stopsLabel.Text = "Stops at this station:";
      // 
      // stopsListBox
      // 
      this.stopsListBox.FormattingEnabled = true;
      this.stopsListBox.ItemHeight = 16;
      this.stopsListBox.Location = new System.Drawing.Point(293, 232);
      this.stopsListBox.Name = "stopsListBox";
      this.stopsListBox.Size = new System.Drawing.Size(301, 228);
      this.stopsListBox.TabIndex = 18;
      this.stopsListBox.SelectedIndexChanged += new System.EventHandler(this.stopsListBox_SelectedIndexChanged);
      // 
      // handicapLbl
      // 
      this.handicapLbl.AutoSize = true;
      this.handicapLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.handicapLbl.Location = new System.Drawing.Point(600, 237);
      this.handicapLbl.Name = "handicapLbl";
      this.handicapLbl.Size = new System.Drawing.Size(194, 20);
      this.handicapLbl.TabIndex = 19;
      this.handicapLbl.Text = "Handicap accessible?";
      // 
      // directionLbl
      // 
      this.directionLbl.AutoSize = true;
      this.directionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.directionLbl.Location = new System.Drawing.Point(600, 270);
      this.directionLbl.Name = "directionLbl";
      this.directionLbl.Size = new System.Drawing.Size(167, 20);
      this.directionLbl.TabIndex = 20;
      this.directionLbl.Text = "Direction of travel:";
      // 
      // handicapValueLbl
      // 
      this.handicapValueLbl.AutoSize = true;
      this.handicapValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.handicapValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.handicapValueLbl.Location = new System.Drawing.Point(819, 237);
      this.handicapValueLbl.Name = "handicapValueLbl";
      this.handicapValueLbl.Size = new System.Drawing.Size(40, 20);
      this.handicapValueLbl.TabIndex = 21;
      this.handicapValueLbl.Text = "N/A";
      // 
      // directionValueLbl
      // 
      this.directionValueLbl.AutoSize = true;
      this.directionValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.directionValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.directionValueLbl.Location = new System.Drawing.Point(819, 270);
      this.directionValueLbl.Name = "directionValueLbl";
      this.directionValueLbl.Size = new System.Drawing.Size(40, 20);
      this.directionValueLbl.TabIndex = 22;
      this.directionValueLbl.Text = "N/A";
      // 
      // locationLbl
      // 
      this.locationLbl.AutoSize = true;
      this.locationLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.locationLbl.Location = new System.Drawing.Point(600, 301);
      this.locationLbl.Name = "locationLbl";
      this.locationLbl.Size = new System.Drawing.Size(87, 20);
      this.locationLbl.TabIndex = 23;
      this.locationLbl.Text = "Location:";
      // 
      // locationValueLbl
      // 
      this.locationValueLbl.AutoSize = true;
      this.locationValueLbl.BackColor = System.Drawing.SystemColors.Info;
      this.locationValueLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.locationValueLbl.Location = new System.Drawing.Point(667, 330);
      this.locationValueLbl.Name = "locationValueLbl";
      this.locationValueLbl.Size = new System.Drawing.Size(40, 20);
      this.locationValueLbl.TabIndex = 24;
      this.locationValueLbl.Text = "N/A";
      // 
      // linesLbl
      // 
      this.linesLbl.AutoSize = true;
      this.linesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linesLbl.Location = new System.Drawing.Point(600, 359);
      this.linesLbl.Name = "linesLbl";
      this.linesLbl.Size = new System.Drawing.Size(66, 24);
      this.linesLbl.TabIndex = 25;
      this.linesLbl.Text = "Lines:";
      // 
      // linesListBox
      // 
      this.linesListBox.FormattingEnabled = true;
      this.linesListBox.ItemHeight = 16;
      this.linesListBox.Location = new System.Drawing.Point(618, 386);
      this.linesListBox.Name = "linesListBox";
      this.linesListBox.Size = new System.Drawing.Size(267, 68);
      this.linesListBox.TabIndex = 26;
      // 
      // txtboxDBFilename
      // 
      this.txtboxDBFilename.Location = new System.Drawing.Point(293, 473);
      this.txtboxDBFilename.Name = "txtboxDBFilename";
      this.txtboxDBFilename.Size = new System.Drawing.Size(510, 22);
      this.txtboxDBFilename.TabIndex = 27;
      this.txtboxDBFilename.Text = "|DataDirectory|\\CTA.mdf";
      this.txtboxDBFilename.TextChanged += new System.EventHandler(this.txtboxDBFilename_TextChanged);
      // 
      // findLbl
      // 
      this.findLbl.AutoSize = true;
      this.findLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.findLbl.Location = new System.Drawing.Point(290, 42);
      this.findLbl.Name = "findLbl";
      this.findLbl.Size = new System.Drawing.Size(45, 18);
      this.findLbl.TabIndex = 28;
      this.findLbl.Text = "Find:";
      // 
      // findTxtBox
      // 
      this.findTxtBox.Location = new System.Drawing.Point(340, 39);
      this.findTxtBox.Name = "findTxtBox";
      this.findTxtBox.Size = new System.Drawing.Size(113, 22);
      this.findTxtBox.TabIndex = 29;
      // 
      // updateADA
      // 
      this.updateADA.Location = new System.Drawing.Point(874, 232);
      this.updateADA.Name = "updateADA";
      this.updateADA.Size = new System.Drawing.Size(75, 33);
      this.updateADA.TabIndex = 30;
      this.updateADA.Text = "Update";
      this.updateADA.UseVisualStyleBackColor = true;
      this.updateADA.Click += new System.EventHandler(this.updateADA_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(961, 504);
      this.Controls.Add(this.updateADA);
      this.Controls.Add(this.findTxtBox);
      this.Controls.Add(this.findLbl);
      this.Controls.Add(this.txtboxDBFilename);
      this.Controls.Add(this.linesListBox);
      this.Controls.Add(this.linesLbl);
      this.Controls.Add(this.locationValueLbl);
      this.Controls.Add(this.locationLbl);
      this.Controls.Add(this.directionValueLbl);
      this.Controls.Add(this.handicapValueLbl);
      this.Controls.Add(this.directionLbl);
      this.Controls.Add(this.handicapLbl);
      this.Controls.Add(this.stopsListBox);
      this.Controls.Add(this.stopsLabel);
      this.Controls.Add(this.sunHolValueLbl);
      this.Controls.Add(this.satValueLbl);
      this.Controls.Add(this.wkdyValueLbl);
      this.Controls.Add(this.wkdyLbl);
      this.Controls.Add(this.sunHolLbl);
      this.Controls.Add(this.satLbl);
      this.Controls.Add(this.percentValueLbl);
      this.Controls.Add(this.percentRdshpLbl);
      this.Controls.Add(this.avgValueLbl);
      this.Controls.Add(this.avgRdshpLbl);
      this.Controls.Add(this.totalValueLbl);
      this.Controls.Add(this.totalRdshpLbl);
      this.Controls.Add(this.numStationsLabel);
      this.Controls.Add(this.loadStationsListBox);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "akhan227proj8";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem top10ToolStripMenuItem;
    private System.Windows.Forms.ListBox loadStationsListBox;
    private System.Windows.Forms.Label numStationsLabel;
    private System.Windows.Forms.Label totalRdshpLbl;
    private System.Windows.Forms.Label totalValueLbl;
    private System.Windows.Forms.Label avgRdshpLbl;
    private System.Windows.Forms.Label avgValueLbl;
    private System.Windows.Forms.Label percentRdshpLbl;
    private System.Windows.Forms.Label percentValueLbl;
    private System.Windows.Forms.Label satLbl;
    private System.Windows.Forms.Label sunHolLbl;
    private System.Windows.Forms.Label wkdyLbl;
    private System.Windows.Forms.Label wkdyValueLbl;
    private System.Windows.Forms.Label satValueLbl;
    private System.Windows.Forms.Label sunHolValueLbl;
    private System.Windows.Forms.Label stopsLabel;
    private System.Windows.Forms.ListBox stopsListBox;
    private System.Windows.Forms.Label handicapLbl;
    private System.Windows.Forms.Label directionLbl;
    private System.Windows.Forms.Label handicapValueLbl;
    private System.Windows.Forms.Label directionValueLbl;
    private System.Windows.Forms.Label locationLbl;
    private System.Windows.Forms.Label locationValueLbl;
    private System.Windows.Forms.Label linesLbl;
    private System.Windows.Forms.ListBox linesListBox;
    private System.Windows.Forms.TextBox txtboxDBFilename;
    private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
    private System.Windows.Forms.Label findLbl;
    private System.Windows.Forms.TextBox findTxtBox;
    private System.Windows.Forms.Button updateADA;
  }
}

