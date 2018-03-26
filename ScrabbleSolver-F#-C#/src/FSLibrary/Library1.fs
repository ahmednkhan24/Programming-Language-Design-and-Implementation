//
// My F# library for Scrabble.
//
// Ahmed N Khan 652469935 akhan227
// U. of Illinois, Chicago
// CS 341, Fall 2017
// Project #05
//


module MyLibrary

open System
open System.Runtime.InteropServices.ComTypes
open System.Collections.Specialized

#light


//
// explode a string into a list of characters.
// Example: "cat" -> ['c'; 'a'; 't']
//
let explode(s:string) =
  [ for c in s -> c ]


//
// implode a list L of characters back into a string.
// Example: implode ['c'; 'a'; 't'] -> "cat"
//
let implode L =
  let sb = System.Text.StringBuilder()
  let ignore = List.map (fun c -> sb.Append (c:char)) L
  sb.ToString()


//
// Initialize:
//
// This function is called ONCE at program startup to initialize any
// data structures in the library.  We use this function to input the
// Scrabble dictionary and build a list of legal Scrabble words.
//
let mutable WordList = []

let Initialize folderPath =
  let alphabetical = System.IO.Path.Combine(folderPath, "alphabetical.txt")
  WordList <- [ for line in System.IO.File.ReadAllLines(alphabetical) -> line ]
  printfn "%A" (List.length WordList)



//-------------------------------------------------------------------------------------------------
//
// contains:
//
// traverses the parameter 'word' and
// returns true if the 'word' can be created using
// the letters from parameter 'letters'
// returns false otherwise
//
let rec contains word letters =
    match word, letters with
    | [] , [] -> true     // base case 1: both lists empty              = true
    | [] , _  -> true     // base case 2: word is empty, letters not    = true
    | _ , []  -> false    // base case 3: word is not empty, letters is = false

    // recursive part

    // if the heads of both lists are equal, then that letter counts
    // continue recursion with the tails
    | hd::tl, hd2::tl2 -> if (hd = hd2) then
                                contains tl tl2

    // if the heads of both lists are not equal, the letter does not count, 
    // but the recursion continues until a base case is reached
    // continue the recursion using the same head, but second tail
                          else
                                contains word tl2
//-------------------------------------------------------
// 
// sortLists:
//
// predicate function for building the list
// explode's the strings to create lists, and then
// sorts them before recursion is used
//
let sortLists word letters =

    // explode the current word and then sort it
    let sortedWord = List.sort (explode word)

    // explode the letters and then sort it 
    let sortedLetters = List.sort (explode letters)
   
    contains sortedWord sortedLetters
//-------------------------------------------------------   
//
// BuildList:
//
// Builds the list 'L' that contains all possible words 
// in the given dictionary parameter 'dict' that 
// can be made using the parameter 'letters'
//
let rec BuildList dict letters L =
    match dict with

    // base case: reverse the built list and return
    | [] -> List.rev L    

    // recursive part

    // if the sortLists/contains functions return true
    // then add the current word to the list

    | hd::tl -> if (sortLists hd letters) then 
                    BuildList dict.Tail letters (hd::L)
    
    // if the sortLists/contains functions return false
    // then continue the recurison with the next word
                else
                   BuildList dict.Tail letters L
//-------------------------------------------------------
//-------------------------------------------------------                   
//
// possibleWords:
//
// Finds all Scrabble words in the Scrabble dictionary that can be 
// spelled with the given letters.  The words are returned as a list
// in alphabetical order.
//
// Example:  letters = "tca" returns the list
//   ["act"; "at"; "cat"; "ta"]
//
let possibleWords letters = 
  let A = BuildList WordList letters []
  A
//-------------------------------------------------------  
//-------------------------------------------------------------------------------------------------
//-------------------------------------------------------  
// 
// getScore:
// 
// returns the score value associated with the
// character parameter 'c'
//
let getScore c =
    if (c = 'a' || c = 'e' || c = 'i' || c = 'l' || 
        c = 'n' || c = 'o' || c = 'r' || c = 's' || c = 't' || c = 'u') then
        1
    else if (c = 'd' || c = 'g') then
        2
    else if (c = 'b' || c = 'c' || c = 'm' || c = 'p') then
        3
    else if (c = 'f' || c = 'v' || c = 'w' || c = 'h' || c = 'y') then
        4
    else if (c = 'k') then
        5
    else if (c = 'j' || c = 'x') then
        8
    else if (c = 'q' || c = 'z') then
        10
    else
        0
//-------------------------------------------------------  
// 
// calcSingle:
//
// calculates the total score of a single word
//
let rec calcSingle word score =
    match word with
    | [] -> score
    | hd::tl -> calcSingle tl (score + getScore hd)
