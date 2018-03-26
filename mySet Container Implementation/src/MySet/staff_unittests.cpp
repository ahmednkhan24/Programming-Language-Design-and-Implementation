/*staff_unittest1.cpp*/

//
// Staff unit tests for myset data structure.
//
// CS 341 Staff
// U. of Illinois, Chicago
// CS 341, Fall 2017
// Project #03
//

#include "stdafx.h"
#include "CppUnitTest.h"
#include <fstream>

#include "myset.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

int __staff_Student_creates;
int __staff_Student_copies; 
int __staff_Student_deletes;

class __staff_Student
{
private:
  int UIN;
  int Mid, Fnl;

public:
  __staff_Student()
  {
    UIN = Mid = Fnl = -1;
    __staff_Student_creates++;
  }

  __staff_Student(int uin, int mid, int fnl)
    : UIN(uin), Mid(mid), Fnl(fnl)
  {
    __staff_Student_creates++;
  }

  __staff_Student(const __staff_Student& other)
    : UIN(other.UIN), Mid(other.Mid), Fnl(other.Fnl)
  {
    __staff_Student_copies++;
  }

  ~__staff_Student()
  {
    __staff_Student_deletes++;
  }

  int getUIN() const
  {
    return UIN;
  }

  bool operator<(const __staff_Student& other) const
  {
    return this->UIN < other.UIN;
  }
};


namespace MySetUnitTests
{		
	TEST_CLASS(your_netid_tests)
	{
	public:

    TEST_METHOD(__original_test01)
    {
      myset<int>  s;

      Assert::IsTrue(s.size() == 0);
      Assert::IsTrue(s.empty());
    }

    TEST_METHOD(__original_test02)
    {
      myset<int>  s;

      Assert::IsTrue(s.size() == 0);
      Assert::IsTrue(s.empty());

      Assert::IsTrue(s.insert(12));

      Assert::IsTrue(s.size() == 1);
      Assert::IsTrue(!s.empty());
    }

    TEST_METHOD(__original_test03)
    {
      myset<string>  s;

      Assert::IsTrue(s.size() == 0);
      Assert::IsTrue(s.empty());

      Assert::IsTrue(s.insert("apple"));

      Assert::IsTrue(s.size() == 1);
      Assert::IsTrue(!s.empty());

      Assert::IsTrue(s["apple"]);
    }
		
    TEST_METHOD(__original_test04)
    {
      myset<string>  s;

      Assert::IsTrue(s.size() == 0);
      Assert::IsTrue(s.empty());

      Assert::IsTrue(s.insert("apple"));
      Assert::IsTrue(!s.insert("apple"));  // should not insert again

      Assert::IsTrue(s.size() == 1);
      Assert::IsTrue(!s.empty());

      Assert::IsTrue(s["apple"]);
      Assert::IsTrue(!s["pear"]);  // should not find
    }

		TEST_METHOD(__original_test05)
		{
      myset<int>  s;

      Assert::IsTrue(s.size() == 0);
      Assert::IsTrue(s.empty());

      Assert::IsTrue(s.insert(12));
      Assert::IsTrue(s.insert(71));
      s += 20;  // another way to insert:
      Assert::IsTrue(!s.insert(20));  // already inserted:

      Assert::IsTrue(s.size() == 3);
      Assert::IsTrue(!s.empty());

      Assert::IsTrue(s[71]);
      Assert::IsTrue(s[12]);
      Assert::IsTrue(s[20]);
      Assert::IsTrue(!s[11]);
      Assert::IsTrue(!s[999]);;
    }

    TEST_METHOD(__original_test06)
    {
      myset<int>  s;

      Assert::IsTrue(s.insert(10));
      Assert::IsTrue(s.insert(20));
      Assert::IsTrue(s.insert(30));

      Assert::IsTrue(s.size() == 3);
      Assert::IsTrue(!s.empty());

      auto iter = s.find(20);
      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 20);

      auto iter2 = s.find(40);
      Assert::IsTrue(iter2 == s.end());
    }

