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
	public string[] facts;
	public List<string> reviewWords;
	
	// indices of already picked books so they aren't reused and can be accessed for review
	public List<int> reviewIndices; 

	public int numBooks;
	public int maxBooks;
	public Text numBooksCollected;
	public int currentBook;

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
		currentBook = 0;
		levelCount = 1;

		facts = new string[] {
			"Pointers should be prepended by a 'p' in most cases and you should place the * close to variable name, not pointer type.", 
			"Pointers are used to dynamically allocate memory space.",
			"& represents the address-of operator", 
			"* is called the dereference operator.", 
			"All pointers in a program are likely going to occupy the same amount of space in memory (the size in memory of a pointer depends on the platform where the program runs).", 
			"Note that the asterisk (*) used when declaring a pointer only means that it is a pointer and should not be confused with the dereference operator which is also written with an asterisk (*).", 
			"It is common to write pointer definitions with the asterisk closer to the variable name than to the type.", 
			"Fact8", 
			"Fact9", 
			"Fact10", 
			"Fact11", 
			"* is the dereference operator, and can be read as 'value pointed to by'", 
			"Fact13", 
			"Fact14", 
			"Fact15", 
			"Fact16", 
			"Fact17", 
			"Fact18", 
			"Fact19", 
			"Pointers are used to dynamically allocate memory space. This memory space must be freed once you are done using it to avoid memory leaks."

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
		//int randomNumber = Random.Range (0, words.Length);

		while (isWordUsed (currentBook)) {
			currentBook++;
		}

		reviewIndices.Add (currentBook); // add index to the list so it won't be picked more than once
			


		return facts [currentBook];
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
			reviewWords.Add (facts [i]); // add only the words that were picked;
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
