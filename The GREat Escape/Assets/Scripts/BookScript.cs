using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookScript : MonoBehaviour {
	
	const int NUM_REVIEW_WORDS = 5;

	public static BookScript bookControl;
	public static int levelCount; // which level the player is on

	// list of all the words
	public string[] words;
	public List<string> reviewWords;
	
	// indices of already picked books so they aren't reused and can be accessed for review
	public List<int> reviewIndices; 

	public int numBooks;
	public int maxBooks;
	public Text numBooksCollected;

	void Awake(){
		if (bookControl == null) {
			DontDestroyOnLoad (gameObject);
			bookControl = this;
		} else {
            numBooks = bookControl.numBooks - 1;
            updateBookTracker();
            Destroy (gameObject);
		}
			
	}


	// Use this for initialization
	void Start () {
		levelCount = 1;

		words = new string[] {
			"'&': the address-of operator.\n\nGiven int n, '&n' will return the address in memory that contains n.",
			"'*(1)': the pointer.\n\n given int *intPtr, the variable intPtr has the ability to 'point' to an int variable in memory.",
			"*(2)': the dereference operator.\n\ngiven int *intPtr, int newInt = *intPtr, intPtr is dereferenced to return the value of the int intPtr is pointing to.",
			"'->': the arrow operator.\n\n For example, if a is a pointer, then a->b means member b of object pointed to by a. Synonymous to (*a).b.",
			"‘~className’: Destructor used when an object is destroyed.\n\nDestructors can be used to deallocate memory when an object is destroyed.",
			"new: this command allocates memory on the fly.\n\nThe new command allocates memory dynamically, on the heap, while the program is running.",
			"Encapsulation: data hiding.\n\nEncapsulation is a principle of object-oriented programming, providing information (interface) the user needs and hiding the code (implementation).",
			"Private: variables with this access modifier can be used only by member functions.\n\nBy default class members are private.",
			"Vector: contiguous storage locations for their elements (like arrays) but, they can change in size.\n\nEven if a vector is full, you can add another element by vector.add(element).",
			"Overloading: when functions with the same call name differ by their parameters. \n\n It is not enough to change just the return types when overloading a function.",
			"this: current object.\n\nthis is automatically set to the current object.",
			"Template: a generic type variable or class.\n\nTemplates make classes more flexible since they do not make variables/classes type specific.",
			"friend: lets a class specify ordinary functions that are allowed to access its private data.\n\nNon-member function can't access private data unless the class allows it.",
			"private: used to mark that a member cannot be used outside the class.\n\nThe keyword private is an access modifier.",
			"protected: allows access by derived classes.\n\nThe keyword protected is an access modifier."
		};

		reviewIndices = new List<int>();
		reviewWords = new List<string> ();
//		numBooksCollected.text = "Books: " + numBooks + "/" + maxBooks;
		numBooksCollected = GameObject.Find("BookScore").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
	public void updateLevelCount(){
		levelCount++;
	}
	//increment book count and change the text
	public void updateBookTracker(){
		numBooks++;
		print("numBooks" + numBooks);
        numBooksCollected = GameObject.Find("BookScore").GetComponent<Text>();
        print("maxBooks" + maxBooks);
        numBooksCollected.text = "Books: " + numBooks + "/5"; //+ maxBooks;
	}


	/*
	pickWord is called by PlayerController script
	when player collides with a book

	method generates random number 
	*/
	public string pickWord(){
		print("in pickWord after colliding with book");
		int randomNumber = Random.Range (0, words.Length);
		while (isWordUsed (randomNumber)) {
			print("that word was already used..get another");
			randomNumber = Random.Range (0, words.Length);
		}

		reviewIndices.Add (randomNumber); // add index to the list so it won't be picked more than once
			
		return words [randomNumber];
	}


	public void ResetBooks(){
		numBooks = 0;
		reviewWords.Clear ();
		//reviewIndices.Clear ();
	}

	public bool isWordUsed (int wordIndex){

		foreach (int i in reviewIndices) {
			if ( wordIndex == i ) {
				return true;
			}
		}
		return false;
	}

	public void setReviewWords(){
		foreach (int i in reviewIndices) {
			reviewWords.Add (words [i]); // add only the words that were picked;
		}
		//ReviewScript.updateReviewNum();
	}
	public List<string> getReviewWords(){
		return reviewWords;
	}

	public bool numBooksCheck(){
		return ( (numBooks % 5) == 0 ) ? true : false;
	}



}
