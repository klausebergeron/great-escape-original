using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class BossQuestions : MonoBehaviour {

	public class Question
	{
		public string question;
		public List<string> choices;
		public string answer;
		public Question (string q, string ch1, string ch2, string ch3, string ch4, string a)
		{
			question = q;
			choices = new List<string>();
			choices.Add(ch1);
			choices.Add(ch2);
			choices.Add(ch3);
			choices.Add(ch4);
			answer = a;
		}
	}

	const int NumOptions = 13;
	const int NumChoices = 4;

	//const int NumOptions = 13;
	//public Text Question, Ans1, Ans2, Ans3;

	public int numWords;//20;//BookScript.bookControl.words.Length;
	//public char delim, delim2;
	//public char delim1, delim2, delim3, delim4, delim5;


	//public string wrdTmp, defTmp, currQuestion;
	public string questionTemp, choice1, choice2, choice3, choice4, answer, currQuestion;
	public static SortedDictionary<string,string> questionsAnswers; // map of questions and answers. Q is key, A is value

	public List<string> answerOptions; //holds words to test on
	public List<string> keyList;
	public static List<string> currAnswers; //Array used to make sure answers aren't repeated
	public string[] multiple_choice; //Array of multiple choice options

	//public string[] words;
	int numQuestions;
	public List<Question> questions;
	
	//public string[] negFB;

	
	/*
	correct_index is used in ButtonPushed script when a player chooses an answer
	*/
	public static int correct_index;
	/*
	questionsUsed has items added by ButtonPushed script
	whenever a player is done answering a question
	*/
	//public static List<string> questionsUsed;
	public static List<Question> questionsUsed;
	public static List<int> indexUsed;
	
	// Use this for initialization
	void Start () {
		questions = new List<Question>();
		questions.Add (new Question ("Choose the correct statement to define a 1d array of pointers to double with 10 elements. ",
			"'*' double ptrarr[10]", "double '*[10]' ptrarr", "double int '*ptrarr[10]'", "double '*prtarr[10]'", "3"));
		questions.Add (new Question ("What problem/error will likely result from the following code?\n\tint *p;\n\tfor (int i=0; i < 5; i++)\n\t\tp = new int[10]; ? ",
			"Dangling pointer", "Memory leak", "Type mismatch", "Segmentation fault", "1"));
		questions.Add (new Question ("Choose the correct statement to free a 1d array of pointers, ptrarr to double with 10 elements.",
			"free(ptrarr)", "delete ptrarr", "'delete *ptrarr'", " '~ *ptrarr'", "1"));
		questions.Add (new Question ("Which of the following are pointer variables in the following definition?\n\t'String *pName, name, address' ",
			"pName, name, and address", "Name", "pName", "Name", "2"));
		questions.Add (new Question ("What symbol represents the address-of operator ? ",
			"'&'", "'*'", "'->'", "None of the above", "0"));
		questions.Add (new Question ("You write the following code: \nint n = 5 \n'int *ptrToN = &n'\nWhat is the name of the operator in this fragment? '&n' ",
			"The and operator", "Pointer", "int pointer operator ", "address-of operator", "address-of operator"));		
		questions.Add (new Question ("You write the following code: \nint n = 5;\n 'int *ptrToN = &n'\nptrToN is of type ? ", 
			"pointer to int", "int", "Std::string",  "'char *'",  "pointer to int ")); 
		questions.Add (new Question ("You write the following code: \nint n = 5; \n'int *ptrToN = &n'\nWhat is the value of '*ptrToN' after the code finishes executing ? ",
			"5", "hexadecimal address of n", "binary address of 5", "address of 5",  "5"));
		questions.Add (new Question ("You write the following code: \nint n = 5; \nint '*ptrToN = n'\nWhat is the value of ptrToN after the code finishes executing ? ",
			"5", "hexadecimal address of n", "binary address of 5", "address of 5", "hexadecimal address of n")); 
		questions.Add (new Question ("In the following code, 'baz = *foo' \n'*' is called the ",
			"dot operator",  "star operator", "dereference operator", "Asterisk operator", "dereference operator")); 
		questions.Add (new Question ("Which symbol represents the dereference operator ? ",
			"'*'", "'.'", "'->'", "'&'", "'*'"));
		questions.Add (new Question ("'*' is the '_________' operator, and can be read as 'value pointed to by' ? ",
				"dereference", "value at start", "reference to malloc", "address-of", "dereference")); 
		questions.Add (new Question ("Choose the correct statement to declare a pointer that points to another pointer ? ",
				"'int *ptr'", "'int ptr**'", "'int *&ptr'", "'int ** ptr'", "int ** ptr")); 
		questions.Add (new Question ("Given the following declarations:\n\t'int * ptr1;'\n\t'int * ptr2;'\n\tptr1 = new int[5];\n\twhat statement will produce a shallow copy ? ",
			"'&ptr2 = ptr1'", "ptr2 = ptr1", "'ptr2 = new int[5]; for(int i = 0; i<5; i++) {ptr2[i] = ptr1[i]}'", "'ptr2 = new int[5]; for(int i = 0; i<5; i++) {*ptr1[i] = *ptr2[i]}'", "ptr2 = ptr1")); 
		questions.Add (new Question ("Given the following declarations:\n\t'int * ptr1;'\n\t'int * ptr2;'\n\tptr1 = new int[5];\n\twhat statement will produce a deep copy ? ",
			"'&ptr2 = ptr1'", "ptr2 = ptr1", "'ptr2 = new int[5]; for(int i = 0; i<5; i++) {ptr2[i] = ptr1[i]}'", "'ptr2 = new int[5]; for(int i = 0; i<5; i++) {*ptr1[i] = *ptr2[i]}'", "&ptr2 = ptr1")); 
		questions.Add (new Question ("Given the following declaration of a 2D integer array within a class-\n\t'int ** nums;'\n\t'nums = new int*[5]'\n\t'for(int i=0; i<5; i++)'\n\t\tnums[i] = new int[10];\n\t How would you implement the destructor ? ",
			"'delete [ ] nums'", "'delete [ ][ ] nums'", "'delete *nums'", "None of the above", "None of the above"));

		
		numQuestions = questions.Count;

		/*

		questionsAnswers = new SortedDictionary<string,string> ();
		/*
		delim = ':';
		delim2 = '.';
		*/
		/* delim1 = '?';
		delim2 = '#';
		delim3 = '$';
		delim4 = '%';
		delim5 = '!';*/

	//	answerOptions = new List<string>[NumOptions];
		//answerOptions = new List<string> (); //I Don't knwo what thsi is
		questionsUsed = new List<Question> ();
		//currAnswers = new List<string> (); // I dont' know what this is
		//parseCorrectWords ();
		indexUsed = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public string getAnswer() {
		return questions[indexUsed[indexUsed.Count-1]].answer;
	}

	

	//checks if index is in reviewIndicies
	public bool isInRevInd(int check){ 
		foreach (int i in BookScript.bookControl.reviewIndices) {
			if(i == check) { 
				return true;
			}
		}
		return false;
	}

	//checks if index is in reviewIndicies
	public bool isRevWord(string check){ 
		foreach (string i in BookScript.bookControl.reviewWords) {
			if(check == i) { 
				return true;
			}
		}
		return false;
	}

	/*
	public bool isQuesUsed(string check){
		foreach (string i in questionsUsed) {
			if (i == check)
				return true;
		}
		return false;
	}
	*/
	/*
	// sets map with questions and answers
	public void parseCorrectWords(int arrPos){

		//foreach (string str in BookScript.bookControl.words) {
		//foreach (string str in words) {
			/*
			if (isRevWord (str)) { // if in review, ignore it
				print("in set words for loop");
				//set review word in QA Map
				parseStr (str);
				questionsAnswers [answer] = questionTemp;
				continue; // go to next iteration
			}

			//only add words that weren't in review as non-correct answer options
			print("outside of set words for loop");
			parseStr (str);
			answerOptions.Add (questionTemp);
			print ("IN PARSEWORDS " + answer);

		}
		*/
		/*string str = words[arrPos];
		parseStr(str);
		//}
	}*/

	/*
	// breaks word,def string into separate word and definition
	/*public void parseStr(string toParse){
		int len = toParse.IndexOf (delim);
		int len2 = toParse.IndexOf (delim2);


		if (len > 0) {
			wrdTmp = toParse.Substring (0, len); // gets word up till the :
			defTmp = toParse.Substring(len+1, (len2-len+1) ); // get def up to .
			print(wrdTmp);
			print (defTmp);
		}

	}
	*/

	
	//version of parseStr for 4 mult choice 
	/*public void parseStr(string toParse){
		 print("in parseStr");
 	     int len = toParse.IndexOf (delim1);
 		 int len2 = toParse.IndexOf (delim2);
		 int len3 = toParse.IndexOf(delim3);
		 int len4 = toParse.IndexOf(delim4);
		 int len5 = toParse.IndexOf(delim5);

 		 if (len > 0) {	
      	      questionTemp = toParse.Substring (0, len); // gets question up to ?
      	      print("question is: " + questionTemp);
              choice1 = toParse.Substring(len+1, (len2 - len+1)); //gets first choice up to #
              choice2 = toParse.Substring(len2+1, (len3 - len2 +1)); // gets second choice up to $
              choice3 = toParse.Substring(len3+1, (len4 - len3 +1)); // gets second choice up to %
              choice4 = toParse.Substring(len4+1, (len5 - len4 +1)); // gets second choice up to !

		 	  answer = toParse.Substring(len4+1, (len5-len4+1)); // gets answer
		} 
	}*/

	public string getQuestionTempStr(){
		return questionTemp;
	}

	public int pickQuestion(){
		print("inside pickQuestion");
		
		//check level 
		Scene scene = SceneManager.GetActiveScene();
		print("Active scene is " + scene.name);
		int chosenQuestIndex = -1;
		if(scene.name == "Boss Battle"){
			chosenQuestIndex = Random.Range(0,4);
			while(indexUsed.Contains(chosenQuestIndex)){ //make sure you haven't used this question already
				chosenQuestIndex = Random.Range(0,4);
			}

		}
		if(scene.name == "Boss Battle 2"){
			chosenQuestIndex = Random.Range(5,9);
			while(indexUsed.Contains(chosenQuestIndex)){
				chosenQuestIndex = Random.Range(5,9);
			}

		}
		if(scene.name == "Boss Battle 3"){
			chosenQuestIndex = Random.Range(10,15);
			while(indexUsed.Contains(chosenQuestIndex)){
				chosenQuestIndex = Random.Range(10,15);
			}
		}

		
		indexUsed.Add(chosenQuestIndex); //add to questions used so it is not used again
		//parseCorrectWords(chosenQuestIndex);
		return chosenQuestIndex;
	}
	

	/*
	public string pickQuestion(){
		//list of all keys in questionAnswers
		print("inside pickQuestion");
		//check level first, if level 1 question 1-5
		int quest = Random.Range(0,5);

		keyList = new List<string> (questionsAnswers.Keys);

		//assign element at a random index from 0 to size of keyList to the string randomKey (will be our question)
		//string randomKey = keyList[Random.Range(0, keyList.Count-1)];
		string randomKey = keyList[Random.Range(0, 3)];
		print ("randomkey chose:");
		print (randomKey);
		while (isQuesUsed (randomKey)) {
		  randomKey = keyList[Random.Range(0, keyList.Count-1)];
		}
		assignAnswers (questionsAnswers [randomKey]);  
		return randomKey;
	}
	*/

	/*
	public void assignAnswers(string correct){
		correct_index = Random.Range (0, NumChoices-1);
		print("The correct index inside bossquestions is:");
		print(correct_index);
	
		multiple_choice = new string[NumChoices];
		multiple_choice [correct_index] = correct;
		currAnswers.Add (correct);

		for (int i = 0; i <= NumChoices-1; i++) {
			if (i != correct_index) {
				/*
				int rand = Random.Range (0, answerOptions.Count - 1);
				string ans = answerOptions [rand];


				while (currAnswers.Contains (ans)) {
					rand = Random.Range (0, answerOptions.Count - 1);
					ans = answerOptions [rand];
				}
				//if (answerOptions [rand] != correct )
				multiple_choice [i] = ans;
				currAnswers.Add (ans);
				*/
				/*multiple_choice[i] = answer;

			}
			else
				continue;
		
		}

		currAnswers.Clear ();

	}*/


/*	public bool isQuestionUsed(string word){ // check if question was already used
		foreach (string str in questionsAnswers.Keys) {
			if ( word == str ) {
				return true;
			}
		}
		return false;
	}*/

	//checks if player got question correct
	public bool checkAnswer(string playerAnswer){
		print("answer is "+ questions[indexUsed[indexUsed.Count-1]].answer);
		//if (questions[indexUsed[indexUsed.Count-1]].answer.Equals (playerAnswer)) {
		if (questions[indexUsed[indexUsed.Count-1]].answer.Equals (playerAnswer)) {
			return true;
		}
		return false;
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//parseCorrectWords ();
			pickQuestion();
		}
	}
}
