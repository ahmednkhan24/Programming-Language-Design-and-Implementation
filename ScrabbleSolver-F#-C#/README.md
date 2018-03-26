# University of Illinois (Chicago)
# Scrabble Solver using F# and C#


## Objective
The program is implemented using C# to create the front-end graphical user interface, and F# in the back-end to obtain data from the dictionary text file.  


## Functionality
The program starts with the dictionary being built. The dictionary can be changed by entering in the name of the text file desired in the text box located at the bottom of the GUI.
The letters the program will use are in the 'Letters' text box located at the top left of the GUI. The default letters used are 'tca.' 

###### Possible Words
Clicking the 'Possible Words' button displays all possible words that are in the dictionary file that can be made by a combination of the given letters. The number of possible words that can be created is conveniently located at the bottom left of the GUI.

###### Words + Score
Clicking the 'Words + Scores' button performs the same functionality as 'Possible Words,' but also displays the scores that are earned by said words in order from greatest score to the least score.

###### Words that fit Pattern
Clicking the 'Words that fit Pattern' button takes the order of letters and stars located in the 'Pattern' text box and displays all possible words that are in the dictionary file that match the given pattern. The points associated with each possible word is also listed in order from greatest score to the least score.



F# is a unique language because it is a functional programming language; therefore, it does not follow the same programming conventions as more popular languages like Java or C++. F# relies mainly on immutable lists, and a heavy use of recursion.

Example Execution: https://github.com/akhan227/School-Projects/blob/master/ScrabbleSolver-F%23-C%23/exampleExecution.PNG

###### Note:
The front-end C# of this program was not done by the students, but it was given to all in the course by the professor. The focus of this project was using F# to obtain the desired functionality.