//-------------------------------------------------------  
//
// scoreList:
//
// calculates the scores of each word in the List passed
// to it, and returns a list of tuples
//
let rec scoreList words L =
    match words with

    // base case, return the created list
    | [] -> L

    // recursive part

    // create a tuple of the head:'word' and it's score
    // adds tuple to the list and continues recursion

    | hd::tl -> scoreList tl ((hd, calcSingle (explode hd) 0)::L)
//-------------------------------------------------------  
//
// sort:
//
// Lambda expression used for sorting in F#
// reference: 
// https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/list.sortwith%5B't%5D-function-%5Bfsharp%5D
//
let sort (string1:string, int1:int) (string2:string, int2:int) =
    if (int1 < int2) then
        1
    else if (int1 > int2) then
        -1
    else 
        String.Compare(string1, string2)
//-------------------------------------------------------     
//
// wordsWithScores:
//
// Finds all Scrabble words in the Scrabble dictionary that can be 
// spelled with the given letters.  The words are then "scored"
// based on the value of each letter, and the results returned as
// a list of tuples in the form (word, score).  The list is ordered
// in descending order by score; if 2 words have the same score,
// they are ordered in alphabetical order.
//
// Example:  letters = "tca" returns the list
//   [("act",5); ("cat",5); ("at",2); ("ta",2)]
//
let wordsWithScores letters =
  let possWords = possibleWords letters
  let unsortedScores = scoreList possWords []
  let sortedScores = List.sortWith sort unsortedScores
  sortedScores
//-------------------------------------------------------  
//-------------------------------------------------------------------------------------------------
//------------------------------------------------------- 
//
// filterLetters:
//
// checks to see if the 'aWord' parameter
// matches the 'pattern' parameter
// while ignoring any asteriks 
//
let rec filterLetters pattern aWord =
    match pattern, aWord with

    // base case, reached the end of both lists, return true
    | [], [] -> true

    // recursive part

    // check to see if the recursive condition is met
    // if the heads are equal, or if the head is an asterik, continue

    | hd::tl, hd2::tl2 -> if ((hd = hd2) || (hd = '*')) then
                            filterLetters tl tl2
                          else
                            false
//------------------------------------------------------- 
//
// extractPat:
//
// removes the asteriks from the pattern
// by creating a new list
// for example, 
// if pattern = [ 'e'; '*'; '*'; 'h' ]
// builds and returns = [ 'e'; 'h' ]
//
let rec extractPat pattern L =
    match pattern with
    | [] -> List.rev L
    | hd::tl -> if (hd <> '*') then
                  extractPat tl (hd::L)
                else
                    extractPat tl L
//------------------------------------------------------- 
//
// createTuple:
//
// creates the list of tuples for wordsThatFitPattern
// takes a completed list of all words that fit the pattern,
// and creates a new list of tuples that contain the word
// and the score for that word
//
let rec createTuple list L =
    match list with
    | [] -> List.rev L
    | hd::tl -> createTuple tl ((hd, calcSingle (explode hd) 0)::L)
//------------------------------------------------------- 
//
// filterDictionary:
//
// returns a filtered dictionary of words that 
// satisfy the given criteria
//
let filterDictionary letters pattern = 

  // filters the dictionary to create a new dictionary
  // that only contains words that are of the exact length
  // of the pattern
  let wordsSizeOfPattern = List.filter (fun x -> (List.length (explode x)) = (List.length (explode pattern))) WordList

  // filters the already filtered list even further to only contain
  // words that match the pattern
  let wordsFormatOfPattern = List.filter (fun x -> (filterLetters (explode pattern) (explode x))) wordsSizeOfPattern
  wordsFormatOfPattern
//------------------------------------------------------- 
//
// wordsThatFitPattern:
//
// Finds all Scrabble words in the Scrabble dictionary that can be 
// spelled with the given letters + the letters in the pattern, such
// that those words all fit into the pattern.  The results are 
// returned as a list of tuples (word, score), in descending order by
// score (with secondary sort on the word in alphabetical order).
//
// Example:  letters = "tca" and pattern = "e**h" returns the list
//   [("each",9); ("etch",9); ("eath",7)]
//
let wordsThatFitPattern letters pattern = 

  // explode the letters
  let explodedLetters = explode letters

  let wordsFormatOfPattern = filterDictionary explodedLetters pattern

  // remove the asteriks from the pattern
  let patternWithoutAsteriks = extractPat (explode pattern) []

  // create the new list of letters to use for the words
  // by concatenating the given letters and the pattern 
  // without any of the asteriks
  let updatedLetters = explodedLetters @ patternWithoutAsteriks

  // list of just the words in the filtered dictionary that match the pattern
  // using the buildList function from part1 of the assignment
  let wrdsThtFitPat = BuildList wordsFormatOfPattern (implode updatedLetters) []

  // list of the tuples of the words that fit the pattern
  let tuplesThatFitPat = createTuple wrdsThtFitPat []
  
  // sort the tuples in order
  let sortedTuples = List.sortWith sort tuplesThatFitPat
  sortedTuples
//------------------------------------------------------- 
//-------------------------------------------------------------------------------------------------