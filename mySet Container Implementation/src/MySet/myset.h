/*myset.h*/

//
// myset data structure, i.e. a data structure modelled
// after std::set in modern C++.
//
// Ahmed Khan, 652469935, akhan227
// U. of Illinois, Chicago
// CS 341, Fall 2017
// Project #03
//

#pragma once

using namespace std;

/*
--------------------------------------------------
CHECKLIST

						DONE		TODO
Iterator:
	iterator()			X
	~iterator()			X
	operator++()		X
	operator*()			X
	operator!=()		X
	operator==()		X

MySet:
	myset()				X
	myset() deep		X
	~myset()			X
	size()				X
	empty()				X
	clear()				X
	insert()			X
	operator+=()		X
	find()				X
	operator[]()		X
	operator=()			X
	begin()				X
	end()				X
--------------------------------------------------
*/

template<typename T>
class myset
{
private:
	//
	// Allows iteration through the set in element order.
	//


	// iterator class
	//
	class iterator
	{	// start iterator class

	private:
		T *CurrentElement;

	public:
		//---------------------------------------------------------------------
		//
		// Constructor
		//
		iterator(T *InitialLocation)
		{
			CurrentElement = InitialLocation;
		}
		//---------------------------------------------------------------------
		//
		// Destructor
		//
		~iterator()
		{
			// since I used an array implementation, I don't "new" anything
			// in my iterator, therefore there's nothing to "delete"
		}
		//---------------------------------------------------------------------
		//
		// ++iter
		//
		// Advances to the next element in the set; elements are
		// visited in element order.  Operation is undefined if
		// iterator == end().
		//
		iterator& operator++()
		{
			// advance "this" iterator

			CurrentElement++;

			// return "this" updated iterator:

			return *this;
		}
		//---------------------------------------------------------------------
		//
		// *iter
		//
		// Returns the element denoted by this iterator; the 
		// operation is undefined if iterator == end().
		//
		T& operator*()
		{
			return *CurrentElement;
		}
		//---------------------------------------------------------------------
		//
		// lhs != rhs
		//
		// Returns true if "this" iterator != rhs iterator.
		//
		bool operator!=(const iterator& rhs)
		{
			if (this->CurrentElement != rhs.CurrentElement)
				return true;
			else
				return false;
		}
		//---------------------------------------------------------------------
		//
		// lhs == rhs
		//
		// Returns true if "this" iterator == rhs iterator.
		//
		bool operator==(const iterator& rhs)
		{
			if (this->CurrentElement == rhs.CurrentElement)
				return true;
			else
				return false;
		}
		//---------------------------------------------------------------------

	};	// end iterator class






	//---------------------------------------------------------------------
	//
	// Data members for myset:
	// variables go here

	T *dArray;		// array that will hold all the values
	int SIZE;		// will hold the number of items in the array
	int CAPACITY;	// will be the max capacity of the array at any given time



	// helper functions
	//---------------------------------------------------------------------
	void IncreaseSize()
	{
		CAPACITY = CAPACITY * 2;		// increase capacity

		T *NewArr = new T[CAPACITY];		// create new array

		for (int i = 0; i < SIZE; i++)
		{
			NewArr[i] = dArray[i];		// copy over the original array to new one
		}

		delete[] dArray;			// delete contents of original array

		dArray = NewArr;			// have original array point to new array
	}
	//---------------------------------------------------------------------
	// pass by reference to avoid copies, 
	// but make it const so that it won't be changed
	int BinarySearch(const T& SearchValue)
	{
		int Low = 0;
		int High = SIZE - 1;	// the SIZEth value is empty, so SIZE-1 is index of last value

								// loop will continue as long as Low <= High, ie
								// Low and High are not the same, or Low has not passed High
		while (Low <= High)
		{
			int Mid = (Low + High) / 2;		// update mid on each iteration

											// search for value in array
			if ((!(SearchValue < dArray[Mid])) && (!(dArray[Mid] <SearchValue)))
				return Mid;							// FOUND!
			else if (SearchValue < dArray[Mid])		// go left
				High = Mid - 1;
			else									// go right
				Low = Mid + 1;
		}

		return -1;			// SearchValue is not in the array
	}
	//---------------------------------------------------------------------






public:

