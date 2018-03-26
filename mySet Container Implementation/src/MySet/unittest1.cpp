/*unittest1.cpp*/

//
// Unit tests for mymap data structure.
//
// YOUR NAME
// U. of Illinois, Chicago
// CS 341, Fall 2017
// Project #03
//

#include "stdafx.h"
#include "CppUnitTest.h"

#include "myset.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace MyMapUnitTests
{
	TEST_CLASS(your_netid_tests)
	{
	public:


		//
		// Testing set of ints
		// insert
		// empty
		// find
		// clear
		// size
		//
		TEST_METHOD(akhan227_test01)
		{
			myset<int> s;

			Assert::IsTrue(s.empty());
			Assert::IsTrue(s.insert(5));
			Assert::IsTrue(!s.empty());
			Assert::IsTrue(s.insert(1));
			Assert::IsTrue(!s.insert(5));
			Assert::IsTrue(s.find(3) == s.end());
			Assert::IsTrue(s.find(1) != s.end());
			Assert::IsTrue(s.size() == 2);

			s.clear();
			Assert::IsTrue(s.size() == 0);
			Assert::IsTrue(s.empty());
		}

		// 
		// set of strings
		// size
		// empty
		// begin
		// end
		// dereference
		// clear
		//
		TEST_METHOD(akhan227_test02)
		{
			myset<string> s;

			Assert::IsTrue(s.size() == 0);
			Assert::IsTrue(s.empty());
			Assert::IsTrue(s.begin() == s.end());
			Assert::IsTrue(s.insert("World"));
			Assert::IsTrue(s.size() == 1);
			Assert::IsTrue(s.insert("Hello"));
			Assert::IsTrue(s.size() != 1);
			Assert::IsTrue(*s.begin() == "Hello");
			Assert::IsTrue(s.begin() != s.end());

			s.clear();
			Assert::IsTrue(s.empty());


		}


		//
		// testing dynamic growth of array
		//
		TEST_METHOD(akhan227_test03)
		{
			myset<int> s;

			Assert::IsTrue(s.insert(90));
			Assert::IsTrue(s.insert(80));
			Assert::IsTrue(s.insert(70));
			Assert::IsTrue(s.insert(60));
			Assert::IsTrue(s.insert(50));
			Assert::IsTrue(s.insert(40));
			Assert::IsTrue(s.insert(20));
			Assert::IsTrue(s.insert(30));
			Assert::IsTrue(s.insert(10));
			Assert::IsTrue(s.insert(4));
			Assert::IsTrue(s.size() == 10);
		}

		//
		// set of chars
		// insert 
		// no duplicates
		// lower case and upper case independent
		// numeric characters
		// size
		//
		TEST_METHOD(akhan227_test04)
		{
			myset<char> s;
			Assert::IsTrue(s.insert('A'));
			Assert::IsTrue(!s.insert('A'));
			Assert::IsTrue(s.insert('B'));
			Assert::IsTrue(s.insert('b'));
			Assert::IsTrue(s.insert('d'));
			Assert::IsTrue(s.insert('C'));
			Assert::IsTrue(s.size() == 5);
			Assert::IsTrue(s.insert('z'));
			Assert::IsTrue(s.insert('3'));
			Assert::IsTrue(s.size() == 7);
		}

		//
		// set of strings
		// empty
		// insert
		// insert similar but different values
		// [] operator
		// += operator
		// isFalse statements
		//
		TEST_METHOD(akhan227_test05)
		{
			myset<string> s;

			Assert::IsTrue(s.empty());
			Assert::IsTrue(s.insert("CS 341"));
			Assert::IsFalse(s.insert("CS 341"));
			Assert::IsTrue(s.insert("CS341"));
			Assert::IsFalse(s.insert("CS341"));
			Assert::IsTrue(s.size() == 2);
			Assert::IsTrue(s["CS 341"]);

			s += "hello";
			Assert::IsFalse(s.insert("hello"));
			Assert::IsTrue(s["hello"]);
			Assert::IsTrue(s.size() == 3);
		}



		TEST_METHOD(akhan227_testDeep)
		{
			myset<int> s;

		
			Assert::IsTrue(s.insert(2));
			Assert::IsTrue(s.insert(4));
			Assert::IsTrue(s.insert(1));

			myset<int> deep = s;

			Assert::IsTrue(*deep.begin() == 1);

		}

	};
}