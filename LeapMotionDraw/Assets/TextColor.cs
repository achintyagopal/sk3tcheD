using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextColor : MonoBehaviour {

	public Text theText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		theText.color = new Color (SharedVariables.red/255f, SharedVariables.green/255f, SharedVariables.blue/255f, 1.0f);
	}
}
