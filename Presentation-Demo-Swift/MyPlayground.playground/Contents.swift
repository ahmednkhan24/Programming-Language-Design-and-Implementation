//: Playground - noun: a place where people can play

import UIKit
import Foundation

//================================================
// Student class (default)
public class Student
{
    // class variables
    public var name: String

    // class constructor
    public init (n: String)
    {
        self.name = n
        print("Student \(name) created")
    }

    // called when the destructor is called
    deinit
    {
        print("Student \(name) deleted")
    }
}

// Grade class (default)
public class Grade
{
    // class variable
    public var score: Int

    // class constructor
    public init(s: Int)
    {
        self.score = s
        print("Grade of \(score) created")
    }

    // class destructor
    deinit
    {
        print("Grade of \(score) deleted")
    }
}
//================================================
//
//
//
//
//
//
//
//
//================================================
// Student class (strong reference cycle)
//public class Student
//{
//    // class variables
//    public var name: String
//    // array of grades
//    public var grades = [Grade]()
//
//    // class constructor
//    public init (n: String)
//    {
//        self.name = n
//        print("Student \(name) created")
//    }
//
//    // class destructor
//    deinit
//    {
//        print("Student \(name) deleted")
//    }
//}
//
//// Grade class (strong reference cycle)
//public class Grade
//{
//    // class variable
//    public var score: Int
//    public var aStudent: Student
////    public weak var aStudent: Student!
//
//
//    // class constructor
//    public init(s: Int, stdnt: Student)
//    {
//        self.score = s
//        self.aStudent = stdnt
//        aStudent.grades.append(self)
//
//        print("Grade of \(score) created")
//    }
//
//    // class destructor
//    deinit
//    {
//        print("Grade of \(score) deleted")
//    }
//}
//================================================




//====================MAIN========================
//var theStudent: Student? = Student(n: "Faraaz Khan")
//var theGrades:    Grade? = Grade(s: 50)
////var theGrades:    Grade? = Grade(s: 50, stdnt: theStudent!)
//
//
//theStudent = nil
//theGrades   = nil
//














