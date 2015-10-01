using UnityEngine;
using System.Collections;
using Leap;

public class RotateController : MonoBehaviour {

	public const float TRIGGER_DISTANCE_RATIO = 0.7f;
	
	/** The stiffness of the spring force used to move the object toward the hand. */
	public float forceSpringConstant = 100.0f;
	/** The maximum range at which an object can be picked up.*/
	public float magnetDistance = 2.0f;
	
	protected bool pinching_;
	protected Collider grabbed_;
	
	void Start() {
		pinching_ = false;
		grabbed_ = null;
		HandModel hand_model = GetComponent<HandModel>();
		SharedVariables.handModel = hand_model;

	}
	
	/** Finds an object to grab and grabs it. */
	void OnPinch(Vector3 pinch_position) {
		pinching_ = true;
		
		// Check if we pinched a movable object and grab the closest one that's not part of the hand.
		Collider[] close_things = Physics.OverlapSphere(pinch_position, magnetDistance);
		Vector3 distance = new Vector3(magnetDistance, 0.0f, 0.0f);
		
		for (int j = 0; j < close_things.Length; ++j) {
			Vector3 new_distance = pinch_position - close_things[j].transform.position;
			if ((close_things[j].name == "Cube" || close_things[j].name == "Sphere" || close_things[j].name == "Line" || close_things[j].name == "Cone") && new_distance.magnitude < distance.magnitude &&
			    !close_things[j].transform.IsChildOf(transform)) {
				grabbed_ = close_things[j];
				distance = new_distance;
			}
		}
	}

	void OnSqueeze(Vector3 pinch_position) {

		// Check if we pinched a movable object and grab the closest one that's not part of the hand.
		Collider[] close_things = Physics.OverlapSphere(pinch_position, magnetDistance);
		Vector3 distance = new Vector3(magnetDistance, 0.0f, 0.0f);
		if (close_things.Length == 0)
			return;

		Collider squeezed = null;
		for (int j = 0; j < close_things.Length; ++j) {
			Vector3 new_distance = pinch_position - close_things[j].transform.position;
			if ((close_things[j].name == "Cube" || close_things[j].name == "Sphere" || close_things[j].name == "Line"  || close_things[j].name == "Cone") && new_distance.magnitude < distance.magnitude &&
			    !close_things[j].transform.IsChildOf(transform)) {
				squeezed = close_things[j];
				distance = new_distance;
			}
		}

		if (squeezed != null) {
			Debug.Log("destroy");
			Object.Destroy (squeezed.gameObject);
		}
	}
	
	/** Clears the pinch state. */
	void OnRelease() {
		grabbed_ = null;
		pinching_ = false;
	}
	
	/**
   * Checks whether the hand is pinching and updates the position of the pinched object.
   */
	void Update() {
		HandModel hand_model = GetComponent<HandModel>();
		Hand leap_hand = hand_model.GetLeapHand();
		//SharedVariables.handModel = hand_model;
		//Hand leap_hand = hand_model.GetLeapHand();

		if (SharedVariables.draw || SharedVariables.menuOn)
			return;

		bool trigger_pinch = false;

		if (leap_hand == null)
			return;
		
		// Scale trigger distance by thumb proximal bone length.
		Vector leap_thumb_tip = leap_hand.Fingers[0].TipPosition;
		float proximal_length = leap_hand.Fingers[0].Bone(Bone.BoneType.TYPE_PROXIMAL).Length;
		float trigger_distance = proximal_length * TRIGGER_DISTANCE_RATIO;
		
		// Check thumb tip distance to joints on all other fingers.
		// If it's close enough, start pinching.
		int numberOfCloseFingers = 0;
		for (int i = 1; i < HandModel.NUM_FINGERS; ++i) {
		//thumb tip to index
			Finger finger = leap_hand.Fingers[i];
			trigger_pinch = false;
			for (int j = 0; j < FingerModel.NUM_BONES; ++j) {
		//for (int j = FingerModel.NUM_BONES-1; j < FingerModel.NUM_BONES; j++) {

				Vector leap_joint_position = finger.Bone((Bone.BoneType)j).NextJoint;
				if (leap_joint_position.DistanceTo(leap_thumb_tip) < trigger_distance) {
					trigger_pinch = true;
				}
			}
			if(trigger_pinch){
				numberOfCloseFingers++;
			}
		}
		Vector3 pinch_position = hand_model.fingers[0].GetTipPosition();

		if (numberOfCloseFingers > 2) {
			//delete object
			OnSqueeze(pinch_position);
			trigger_pinch = false;
		}
		if (numberOfCloseFingers == 1) {
			trigger_pinch = true;
		}

		// Only change state if it's different.
		if (trigger_pinch && !pinching_)
			OnPinch(pinch_position);
		else if (!trigger_pinch && pinching_)
			OnRelease();
		
		// Accelerate what we are grabbing toward the pinch.
		if (grabbed_ != null) {
			//Vector3 distance = pinch_position - grabbed_.transform.position;
			grabbed_.transform.position = pinch_position;
			
			//grabbed_.GetComponent<Rigidbody>().AddForce(forceSpringConstant * distance);
		}
	}
}