/*student.h*/

#pragma once

using namespace std;

class Student
{
private:
	string  ClickerID;
	int     NumAnswered;
	int     NumCorrect;

public:
	// default constructor:
	Student(std::string id, int num1, int num2)
		: ClickerID(id), NumAnswered(num1), NumCorrect(num2)
	{ }

	// getters
	string GetID() const
	{
		return ClickerID;
	}
	int GetNumAnswered() const
	{
		return NumAnswered;
	}
	int GetNumCorrect() const
	{
		return NumCorrect;
	}

	// functions to update values
	void IncrementNumAnswered()
	{
		NumAnswered++;
	}
	void IncrementNumCorrect()
	{
		NumCorrect++;
	}


};