    TEST_METHOD(__original_test07)
    {
      myset<int>  s;

      Assert::IsTrue(s.insert(12));
      Assert::IsTrue(s.insert(71));
      Assert::IsTrue(s.insert(20));
      Assert::IsTrue(s.insert(2));
      Assert::IsTrue(s.insert(99));

      Assert::IsTrue(s.size() == 5);
      Assert::IsTrue(!s.empty());

      auto iter = s.find(2);

      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 2);
      ++iter;
      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 12);
      ++iter;
      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 20);
      ++iter;
      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 71);
      ++iter;
      Assert::IsTrue(iter != s.end());
      Assert::IsTrue(*iter == 99);
      ++iter;
      Assert::IsTrue(iter == s.end());

      auto iter2 = s.find(1);
      Assert::IsTrue(iter2 == s.end());
    }

    TEST_METHOD(__original_test08)
    {
      myset<int>  s;

      for (int i = 1000; i >= 100; --i)
      {
        Assert::IsTrue(s.insert(i));
      }

      Assert::IsTrue(s.size() == 901);
      Assert::IsTrue(!s.empty());

      int j = 100;
      auto iter = s.begin();
      while (iter != s.end())
      {
        Assert::IsTrue(*iter == j);
        ++iter;
        ++j;
      }

      int k = 100;
      for (auto x : s)
      {
        Assert::IsTrue(x == k);
        ++k;
      }
    }


    //
    // Additional tests:
    //
    TEST_METHOD(__staff_copy_constructor_01)
    {
      myset<std::string>  V;

      std::vector<std::string> S = {
        "apples", "bananas", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan"
      };

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == S.size());

      //
      // copy constructor:
      //
      myset<std::string> V2 = V;

      V.insert("AAA");

      Assert::IsTrue(V2.size() == S.size());
      Assert::IsTrue(V.size() == S.size() + 1);

      Assert::IsTrue(V["apples"]);
      Assert::IsTrue(V["AAA"]);

      Assert::IsTrue(V2["apples"]);
      Assert::IsTrue(!V2["AAA"]);
    }

    void __staff_copy_constructor_02_Function(myset<std::string> V2, int origSize)
    {
      Assert::IsTrue(V2.size() == origSize);

      V2.insert("AAA");
      V2.insert("ZZZ");

      Assert::IsTrue(V2.size() == origSize + 2);

      Assert::IsTrue(V2["apples"]);
      Assert::IsTrue(V2["AAA"]);
      Assert::IsTrue(V2["ZZZ"]);
    }

    TEST_METHOD(__staff_copy_constructor_02)
    {
      myset<std::string>  V;

      std::vector<std::string> S = {
        "apples", "bananas", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan"
      };

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == S.size());

      //
      // copy constructor:
      //
      __staff_copy_constructor_02_Function(V, S.size());

      // no changes to V:
      Assert::IsTrue(V.size() == S.size());

      Assert::IsTrue(V["apples"]);
      Assert::IsTrue(!V["AAA"]);
      Assert::IsTrue(!V["ZZZ"]);
    }

    TEST_METHOD(__staff_operator_assign_01)
    {
      myset<std::string>  V;

      std::vector<std::string> S = {
        "apples", "bananas", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan"
      };

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == S.size());

      //
      // copy constructor:
      //
      myset<std::string> V2;

      V2.insert("AAA");
      V2.insert("ZZZ");

      V2 = V;

      V.insert("AAA");

      Assert::IsTrue(V2.size() == S.size());
      Assert::IsTrue(V.size() == S.size() + 1);

      Assert::IsTrue(V["apples"]);
      Assert::IsTrue(V["AAA"]);

      Assert::IsTrue(V2["apples"]);
      Assert::IsTrue(!V2["AAA"]);
      Assert::IsTrue(!V2["ZZZ"]);
    }

    TEST_METHOD(__staff_clear_01)
    {
      myset<std::string>  V;

      std::vector<std::string> S = {
        "apples", "bananas", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan"
      };

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == S.size());

      //
      // clear:
      //
      V.clear();

      Assert::IsTrue(V.size() == 0);
      Assert::IsTrue(!V["apples"]);

      Assert::IsTrue(V.insert("BBB"));
      Assert::IsTrue(V.insert("AAA"));
      Assert::IsTrue(V.insert("ZZZ"));
      Assert::IsTrue(V.insert("DDD"));

      Assert::IsTrue(V.size() == 4);

      Assert::IsTrue(V.insert("HHH"));

      Assert::IsTrue(V.size() == 5);

      Assert::IsTrue(V["AAA"]);
      Assert::IsTrue(V["BBB"]);
      Assert::IsTrue(V["ZZZ"]);
      Assert::IsTrue(V["DDD"]);
      Assert::IsTrue(V["HHH"]);
    }

    TEST_METHOD(__staff_clear_02)
    {
      myset<std::string>  V;

      std::vector<std::string> S = {
        "apples", "bananas", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan"
      };

      std::vector<std::string> Just4 = {
        "ZZZ", "YYY", "XXX", "AAA"
      };

      for (auto s : Just4)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == Just4.size());

      //
      // clear:
      //
      V.clear();

      Assert::IsTrue(V.size() == 0);
      Assert::IsTrue(!V["AAA"]);

      // now let's insert more items than it had, to make sure
      // it resets and grows correctly:

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      Assert::IsTrue(V.size() == S.size());

      Assert::IsTrue(V["apples"]);
      Assert::IsTrue(V["uic"]);
      Assert::IsTrue(!V["ZZZ"]);
      Assert::IsTrue(!V["AAA"]);
      Assert::IsTrue(!V["YYY"]);
      Assert::IsTrue(!V["XXX"]);
    }

    TEST_METHOD(__staff_data_file)
    {
      //
      // This one loads about 10,000 strings from a
      // file.  In "Debug" mode, could take a few mins
      // to run given O(N) insert.  But if you switch 
      // to "Release" mode (drop-down on Visual Studio 
      // toolbar), build, and run the tests, this one
      // should complete in a couple seconds.  
      //
      // If Debug mode takes > 10 mins, or release mode
      // takes > 10 secs, then something is wrong with
      // insert or find in terms of time complexity.
      //
      myset<std::string>  S;
      string line, first, last;

      {
        //ifstream file("..\\MySet\\fortune1000.txt");
        ifstream file("..\\MySet\\words-by-freq-top10K.txt");

        Assert::IsTrue(file.good());

        getline(file, line);  // N: # of names
        int N = stoi(line);
        int i = 1;

        while (getline(file, line))
        {
          if (i == 1)
            first = line;
          else if (i == N)
            last = line;

          Assert::IsTrue(S.insert(line));

          ++i;
        }

        Assert::IsTrue(S.size() == N);
      }

      Assert::IsTrue(S[first]);
      Assert::IsTrue(S[last]);

      Assert::IsTrue(!S.insert(first));
      Assert::IsTrue(!S.insert(last));

      // file is closed so we can reopen and test find.

      {
        //ifstream file("..\\MySet\\fortune1000.txt");
        ifstream file("..\\MySet\\words-by-freq-top10K.txt");

        Assert::IsTrue(file.good());

        getline(file, line);  // N: # of names

        while (getline(file, line))
        {
          Assert::IsTrue(S[line]);
          Assert::IsTrue(!S.insert(line));
        }
      }

    }//test

    TEST_METHOD(__staff_objects_01)
    {
      __staff_Student_creates = 0;
      __staff_Student_copies = 0;
      __staff_Student_deletes = 0;

      {
        __staff_Student kyang3(123, 0, 0);
        __staff_Student jlo8(234, 0, 0);

        myset<__staff_Student>  V;

        Assert::IsTrue(V.insert(__staff_Student(123, 100, 100)));
        Assert::IsTrue(V.size() == 1);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(!V[jlo8]);
      }

      int students = __staff_Student_creates + 
        + __staff_Student_copies;

      int deletes = __staff_Student_deletes;
      
      Assert::IsTrue(students == deletes);
    }

    TEST_METHOD(__staff_objects_02)
    {
      __staff_Student_creates = 0;
      __staff_Student_copies = 0;
      __staff_Student_deletes = 0;

      {
        __staff_Student kyang3(1, 0, 0);
        __staff_Student jhummel2(999, 0, 0);
        __staff_Student sdeitz2(1000, 0, 0);

        myset<__staff_Student>  V;

        for (int uin = 1; uin < 1000; ++uin)
        {
          Assert::IsTrue(V.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);      }

      int students = __staff_Student_creates +
        +__staff_Student_copies;

      int deletes = __staff_Student_deletes;

      Assert::IsTrue(students == deletes);
    }

    TEST_METHOD(__staff_objects_03)
    {
      __staff_Student_creates = 0;
      __staff_Student_copies = 0;
      __staff_Student_deletes = 0;

      {
        __staff_Student kyang3(1, 0, 0);
        __staff_Student jhummel2(999, 0, 0);
        __staff_Student sdeitz2(1000, 0, 0);

        myset<__staff_Student>  V;

        for (int uin = 1; uin < 1000; ++uin)
        {
          Assert::IsTrue(V.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);

        V.clear();

        Assert::IsTrue(V.size() == 0);
        Assert::IsTrue(!V[kyang3]);
        Assert::IsTrue(!V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);

        for (int uin = 1; uin < 6; ++uin)
        {
          Assert::IsTrue(V.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V.size() == 5);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(!V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);      }

      int students = __staff_Student_creates +
        +__staff_Student_copies;

      int deletes = __staff_Student_deletes;

      Assert::IsTrue(students == deletes);
    }

    TEST_METHOD(__staff_objects_04)
    {
      __staff_Student_creates = 0;
      __staff_Student_copies = 0;
      __staff_Student_deletes = 0;

      {
        __staff_Student kyang3(1, 0, 0);
        __staff_Student jhummel2(999, 0, 0);
        __staff_Student sdeitz2(1000, 0, 0);

        myset<__staff_Student>  V;

        for (int uin = 1; uin < 1000; ++uin)
        {
          Assert::IsTrue(V.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);

        // copy constructor:
        myset<__staff_Student>  V2 = V;

        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V2.size() == 999);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);
        Assert::IsTrue(V2[kyang3]);
        Assert::IsTrue(V2[jhummel2]);
        Assert::IsTrue(!V2[sdeitz2]);

        for (int uin = 1000; uin < 1005; ++uin)
        {
          Assert::IsTrue(V2.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V2.size() == 1004);
        Assert::IsTrue(V2[kyang3]);
        Assert::IsTrue(V2[jhummel2]);
        Assert::IsTrue(V2[sdeitz2]);      }

      int students = __staff_Student_creates +
        +__staff_Student_copies;

      int deletes = __staff_Student_deletes;

      Assert::IsTrue(students == deletes);
    }

    TEST_METHOD(__staff_objects_05)
    {
      __staff_Student_creates = 0;
      __staff_Student_copies = 0;
      __staff_Student_deletes = 0;

      {
        __staff_Student kyang3(1, 0, 0);
        __staff_Student jhummel2(999, 0, 0);
        __staff_Student sdeitz2(1000, 0, 0);
        
        myset<__staff_Student>  V;

        for (int uin = 1; uin < 1000; ++uin)
        {
          Assert::IsTrue(V.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);

        // assignment:
        myset<__staff_Student>  V2;

        for (int uin = 1; uin < 6; ++uin)
        {
          Assert::IsTrue(V2.insert(__staff_Student(uin, 100, 100)));
        }

        Assert::IsTrue(V2.size() == 5);
        Assert::IsTrue(V2[kyang3]);

        V2 = V;  // assignment copy:
        Assert::IsTrue(V.size() == 999);
        Assert::IsTrue(V2.size() == 999);

        Assert::IsTrue(V[kyang3]);
        Assert::IsTrue(V[jhummel2]);
        Assert::IsTrue(!V[sdeitz2]);
      }

      int students = __staff_Student_creates +
        +__staff_Student_copies;

      int deletes = __staff_Student_deletes;

      Assert::IsTrue(students == deletes);
    }

    TEST_METHOD(__staff_iterators_01)
    {
      myset<std::string>  V;
      myset<std::string>  V2;

      std::vector<std::string> S = {
        "apples", "oranges", "pizza",
        "cat", "dog", "lion", "tiger", "bear",
        "house", "boat", "car", "plane", "uic",
        "uci", "michigan", "bananas"
      };

      for (auto s : S)
        Assert::IsTrue(V.insert(s));

      std::vector<std::string> Just4 = {
        "ZZZ", "YYY", "XXX", "AAA"
      };

      for (auto s : Just4)
        Assert::IsTrue(V2.insert(s));

      Assert::IsTrue(V.size() == S.size());
      Assert::IsTrue(V2.size() == Just4.size());

      auto iter = V.begin();
      auto iter2 = V2.begin();

      Assert::IsTrue(*iter == "apples");
      Assert::IsTrue(*iter2 == "AAA");

      ++iter;
      ++iter2;
      Assert::IsTrue(*iter == "bananas");
      Assert::IsTrue(*iter2 == "XXX");

      ++iter;
      ++iter2;
      Assert::IsTrue(*iter == "bear");
      Assert::IsTrue(*iter2 == "YYY");

      ++iter;
      ++iter2;
      Assert::IsTrue(*iter == "boat");
      Assert::IsTrue(*iter2 == "ZZZ");

      ++iter;
      ++iter2;
      Assert::IsTrue(*iter == "car");
      Assert::IsTrue(iter2 == V2.end());

      ++iter;
      Assert::IsTrue(iter != V.end());
    }



	};
}