/*main.cpp*/

//
// iClicker Analysis program in modern C++.
//
// <<Ahmed N Khan>>
// U. of Illinois, Chicago
// CS 341: Fall 2017
// Project 02
//

#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <sstream>
#include <vector>
#include <algorithm>
#include <numeric>
#include <functional>
#include <map>


#include "student.h"
#include "session.h"

using namespace std;

// file names to use
//
// L1709081300.xml
// L1708301259.xml
// L1709061256.xml


// function prototypes
bool FileExists(string filename);

string ExtractName(string line);
string ExtractCorrectAnswer(string line);
string ExtractClickerID(string line);
string ExtractClickerID(string line);
int AnsweredCorrect(string CorrectAnswer, char StudentAnswer);
char ExtractStudentAnswer(string line);

int ParseData(vector<Session>& Sessions, string xmlfilename);

void PrintTotalStats(vector<Session>& Sessions);
void IndividualStudentStats(vector<Session>& Sessions);
double GetPercentage(Session& aSession, int num);
int NumAnswers(Session& aSession, int num);



//====================================================================================================
int main()
{

	string txtfilename = "files.txt";

	cout << "**Starting**" << endl;

	if (!FileExists(txtfilename))
	{
		cout << "Error: '" << txtfilename << "' not found, exiting..." << endl;
		return -1;
	}

	int TotalNumQuestions = 0;		// total questions between all sessions
	vector<Session> Sessions;		// will hold data for all sessions

	string line;					// reads one line at a time from file.txt
	ifstream file(txtfilename);		// trust ifstream to open and close files

	while (getline(file, line))		// read in each xml file from files.txt
	{
		cout << ">> Parsing '" << line << "'..." << endl;
		TotalNumQuestions += ParseData(Sessions, line);		// get data for an indivdual session
	}

	cout << "\n**Class Analysis**" << endl;
	cout << ">>Total sessions: " << Sessions.size() << endl;
	cout << ">>Total questions: " << TotalNumQuestions << endl;
	
	// stats for all sessions
	PrintTotalStats(Sessions);

	cout << "\n**Student Analysis**" << endl;
	// will search each session for the clickerID the user will enter
	IndividualStudentStats(Sessions);

	cout << "\n\n**END**" << endl;
	//system("pause");
	return 0;
}
//====================================================================================================



