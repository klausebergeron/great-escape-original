﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BossQuestions : MonoBehaviour {

	const int NumOptions = 13;

	public char delim, delim2;
	public string wrdTmp, defTmp, currQuestion;
	public SortedDictionary<string,string> questionsAnswers;
	public List<string> answerOptions;

	// Use this for initialization
	void Start () {
		questionsAnswers = new SortedDictionary<string, string> ();
		delim = ':';
		delim2 = '.';
	//	answerOptions = new List<string>[NumOptions];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool isInRevInd(int check){
		foreach (int i in BookScript.bookControl.reviewIndices) {
			if(i == check) { 
				return true;
			}
		}
		return false;
	}

	public void setWordOptions(){
		for (int i = 0; i < BookScript.bookControl.words.Length; i++) {
			if (isInRevInd (i)) {
				continue; // go to next iteration
			}
			parseStr (BookScript.bookControl.words [i]);
			answerOptions.Add (defTmp);
		}
	}

	public void parseWords(){
		foreach (string str in BookScript.bookControl.words) {
			parseStr (str);
			questionsAnswers [defTmp] = wrdTmp;
		}
	}

	public bool checkAnswer(string playerAnswer){
		if (questionsAnswers [currQuestion].Equals (playerAnswer)) {
			return true;
		}
		return false;
	}

	public void parseStr(string toParse){
		int len = toParse.IndexOf (delim);
		int len2 = toParse.IndexOf (delim2);


		if (len > 0) {
			wrdTmp = toParse.Substring (0, len); // gets word up till the :
			defTmp = toParse.Substring(len+1, (len2-len+1) ); // get def up to .
			print(wrdTmp);
			print (defTmp);
		}

	}
}
