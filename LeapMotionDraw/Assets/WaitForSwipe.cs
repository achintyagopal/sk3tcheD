using UnityEngine;
//using UnityEditor;
using System.Collections;
using Leap;
// an Editor method to create a cone primitive (so far no end caps)
// the top center is placed at (0/0/0)
// the bottom center is placed at (0/0/length)
// if either one of the radii is 0, the result will be a cone, otherwise a truncated cone
// note you will get inevitable breaks in the smooth shading at cone tips
// note the resulting mesh will be created as an asset in Assets/Editor
// Author: Wolfram Kresse
public class WaitForSwipe : MonoBehaviour {


	public HandController handController;

	// Use this for initialization
	void Start () {
		Controller controller = handController.GetLeapController ();
		controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);
		controller.Config.SetFloat("Gesture.Swipe.MinLength", 200.0f);
		controller.Config.SetFloat("Gesture.Swipe.MinVelocity", 750f);
		controller.Config.Save();
	}
	
	void Update(){
		Controller controller = handController.GetLeapController ();
		GestureList gestureList = controller.Frame ().Gestures ();
		if(gestureList.Count > 0){
			for(int i = 0; i < gestureList.Count; i++){
				Gesture gesture = gestureList[i];
				if (gesture.Type == Gesture.GestureType.TYPESWIPE){// Gesture.GestureType.TYPESWIPE) {
					Debug.Log("Swipe");
					SwipeGesture swipe = new SwipeGesture(gesture);
					bool isHorizontal = Mathf.Abs(swipe.Direction.x) > Mathf.Abs(swipe.Direction.y);
					//swipe.Position
					//start Position choose object
					///average swipe.Position and startPosition, closest object
					
					if(isHorizontal){
						Debug.Log("horizontal");
						Application.LoadLevel("StartScene");
					}
					else{
						if(swipe.Direction.y > 0){
							//up
						}
						else{
							//down
						}
					}
				}
			}
		}
	}

}