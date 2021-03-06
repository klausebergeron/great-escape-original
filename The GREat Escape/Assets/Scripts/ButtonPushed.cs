using UnityEngine;
using System.Collections;

public class ButtonPushed : MonoBehaviour {

	public string Name;
	public int chosen;
	public int correct_answer;
	public HealthBar Health;
	public BossHealthBar bossHealth;
	public PlayerController player;
	public GameButtons clear;
	public FeedbackPanel fbPanel;
	public BossQuestions bossQ;

	float timer = 0;

	public string feedback;

	//for feedback
	public string[] correctFB;
	public string[] wrongFB;

	// Use this for initialization
	void Start () {
		feedback = "";
		bossQ = FindObjectOfType<BossQuestions> ();
		fbPanel = FindObjectOfType<FeedbackPanel> ();
		Health = FindObjectOfType<HealthBar> ();
		bossHealth = FindObjectOfType<BossHealthBar> ();
		player = FindObjectOfType<PlayerController> ();
		clear = FindObjectOfType<GameButtons> ();

		correctFB = new string[] {
			"Way to go!",
			"You rock at this!",
			"Lookin’ good!",
			"Wow! I’m impressed!",
			"Keep at it!",
			"Good going!",
			"You were born a winner!",
			"Victory is yours!",
			"That was awesome!",
			"Great Job!",
			"Knew you could do it",
			"Great Job!",
			"You’re so good at this!",
			"That answer was perfect!"
		};

		wrongFB = new string[] {
			"That was terrible",
			"Better luck next time!", 
			"You really went with that answer?", 
			"FAIL"
		};

	}

		
	public void Pushed(){
		correct_answer = int.Parse(bossQ.getAnswer());
		print ("correct answer inside buttonpushed is");
		print(correct_answer);
		Name = gameObject.name;
		print ("Name is");
		print (Name);
		chosen = int.Parse (Name);


		/*
		if player chooses correct answer, 
		boss loses health and the question just answered is added to used questions arr
		*/
		if (chosen == (int)correct_answer) {
			print("chose correct answer");
			feedback = getCorrectFeedback();
			print("feedback received: " + feedback);
			player.rightSound.Play ();
			bossHealth.changeBar (10);
			//BossQuestions.questionsUsed.Add (StompEnemy.ques);
			fbPanel.enableFBPanel(feedback, true); //enable feedback panel
			clear.ClearQuestionDisplay ();
			//Pause(10);
			//yield return new WaitForSeconds(10);
			Invoke("closePanel", 1);

	
		} 
		if (chosen != (int)correct_answer)
		{	
			print("chose wrong answer");
			feedback = getWrongFeedback();
			print("feedback received: " + getWrongFeedback());
			//Health.changeBar (10);	
			player.wrongSound.Play ();
			fbPanel.enableFBPanel(feedback, false); //enable feedback panel
			//yield return new WaitForSeconds(0);
		}
		//clear.ClearQuestionDisplay ();
	}

	private void closePanel()
	{
		fbPanel.disableFBPanel ();
	}



/*	private IEnumerator Pause(int p) {
         Time.timeScale = 0.1f;
         float pauseEndTime = Time.realtimeSinceStartup + 1;
         while (Time.realtimeSinceStartup < pauseEndTime) {
              yield return 0;
         }
         Time.timeScale = 1;
    }*/

	private IEnumerator Pause(int p)
	{
		print ("In pause");
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(p);
		Time.timeScale = 1;
		print ("End pause");
	}




	/*
	returns random feedback phrase when player answers correctly
	is called by Button Pushed script
	*/
	public string getCorrectFeedback(){
		int size = correctFB.Length;
		int pos = Random.Range(0,size-1);
		return correctFB[pos];
	}



	/*
	returns random feedback phrase when player answers correctly
	called by Button Pushed script
	*/
	public string getWrongFeedback(){
		int size = wrongFB.Length;
		int pos = Random.Range(0,size-1);
		return wrongFB[pos];
	}


		
}
