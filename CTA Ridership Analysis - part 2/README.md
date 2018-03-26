# University of Illinois (Chicago)
# CTA Database Analysis using C# and SQL

## Objective
This project uses data obtained from a Chicago Transit Authority database. 
There are 5 unique tables within the database that are all connected using primary and foreign keys.

Tables: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/tables.PNG


The program is implemented using C# to create the front-end graphical user interface, and SQL in the back-end to obtain data from the database via SQL queries. 
The program is able to use any type of database that is relevant to the functionality. 
Changing the database directory can be done at the bottom of the GUI by typing in the mdf file of the database.

Initial Build: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/cta.PNG

## Functionality
###### Loading & Stations
In order to start the program, the user will click 'File'->'Load' to obtain the necessary data from the database.
The largest list box that is located in the extreme left displays the names of all of the stations found in the database. 
The total number of stations is conveniently listed for the user directly below the listbox.
The data to the top right of the stations list box is the statistics calculated for the currently selected station, including the total number of riders for the station, the average ridership for the station, the overall percentage of riders for this station from the entire database, and the total number of riders for weekdays Saturdays and Sundays/Holidays.

Loading Execution: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/load.PNG

###### Stops
The medium sized list box located in the bottom center displays the names of all the stops located at the currently selected station. 
The data to the right of the stops list box is the data for the currently selected stop, including the direction of travel for the stop, the coordinates for the stop, the different lines that are connected to the stop, and whether or not the stop is handicap accessible. 
The possibility of future construction for handicap accessibility was kept in mind, therefore there is an update button that actually goes into the database and makes a permanent change to the database for handicap accessibility for the currently selected stop.

Stops: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/stops.PNG

###### Find
The 'Find' functionality finds all stations within the database that have similar names as the entered phrase. 
For example, I live in Skokie, Illinois and the closest CTA line to me is the yellow line. 
If I wanted to find all the data for the CTA yellow line, I simply have to type a phrase similar to 'yellow' and click find. 
Then the program will display all of the data found for all stations that contain the specified phrase within the database.

Find: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/find.PNG

###### Top-10
Finally, the 'Top-10' functionality simply displays the top-10 stations in terms of ridership within the database.

Top-10: https://github.com/akhan227/School-Projects/blob/master/CTAdatabaseAnalysis-C%23-SQL/cs341proj8/exampleExecution/top.PNG


###### Note:
The SQL queries are being created and executed as C# strings.
Project 7 for this class was this exact program, but all of the SQL queries were being created and executed all within the front-end. 
Project 8 for this class was to take this exact program and make it more realistic by creating back-end functions that do all of the SQL behind the scenes, while the front-end simply calls said functions.
