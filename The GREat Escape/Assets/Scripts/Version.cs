using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Version : MonoBehaviour {
	public int version;

	// Use this for initialization
	void Start () {
		version = -1;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int getVersion() {
		version = Random.Range(0,2);
		return version;
	}
}
