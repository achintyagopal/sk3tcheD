using UnityEngine;
using System.Collections;

public class ConeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