	//---------------------------------------------------------------------
	//
	// Default constructor: constructs a new, empty set
	//
	myset()
	{
		CAPACITY = 4;		// start with an initial size of 4
		SIZE = 0;			// the array is empty so size is 0

		dArray = new T[CAPACITY];	// create the memory for the array
	}
	//---------------------------------------------------------------------
	//
	// Copy constructor:
	//
	myset(const myset& other)
	{
		SIZE = other.SIZE;
		CAPACITY = SIZE * 2;

		dArray = new T[CAPACITY];

		for (int i = 0; i < SIZE; ++i)
		{
			dArray[i] = other.dArray[i];
		}
	}
	//---------------------------------------------------------------------
	//
	// Destructor: destroy all elements in set.
	//
	~myset()
	{
		delete[] dArray;
	}
	//---------------------------------------------------------------------
	//
	// size()
	//
	// Returns # of elements in the set.
	//
	int size() const
	{
		return SIZE;
	}
	//---------------------------------------------------------------------
	// 
	// empty()
	//
	// Returns true if set is empty, false if not.
	//
	bool empty() const
	{
		if (SIZE == 0)
			return true;
		else
			return false;
	}
	//---------------------------------------------------------------------
	//
	// clear()
	//
	// Empties the set, destroying all the elements.  Afterwards, 
	// the size of the set is 0.
	//
	void clear()
	{
		delete[] dArray;

		SIZE = 0;
		CAPACITY = 4;

		dArray = new T[CAPACITY];
	}
	//---------------------------------------------------------------------
	//
	// insert(e)
	//
	// Inserts the element e into the set.  Returns true if e
	// was inserted, false if e was already in the set (and thus
	// not inserted again).  Elements are inserted in element 
	// order as defined by the < operator; this enables in order 
	// iteration.
	//
	// Requirements:
	//   1. Insert time must be <= O(N).
	//   2. Set grows as needed to accommodate new elements.
	//   3. Assumes only < operator; two elements x and y are
	//      equal if (!(x<y)) && (!(y<x)).
	//
	bool insert(const T& e)
	{
		// step 1: check if array is empty
		if (empty() == true)
		{
			dArray[0] = e;			// insert at first index
			SIZE++;
			return true;
		}

		// step 2: find insertion point of e in the array
		int InsertIndex = -1;

		for (int i = 0; i < SIZE; i++)
		{
			// step 2a: make sure e is not already in the array
			if ((!(dArray[i] < e)) && (!(e < dArray[i])))
				return false;		// repeats not allowed

			// step 2b: if e is less than current index, we 
			if (e < dArray[i])		// found insertion point
			{
				InsertIndex = i;
				break;
			}

			// step 2c: if e is greater than the last index,
			if (dArray[SIZE - 1] < e)
			{
				InsertIndex = SIZE;		// insertion point is at end of array
				break;
			}
		}


		// step 3: check if we ran out of room
		if (SIZE == CAPACITY - 1)
		{
			IncreaseSize();			// array is full, make more room
		}

		// step 4: move everything over in the array over by 1 to make room
		// for e, as long as e shouldn't be at the end of the array
		if (InsertIndex != SIZE)
		{
			for (int i = SIZE; i > InsertIndex; i--)
			{
				dArray[i] = dArray[i - 1];
			}
		}

		// insert e at the desired index
		dArray[InsertIndex] = e;

		// increment the size counter
		SIZE++;

		return true;		// insert SUCCESS!
	}
	//---------------------------------------------------------------------
	// 
	// += e
	//
	// Inserts e into the set; see insert(e).
	//
	myset& operator+=(const T& e)
	{
		// insert e into "this" set:

		this->insert(e);

		// return "this" updated set:

		return *this;
	}
	//---------------------------------------------------------------------
	//
	// find(e)
	//
	// Searches the set for the element e, returning an iterator
	// to e if found.  If e is not found, end() is returned, i.e.
	// an iterator denoting one past the last element.
	//
	// Requirements:
	//   1. Find time must be <= O(lgN).
	//   2. Assumes only < operator; two elements x and y are
	//      equal if (!(x<y)) && (!(y<x)).
	//
	iterator find(const T& e)
	{
		int Index = BinarySearch(e);

		if (Index != -1)
			return iterator(dArray + Index);
		else
			return end();
	}
	//---------------------------------------------------------------------
	//
	// [e]
	//
	// Returns true if set contains e, false if not.
	//
	// Requirements:
	//   1. operation time must be <= O(lgN).
	//   2. Assumes only < operator; two elements x and y are
	//      equal if (!(x<y)) && (!(y<x)).
	//
	bool operator[](const T& e)
	{
		if (BinarySearch(e) != -1)
			return true;
		else
			return false;
	}
	//---------------------------------------------------------------------
	//
	// lhs = rhs;
	//
	// Makes a deep copy of rhs (right-hand-side) and assigns into
	// lhs (left-hand-side).  Any existing elements in the lhs
	// are destroyed.
	//
	// Notes:
	//   1. this is essentially a clear() followed by a copy().
	//   2. the lhs operand is hidden --- it's "this" object.
	//
	myset& operator=(const myset& rhs)
	{
		// clear the lhs ie "this"
		delete[] dArray;

		// reset the size and capacity
		SIZE = rhs.SIZE;
		CAPACITY = SIZE * 2;
		dArray = new T[SIZE];

		for (int i = 0; i < rhs.SIZE; ++i)
		{
			dArray[i] = rhs.dArray[i];
		}

		// return "this" pointer
		return *this;
	}
	//---------------------------------------------------------------------
	//
	// begin()
	//
	// Returns an iterator denoting the first element in the
	// set; iteration is performed in element order (as defined
	// by the < operator).
	//
	iterator begin()
	{
		return iterator(dArray);
	}
	//---------------------------------------------------------------------
	//
	// end()
	//
	// Returns an iterator denoting one past the last element
	// in the iteration.
	//
	iterator end()
	{
		return iterator(dArray + SIZE);
	}
	//---------------------------------------------------------------------
};
