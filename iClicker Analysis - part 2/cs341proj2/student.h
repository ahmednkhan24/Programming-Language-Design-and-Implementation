/*student.h*/

#pragma once

using namespace std;

class Student
{
private:
	string  ClickerID;
	string  SessionName;
	int		NumAnswered;
	int		NumCorrect;
	
public:
	// default constructor:
	Student(std::string id, std::string SessName, int ans, int corr)
		: ClickerID(id), SessionName(SessName), NumAnswered(ans), NumCorrect(corr)
	{ }

	// getters
	string GetClickerID() const
	{
		return ClickerID;
	}
	string GetSessionName() const
	{
		return SessionName;
	}
	int GetNumAns() const
	{
		return NumAnswered;
	}
	int GetNumCorr() const
	{
		return NumCorrect;
	}
	
	// setters
	void IncrmAns()
	{
		NumAnswered++;
	}
	void IncrmCorr()
	{
		NumCorrect++;
	}
};