//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// FileExists:
//
// Returns true if the file exists, false if not.
//
bool FileExists(string filename)
{
	ifstream file(filename);

	return file.good();
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// ExtractName()
//
// returns the name of the session
//
string ExtractName(string line)
{
	// The <ssnn ...> tag is where you’ll find the session name attribute
	// The name is surrounded by quotation marks, so the index of where
	// the name starts is after the first quotation mark, i.e. 
	// the index we found the first 's' in 'ssnn=', plus 6 to skip quotation
	int NameIndex = line.find("ssnn=\"") + 6;

	// the ending of the name is when we encounter the closing quotation mark
	int EndNameIndex = line.find("\"", NameIndex);

	// full name is the substring between NameIndex and EndNameIndex
	return line.substr(NameIndex, EndNameIndex - NameIndex);
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// ExtractCorrectAnswer()
//
// returns the string containing all right answers for the question
//
string ExtractCorrectAnswer(string line)
{
	// The correct answer is denoted by the attribute cans = "..."

	int AnswerIndex = line.find("cans=") + 6;

	// the ending of the correct answer is when we encounter the closing quotation mark
	int EndCansIndex = line.find("\"", AnswerIndex);

	// full correct answer is the substring between two indexes
	return line.substr(AnswerIndex, EndCansIndex - AnswerIndex);
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
// 
// ExtractClickerID()
//
// returns the iClicker ID for the line passed to it
//
string ExtractClickerID(string line)
{
	//  The idattribute is the clicker ID, and how we identify the student

	int IDIndex = line.find("id=") + 5;

	// the ending of the correct answer is when we encounter the closing quotation mark
	int EndIDIndex = line.find("\"", IDIndex);

	// full correct answer is the substring between two numbers
	return line.substr(IDIndex, EndIDIndex - IDIndex);
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// AnsweredCorrect()
//
// Compares student answer to correct answer(s)
// returns  1 if student answered correct
// returns  0 if student answered incorrect
// returns -1 if student did not answer question
//
int AnsweredCorrect(string CorrectAnswer, char StudentAnswer)
{
	if (StudentAnswer == '\"')		// student did not respond
		return -1;

	// search correct answers for student's response
	auto iter = find_if(CorrectAnswer.begin(), CorrectAnswer.end(),
				[=](const char c)
				{
					if (c == StudentAnswer)
						return true;
					else
						return false;
				});

	if (iter == CorrectAnswer.end())	// student did not answer it correctly
		return 0;
	else								// student answered question correctly
		return 1;
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
// 
// ExtractStudentAnswer()
//
// returns the students' final answer
//
char ExtractStudentAnswer(string line)
{
	int index = line.find(" ans=") + 6;

	return line[index];
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// ParseData():
//
// opens the one xml file, builds the map for that file, inserts that map into vector,
// returns the total number of questions for this session
//
int ParseData(vector<Session>& Sessions, string xmlfilename)
{	// open

	string line, SessionName;

	// let ifstream open and close the file
	ifstream file(xmlfilename);



	// start parsing the data

	// skip data until the session tag <ssn...> is found
	while (getline(file, line))
	{
		// the <ssn ...> tag is where you’ll find the session name attribute ssnn = "..."
		if (line.find("<ssn") == 0)
		{
			SessionName = ExtractName(line);
			break;
		}
	}

	// THE MAP
	map<string, Student> M;
	string CorrectAnswer, ClickerID;
	int NumberOfQuestions = 0;

	while (getline(file, line))
	{	// open while

		//  Each question begins with the <p ...> tag
		if (line.find(" <p") == 0)
		{
			NumberOfQuestions++;
			CorrectAnswer = ExtractCorrectAnswer(line);
		}
		else if (line.find("  <v") == 0)
		{	// open <v if 
			//cout << "Total Question " << NumberOfQuestions << " " << total << endl;
			// extract student data
			ClickerID			= ExtractClickerID(line);
			char StudentAnswer	= ExtractStudentAnswer(line);
			int AnsCorrect		= AnsweredCorrect(CorrectAnswer, StudentAnswer);

			// find if the student is already in the map
			auto MapIter = M.find(ClickerID);

			if (MapIter == M.end())		// the student was not found in the map, so insert
			{	// open if

				// check if the student answered the question and if he/she got it right...

				int numAns, numCor;

				if (AnsCorrect == 1)		// student is not in map, answered the question correctly
				{
					numAns = 1;
					numCor = 1;
				}
				else if (AnsCorrect == 0)	// student is not in map, did not answer the question correctly
				{
					numAns = 1;
					numCor = 0;
				}
				else						// student is not in map, did not answer the question
				{
					numAns = 0;
					numCor = 0;
				}

				// create the student
				Student aStudent(ClickerID, SessionName, numAns, numCor);

				M.insert(std::pair<string, Student>(ClickerID, aStudent));
			}	// close if
			else				// the student is in the map
			{	// open else

				// update the student's data 
				if (AnsCorrect == 1)
				{
					MapIter->second.IncrmAns();
					MapIter->second.IncrmCorr();
				}
				else if (AnsCorrect == 0)
				{
					MapIter->second.IncrmAns();
				}
			}	// close else
		}	// close <v if
	}	// close while

	Session ThisSession(SessionName, NumberOfQuestions, M);

	Sessions.push_back(ThisSession);

	return NumberOfQuestions;
}	// close
//----------------------------------------------------------------------------------------------------
//
// IndividualStudentStats():
//
// Outputs stats of all sessions of a student whose ClickerID corresponds to the user input
//
void IndividualStudentStats(vector<Session>& Sessions)
{	// open
	
	// counter will keep track of how many sessions desired id is not in
	int counter = 0;

	string id;
	cout << ">> Enter a clicker id (# to quit): ";
	cin >> id;

	while (id != "#")
	{	// open while 

		// all clicker ID's will be 8 characters look, therefore if
		// the length isn't 8 we know right away invalid input given
		if (id.size() != 8)
			cout << "** not found..." << endl;
		else
		{	// open else

			// search for the student in each session
			for (Session aSession : Sessions)
			{	// open for

				auto MAP = aSession.GetMap();

				auto iter = MAP.find(id);

				if (iter == MAP.end())		// if the student is not in this specific session
					counter++;
				else
				{
					cout << "  \"" << iter->second.GetSessionName() << "\": " << iter->second.GetNumAns() <<
						" out of " << aSession.GetNumQuestions() << " answered, " <<
						((double)iter->second.GetNumCorr() / aSession.GetNumQuestions() * 100) << "% correctly" << endl;
				}
			}	// close for

			if (counter == Sessions.size())			// if the ID wasn't found in any session, invalid input was given
				cout << "** not found..." << endl;

			counter = 0;
		}	// close else
		cout << "\n>> Enter a clicker id (# to quit): ";
		cin >> id;

	}	// close while
}	// close
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
// 
// PrintTotalStats():
//
// Outputs stats of all sessions
//
void PrintTotalStats(vector<Session>& Sessions)
{	// open

	// Answered stats
	cout << ">>Answered:" << endl;
	for (Session aSession : Sessions)
	{
		cout << "  \"" << aSession.GetSessionName() << "\": " << GetPercentage(aSession, 1) <<
			"% (" << aSession.GetNumQuestions() << " questions, " << aSession.GetMapSize() << " clickers, " <<
			NumAnswers(aSession, 1) << " answers)" << endl;
	}
	// Correct stats
	cout << ">>Correctly:" << endl;
	for (Session aSession : Sessions)
	{
		cout << "  \"" << aSession.GetSessionName() << "\": " << GetPercentage(aSession, 2) <<
			"% (" << aSession.GetNumQuestions() << " questions, " << aSession.GetMapSize() << " clickers, " <<
			NumAnswers(aSession, 2) << " correct answers)" << endl;
	}
}	// close
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// NumAnswers()
//
// accumulates the total number of question answers or total number of correct answers in the session
//
int NumAnswers(Session& aSession, int num)
{	// open

	int total = 0;

	// if we want only answered question stats
	if (num == 1)
	{
		total = accumulate(aSession.GetMapBegin(), aSession.GetMapEnd(), 0,
			[&](int a, const std::pair<string, Student> keyvaluepair)
		{
			return (a + keyvaluepair.second.GetNumAns());
		});
	}
	// if we want only correctly answered stats
	else
	{
		total = accumulate(aSession.GetMapBegin(), aSession.GetMapEnd(), 0,
			[&](int a, const std::pair<string, Student> keyvaluepair)
		{
			return (a + keyvaluepair.second.GetNumCorr());
		});
	}

	return total;
}	// close
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// GetPercentage():
//
// Calculates and returns the percentage of total answered questions or total correctly answered questions
//
double GetPercentage(Session& aSession, int num)
{
	int numAns = -1;

	// if we want only answered questions stats
	if (num == 1)
	{
		numAns = accumulate(aSession.GetMapBegin(), aSession.GetMapEnd(), 0,
			[&](int a, const std::pair<string, Student> keyvaluepair)
		{
			return (a + keyvaluepair.second.GetNumAns());
		});
	}
	// if we want only correctly answered stats
	else
	{
		numAns = accumulate(aSession.GetMapBegin(), aSession.GetMapEnd(), 0,
			[&](int a, const std::pair<string, Student> keyvaluepair)
		{
			return (a + keyvaluepair.second.GetNumCorr());
		});
	}

	double totalAnswers = aSession.GetMapSize() * aSession.GetNumQuestions();

	return (numAns / totalAnswers) * 100;
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
