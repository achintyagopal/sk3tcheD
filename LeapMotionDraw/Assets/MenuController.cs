using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject menu;

	// Use this for initialization
	void Start () {
		if (SharedVariables.menuOn) {
			menu.SetActive(true);
		} else {
			menu.SetActive(false);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SharedVariables.menuOn) {
			menu.SetActive(true);
		} else {
			menu.SetActive(false);	
		}
	}
}
