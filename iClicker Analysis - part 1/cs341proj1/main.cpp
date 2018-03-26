/*main.cpp*/

//
// iClicker Analysis program in modern C++.
//
// <<Ahmed N Khan>>
// U. of Illinois, Chicago
// CS 341: Fall 2017
// Project 01
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

#include "student.h"

using namespace std;

// file names to use
//
// L1709081300.xml
// L1708301259.xml
// L1709061256.xml


// function prototypes
bool FileExists(string filename);
void PrintDateAndTime(string filename);
string ExtractName(string line);
string ExtractCorrectAnswer(string line);
string ExtractClickerID(string line);
int AnsweredCorrect(string CorrectAnswer, char StudentAnswer);
char ExtractStudentAnswer(string line);
void CalculateAns(vector<Student> students, int NumberOfQuestions, int& AnsAll,
	int& AnsAtLeastHalf, int& AnsAtLeastOne, int&AnsNone);
void CalculateCor(vector<Student> students, int NumberOfQuestions, int& AnsAllCor,
	int& AnsAtLeastHalfCor, int& AnsAtLeastOneCor, int& AnsNoneCor);


int main()
{

	string filename;

	cout << "**Starting**" << endl;
	cout << "**Filename?**" << endl;
	cin >> filename;
	cout << filename << endl;

	if (!FileExists(filename))
	{
		cout << "Error: '" << filename << "' not found, exiting..." << endl;
		return -1;
	}


	// Open file and analyze the clicker data:
	cout << "**Analysis**" << endl;


	// let ifstream open and close the file
	ifstream file(filename);

	PrintDateAndTime(filename);

	string line, Name;


	// skip data until the session tag <ssn...> is found
	while (getline(file, line))
	{
		// the <ssn ...> tag is where you’ll find the session name attribute ssnn = "..."
		if (line.find("<ssn") == 0)
		{
			Name = ExtractName(line);
			break;
		}
	}

	int NumberOfQuestions = 0;		// counter for number of questions
	vector<Student> students;		// vector of students to keep track of each student
	string CorrectAnswer;

	string  ClickerID;

	while (getline(file, line))
	{	// open while loop

		//  Each question begins with the <p ...> tag
		if (line.find(" <p") == 0)
		{
			NumberOfQuestions++;
			CorrectAnswer = ExtractCorrectAnswer(line);
		}
		else if (line.find("  <v") == 0)
		{	// open <v if 

			// get the information for the student
			ClickerID = ExtractClickerID(line);
			char StudentAnswer = ExtractStudentAnswer(line);
			int AnsCorrect = AnsweredCorrect(CorrectAnswer, StudentAnswer);

			// check if the student already exists
			auto iter = find_if(students.begin(), students.end(),
				[&](const Student& s)
			{
				if (s.GetID() == ClickerID)
					return true;
				else
					return false;
			});

			if (iter == students.end())
			{
				// did not find the student in the vector, so insert
				int numAns, numCor;

				if (AnsCorrect == 1)		// student is not in vector, answered the question correctly
				{
					numAns = 1;
					numCor = 1;
				}
				else if (AnsCorrect == 0)	// student is not in the vector, did not answer the question correctly
				{
					numAns = 1;
					numCor = 0;
				}
				else						// student is not in the vector, did not answer the question
				{
					numAns = 0;
					numCor = 0;
				}

				Student aStudent(ClickerID, numAns, numCor);	// create new student
				students.push_back(aStudent);				// insert new student to the vector
			}
			else
			{
				// found the student in the vector
				if (AnsCorrect == 1)				// student is in vector, answered question correctly
				{
					iter->IncrementNumAnswered();
					iter->IncrementNumCorrect();
				}
				else if (AnsCorrect == 0)			// student is in the vector, did not answer the question correctly
					iter->IncrementNumAnswered();
			}
		}	// close <v if

	}	// close while loop


		// we are done parsing the data.
		// now calculate the statistics

		/*
		# of Students who answered:
		All questions
		at least half
		at least one
		none
		*/
	int AnsAll, AnsAtLeastHalf, AnsAtLeastOne, AnsNone;
	int AnsAllCor, AnsAtLeastHalfCor, AnsAtLeastOneCor, AnsNoneCor;


	CalculateAns(students, NumberOfQuestions, AnsAll, AnsAtLeastHalf, AnsAtLeastOne, AnsNone);
	CalculateCor(students, NumberOfQuestions, AnsAllCor, AnsAtLeastHalfCor, AnsAtLeastOneCor, AnsNoneCor);

	cout << "Name: \"" << Name << "\"" << endl;
	cout << "# questions: " << NumberOfQuestions << endl;
	cout << "# clickers: " << students.size() << endl;

	cout << "# of students who answered:" << endl;
	cout << "  All questions: " << AnsAll << endl;
	cout << "  At least half: " << AnsAtLeastHalf << endl;
	cout << "  At least one: " << AnsAtLeastOne << endl;
	cout << "  None: " << AnsNone << endl;

	cout << "# of students who answered correctly:" << endl;
	cout << "  All questions: " << AnsAllCor << endl;
	cout << "  At least half: " << AnsAtLeastHalfCor << endl;
	cout << "  At least one: " << AnsAtLeastOneCor << endl;
	cout << "  None: " << AnsNoneCor << endl;


	cout << "Students who answered < half:" << endl;

	for (Student s : students)
	{
		if (s.GetNumAnswered() < NumberOfQuestions / 2)
			cout << s.GetID() << endl;
	}

	cout << "Students with 0 correct:" << endl;

	for (auto s : students)
	{
		if (s.GetNumCorrect() == 0)
			cout << s.GetID() << endl;
	}

	cout << "**END**" << endl;

	//system("pause");
	return 0;
}
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
// PrintDateAndTime:
//
// extracts the date and time from the filename and prints it
//
void PrintDateAndTime(string filename)
{
	//L1709081300.xml
	// denotes the year 17, 
	// the month 09, 
	// the day 08, 
	// the hour 13, 
	// and the minute 00

	string year, month, day, hour, minute;

	year = filename.substr(1, 2);
	month = filename.substr(3, 2);
	day = filename.substr(5, 2);
	hour = filename.substr(7, 2);
	minute = filename.substr(9, 2);

	cout << "Date: " << month << "/" << day << "/" << year << endl;
	cout << "Time: " << hour << ":" << minute << endl;
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
	if (StudentAnswer == '\"')
		return -1;

	auto iter = find_if(CorrectAnswer.begin(), CorrectAnswer.end(),
		[=](const char c)
	{
		if (c == StudentAnswer)
			return true;
		else
			return false;
	});

	if (iter == CorrectAnswer.end())
		return 0;
	else
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
// CalculateAns()
//
// Calculates statistics for answered questions and returns using reference parameters
//
void CalculateAns(vector<Student> students, int NumberOfQuestions, int& AnsAll,
	int& AnsAtLeastHalf, int& AnsAtLeastOne, int&AnsNone)
{
	/*
	# of Students who answered:
	All questions
	at least half
	at least one
	none
	*/
	AnsAll = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumAnswered() == NumberOfQuestions)
			return true;
		else
			return false;
	});
	AnsAtLeastHalf = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumAnswered() >= NumberOfQuestions / 2)
			return true;
		else
			return false;
	});
	AnsAtLeastOne = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumAnswered() >= (NumberOfQuestions + 1) - NumberOfQuestions)
			return true;
		else
			return false;
	});
	AnsNone = count_if(students.begin(), students.end(),
		[](const Student &s)
	{
		if (s.GetNumAnswered() == 0)
			return true;
		else
			return false;
	});
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------
//
// CalculateCor()
//
// Calculates statistics for correct answers and returns using reference parameters
//
void CalculateCor(vector<Student> students, int NumberOfQuestions, int& AnsAllCor,
	int& AnsAtLeastHalfCor, int& AnsAtLeastOneCor, int& AnsNoneCor)
{
	/*
	# of Students who answred correctly:
	All questions
	at least half
	at least one
	none
	*/
	AnsAllCor = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumCorrect() == NumberOfQuestions)
			return true;
		else
			return false;
	});
	AnsAtLeastHalfCor = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumCorrect() >= NumberOfQuestions / 2)
			return true;
		else
			return false;
	});
	AnsAtLeastOneCor = count_if(students.begin(), students.end(),
		[=](const Student& s)
	{
		if (s.GetNumCorrect() >= (NumberOfQuestions + 1) - NumberOfQuestions)
			return true;
		else
			return false;
	});
	AnsNoneCor = count_if(students.begin(), students.end(),
		[](const Student &s)
	{
		if (s.GetNumCorrect() == 0)
			return true;
		else
			return false;
	});
}
//----------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------