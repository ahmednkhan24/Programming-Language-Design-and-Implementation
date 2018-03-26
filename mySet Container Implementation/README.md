# University of Illinois (Chicago)

# mySet: Creating a generic C++ container

## Objective
Sets are containers that store unique elements following a specific order. The value of the element s in a set cannot be modified once in the container.

## Functions defined for mySet:

* myset() 
	– Default constructor: constructs a new, empty set		
* myset() 
	– Copy constructor		
* ~myset() 
	- Destroy all elements in set
* size() 
	- Returns # of elements in the set			
* empty() 
	- Returns true if set is empty, false if not				
* clear() 
	- Empties the set, destroying all the elements
* insert() 
	- Inserts e into the set				
* operator+=() 
	- Inserts e into the set	
* find() 
	- Finds the element e into the set. Uses binary search to find e	
* operator[]() 
	- Finds the element e into the set. Uses binary search to find e	
* operator=() 
	- Makes a deep copy of rhs (right-hand-side) and assigns into lhs (left-hand-side). Any existing elements in the lhs are destroyed	
* begin() 
	- Returns an iterator denoting the first element in the set				
* end() 
	- Returns an iterator denoting one past the last element in the iteration	


## Iterators
When creating a container in C++ it is important to make sure that it can be used for any type; integers, strings, objects, etc. To achieve this, C++ uses iterators to make containers as general as possible. 

###### Functions defined for Iterator:
* iterator()
	- Constructor
* ~iterator()			
	- Destructor
* operator++()
	- Advances to the next element in the set
* operator*()	
	- Returns the element denoted by this iterator
* operator!=()	
	- Returns true if "this" iterator != rhs iterator
* operator==()
	- Returns true if "this" iterator == rhs iterator

## Unit Testing
Testing software is just as crucial as creating it. A good programmer will always test his/her code to find and fix any possible bugs, but testing each individual functionality can be cumbersome and take up a lot of time. Unit Testing is creating small tests that test functionality of a specific part of code. In doing so, all the programmer has to do is click the ‘run tests’ button and will immediately be able to obtain feedback of whether his/her code functions properly. Unit testing also helps speed up debugging time because the programmer can simply go to whatever test did not pass and narrow down the search for the bug. 
For this program, all students created at least 3-5 non-trivial unit tests for a generic set container. The professor also provided his own tests. In total, we had hundreds of tests to run our code against. This code passed all tests.
