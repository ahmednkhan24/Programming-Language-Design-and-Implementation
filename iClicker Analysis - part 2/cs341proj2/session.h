/*session.h*/

#pragma once

using namespace std;


class Session
{
private:
	string	SessionName;
	int		NumQuestions;
	map<string, Student> MapOfStudents;

public:
	// default constructor:
	Session(std::string Name, int num, std::map<string, Student> M)
		: SessionName(Name), NumQuestions(num), MapOfStudents(M)
	{ }

	// getters
	string GetSessionName() const
	{
		return SessionName;
	}
	int GetNumQuestions() const
	{
		return NumQuestions;
	}
	auto GetMap()
	{
		return MapOfStudents;
	}
	auto GetMapBegin()
	{
		return MapOfStudents.begin();
	}
	auto GetMapEnd()
	{
		return MapOfStudents.end();
	}
	int GetMapSize() const
	{
		return MapOfStudents.size();
	}
};
