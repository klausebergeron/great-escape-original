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
			"Keep going!",
			"Keep trying!", 
			"You can do it... Try again!", 
			"You WILL succeed!", 
			"You are so close to success!"
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
			fbPanel.enableFBPanel(feedback); //enable feedback panel
			clear.ClearQuestionDisplay ();
			Pause(10);
			fbPanel.disableFBPanel ();
			
	
		} 
		if (chosen != (int)correct_answer)
		{	
			print("chose wrong answer");
			feedback = getWrongFeedback();
			print("feedback received: " + getWrongFeedback());
			//Health.changeBar (10);	
			player.wrongSound.Play ();
			fbPanel.enableFBPanel(feedback); //enable feedback panel
		}
		//clear.ClearQuestionDisplay ();
	}


	private IEnumerator Pause(int p) {
         Time.timeScale = 0.1f;
         float pauseEndTime = Time.realtimeSinceStartup + 1;
         while (Time.realtimeSinceStartup < pauseEndTime) {
              yield return 0;
         }
         Time.timeScale = 1;
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
