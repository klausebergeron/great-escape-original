using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackPanel : MonoBehaviour {
	public Text fbDisplay;
	public GameObject button;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().enabled = false;
		button.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		print("in update in Feedback Panel");
		//if (!string.IsNullOrEmpty (WordDisplay.text.ToString ().Trim ())) {
			this.GetComponent<SpriteRenderer>().enabled = true;
			button.SetActive(true);
		//} else {
		//	this.GetComponent<SpriteRenderer>().enabled = false;
		//	button.SetActive(false);
		//}
		
	}
}
