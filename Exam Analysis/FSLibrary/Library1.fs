//
// My F# library for exam score analysis.
//
// Ahmed N Khan 652469935 akhan227
// U. of Illinois, Chicago
// CS 341, Fall 2017
// Project #04
//


module MyLibrary

open System.Runtime.InteropServices.ComTypes

#light

//-------------------------------------------------------------------------
//
// InputScores
//
// Given the complete filepath to a text file of exam scores, 
// inputs the scores and returns them as a list of integers.
//
let InputScores filepath = 
  let L = [ for line in System.IO.File.ReadAllLines(filepath) -> line ]
  List.map (fun score -> System.Int32.Parse(score)) L

//-------------------------------------------------------------------------




// ALL HELPER FUNCTIONS GO HERE
//=========================================================================
//-------------------------------------------------------------------------
//
// traverses the list and keeps track of the lowest value found in the list
// while updating it when a new lower number is seen
//
let rec _minHelper minSoFar L =

  match L with

  // base case: reached end of list or an empty list, return whatever minSoFar is
  | [] -> minSoFar  
  
  // first check to see if we need to update minSoFar and then continue recursion
  | hd::tl -> if hd < minSoFar then
                  _minHelper hd tl
              else
                  _minHelper minSoFar tl
//-------------------------------------------------------------------------
//
// traverses the list and keeps track of the highest value found in the list
// while updating it when a new higher number is seen
//
let rec _maxHelper maxSoFar L =

  match L with

  // base case: reached end of list or an empty list, return whatever maxSoFar is
  | [] -> maxSoFar

  // first check to see if we need to update maxSoFar and then continue recursion
  | hd::tl -> if hd > maxSoFar then
                  _maxHelper hd tl
              else 
                  _maxHelper maxSoFar tl
//-------------------------------------------------------------------------
//
// add the head of the list, and then use recursion to add the tail elements
//
let rec sumList L =

  match L with

  // base case: empty list or reached end, return 0
  | [] -> 0

  // add the head of the list then continue the recursion
  | first::rest -> first + sumList rest
//-------------------------------------------------------------------------
//
// calculuates the median value of the list
// type casting from int to float in order to get accurate median
// if the number of elements is even
//
let rec _medianHelper L skip isEven = 

  match skip with

  | 0.0 when isEven -> let first = float (List.head L)     
                       let second = float (List.head (List.tail L))
                       float (first+second) / 2.0

  | 0.0 -> float (List.head L)

  | _ -> _medianHelper (List.tail L) (skip-1.0) isEven
//-------------------------------------------------------------------------
//
// converts an integer list to a float list with the same values
//
let IntListToFloat L =

  let Lfloat = List.map(fun i -> (float i)) L

  Lfloat
//-------------------------------------------------------------------------
//
// takes a min and max value, and then counts how many numbers in the 
// list passed to it is within that range
//
let Range L min max = 

  let L2 = List.filter (fun x -> ((x >= min) && (x <= max))) L

  let R = List.length L2

  R
//-------------------------------------------------------------------------
//
// creates the trend for the data, and adds each new data to the front of
// the list, so reversing it at the end is needed in order to keep order
//
let rec _trend L1 L2 L3 accumulator =

    match L1, L2, L3 with 

    // base case, if 1 list is empty all lists are empty
    | [], [], [] -> List.rev accumulator

    | hd1::tl1, hd2::tl2, hd3::tl3 -> if (hd1 < hd2) && (hd2 < hd3) then 
                                        _trend tl1 tl2 tl3 ('+'::accumulator)
                                      elif (hd1 > hd2) && (hd2 > hd3) then 
                                        _trend tl1 tl2 tl3 ('-'::accumulator)
                                      else 
                                        _trend tl1 tl2 tl3 ('='::accumulator)
 //-------------------------------------------------------------------------
//=========================================================================





//-------------------------------------------------------------------------
//
// NumScores
//
// Recursively counts the # of scores in the list.
//
// write this recursively, do not use higher-order and do not call built-in functions.  
// Tail-recursion not required; if you need helper function(s), add them
//
let rec NumScores L = 

  match L with

  | [] -> 0                  // base case: empty list - return 0

  | hd::tl -> 1 + NumScores tl     

