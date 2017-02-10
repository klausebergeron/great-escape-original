using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookScript : MonoBehaviour {
	
	const int NUM_REVIEW_WORDS = 4;

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
			"'&': the address-of operator. \n\n given int n, '&n' will return the adress in memory that contains n",
			"'*(1)': the pointer. \n\n given int *intPtr, the variable intPtr has the ability to 'point' to an int variable in memory",
			"*(2)': the dereference operator. \n\n given: int *intPtr, int newInt = *intPtr, intPtr is dereferenced to return the value of the int intPtr is pointing to.",
			"'->': the arrow operator. \n\n \n One example: If a is a pointer, then a->b means member b of object pointed to by a. Synonymous to (*a).b",
			"Disseminate: Spread or disperse. \n\n Cover your mouth while sneezing so that you do not disseminate bacteria.",
			"Garrulous: Full of trivial conversation. \n\n Due to Jake's garrulous nature, asking him to keep a secret is impossible.",
			"Laud: Praise, glorify, or honor. \n\n The purpose of the awards is to laud students for their academic excellence.",
			"Cogent: Powerfully persuasive. \n\n The angry husband hired a detective to find cogent proof of his wife's extramarital affair.",
			"Coagulate: Change from a liquid to a thickened or solid state. \n\n Over time the milk will coagulate and become a bottle of disgusting clots.",
			"Lethargic: Deficient in alertness or activity. \n\n You can find my lethargic cat curled up asleep in the warmest spot she can find.",
			"Gainsay: Take contradict or deny. \n\n It would be unwise to gainsay your doctor's suggestions and stop taking your prescription medication. ",
			"Latent: Potentially existing but not presently evident or realized.\n\n The detective asked the lab technician to search the room for latent fingerprints.",
			"Aberrant: Markedly different from an accepted norm.\n\n Sarah's aberrant manners led to her being kicked out of the movie theater.",
			"Abeyance: Temporary cessation or suspension.\n\n Jane's cancer has returned after being in abeyance for nearly two years.",
			"Dissonance: A lack of agreement, may refer to sounds.\n\n There is a great deal of dissonance between what a liar says and does.",
			"Diverge: Move or draw apart.\n\n Now this is where the stories offered by the boy and the State begin to diverge slightly."
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
        numBooksCollected = GameObject.Find("BookScore").GetComponent<Text>();
        numBooksCollected.text = "Books: " + numBooks + "/" + maxBooks;
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
		return ( (numBooks % 4) == 0 ) ? true : false;
	}



}