//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// FindMin
//
// Recursively finds the min score in the list.
//
// write this recursively, do not use higher-order and do not call built-in functions.  
// Tail-recursion not required; if you need helper function(s), add them
//
let rec FindMin L = 

  let hd = List.head L

  _minHelper hd (List.tail L)

//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// FindMax
//
// Recursively finds the max score in the list.
//
// write this recursively, do not use higher-order and do not call built-in functions.  
// Tail-recursion not required; if you need helper function(s), add them
//
let rec FindMax L = 

  let hd = List.head L

  _maxHelper hd (List.tail L)

//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// Average
//
// Computes the average of a non-empty list of integers;
// the result is a real number (not an integer).
//
// write this recursively, do not use higher-order and do not call built-in functions.  
// Tail-recursion not required; if you need helper function(s), add them
//
let Average L = 

  match L with

  | [] -> 0.0       // base case: empty list, return 0 as a float

  | hd::tl -> (float (sumList L)) / (float (NumScores L))   // type cast to float
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// Median
//
// Computes the median of a non-empty list of integers;
// the result is a real number (not an integer) since the 
// median may be the average of 2 scores if the # of scores
// is even.
//
// use the built-in sort to sort the data, then return the median.  
// Remember that if |L| is even, then the median is the average of the middle elements.  
// Access the middle element(s) however you want.
//
let Median L = 

  // first sort the list
  let SortedList = List.sort L

  // then convert the list from an integer list to float list
  let FloatList = List.map(fun x -> float (x)) SortedList

  let skip = float (((List.length FloatList) - 1) / 2)

  let isEven = ((List.length FloatList) % 2) = 0

  _medianHelper FloatList skip isEven
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// StdDev
//
// Computes the standard deviation of a complete population
// defined by the integer list L.  Returns a real number.
//
// solve this however you want.  
// Recall that we talked about standard deviation in class on Monday 10/2 (Week 06, Day 15). 
// We are computing the standard deviationwhere L is considered a complete population
// 
// Helpful function: to convert an integer x to a real number, one approach is to use the float(x)function
//
let StdDev L = 

  // calculate the mean using the function we created
  let mean = Average L

  // turn the list to type float
  let Lfloat = IntListToFloat L

  // calculate the differences
  let diffs = List.map(fun x -> System.Math.Pow(x-mean, 2.0)) Lfloat

  // get the average of the differences then square root
  let result = System.Math.Sqrt(List.average diffs)

  result
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// Histogram
//
// Returns a list containing exactly 5 integers: [A;B;C;D;F].
// The integer A denotes the # of scores in L that fell in the
// range 90-100, inclusive.  B is the # of scores that fell in
// the range 80-89, inclusive.  C is the range 70-79, D is the
// range 60-69, and F is the range 0-59.
//
// solve however you want.
//
// Helpful function: to convert an integer x to a real number, one approach is to use the float(x)function
//
let Histogram L = 

  // get the number of A's in the list
  let A = Range L 90 100

  // get the number of B's in the list
  let B = Range L 80 89

  // get the number of C's in the list
  let C = Range L 70 79

  // get the number of D's in the list
  let D = Range L 60 69

  // get the number of F's in the list
  let F = Range L 0 59

  // create a list of the histogram, and return it
  let Hist = [A;B;C;D;F;]

  Hist
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//
// Trend
//
// Trend is given 3 lists of integer scores:  L1, L2, L3.  The lists are 
// non-empty, and |L1| = |L2| = |L3|.  L1 are the scores for exam 01, L2
// are the scores for exam 02, and L3 are the scores for exam 03.  The
// lists are in "parallel", which means student i has their scores at 
// position i in each list.  Example: the first exam in each list denote
// the exams for student 0.
//
// Trend returns a new list R such that for each student, R contains a '+'
// if the exam scores were score1 < score2 < score3 --- i.e. the scores
// are trending upward.  R contains a '-' if score1 > score2 > score3, i.e.
// the scores are trending downward.  Otherwise R contains '=' (e.g. if
// score1 < score2 but then score2 > score3).  
//
// solve however you want
//
let Trend L1 L2 L3 =

  let r = _trend L1 L2 L3 []

  [ for score in r -> score]