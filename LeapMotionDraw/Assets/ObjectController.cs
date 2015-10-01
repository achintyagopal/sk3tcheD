using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Leap;
//using UnityEditor;

public class ObjectController : MonoBehaviour {

	private int type;
	private bool draw;
	private GameObject drawObject = null;
	private Vector3 startPoint;
	private bool pencilDraw;
	public Material material;

	// Use this for initialization
	void Start () {
		draw = false;
		type = -1;
		pencilDraw = false;
	}
	
	// Update is called once per frame
	void Update () {
		//get color (def: red)
		//get thickness (def: 1)
		if (SharedVariables.menuOn)
			return;

		SharedVariables.draw = draw;
		SharedVariables.pencilDraw = pencilDraw;
		if (Input.GetKey (KeyCode.Alpha1)) {
			//pencil
			if(draw){
				if(type == 1){
					GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
					cylinder.transform.localScale = new Vector3(SharedVariables.thickness/100.0f, 0.001f, SharedVariables.thickness/100.0f);
					cylinder.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
					cylinder.transform.parent = drawObject.transform;
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;

					Vector3 nextPoint = hand_model.fingers[1].GetTipPosition();
					float distance = Vector3.Distance(startPoint,nextPoint) + 2*SharedVariables.thickness/20.0f;
					cylinder.transform.localScale  = new Vector3(distance, 0.001f, SharedVariables.thickness/20.0f);
					cylinder.transform.rotation = Quaternion.Euler(new Vector3(90,90+(180.0f/Mathf.PI)* (nextPoint.z-startPoint.z)/distance,90+(180.0f/Mathf.PI)* Mathf.Asin((nextPoint.y-startPoint.y)/distance)));
					MeshRenderer gameObjectRenderer = cylinder.GetComponent<MeshRenderer>();
					gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

					startPoint = nextPoint;
					cylinder.transform.position = startPoint + (nextPoint - startPoint)/2;
				}
				else{
					if(type == 2){
						drawObject.name = "Line";
					}
					else if(type == 3){
						drawObject.name = "Cube";
					}
					else if(type == 4){
						drawObject.name = "Sphere";
					} else if(type == 5){
						drawObject.transform.name = "Cone";
					}

					//create stream of cylinders: scale (thickness/100.0f, 0.01, thickness/100.0f)
					//cylinders: rotation (90,0,0)
					GameObject empty = new GameObject();
					GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
					cylinder.transform.localScale = new Vector3(SharedVariables.thickness/20.0f, 0.001f, SharedVariables.thickness/20.0f);
					cylinder.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
					cylinder.transform.parent = empty.transform;
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;


					//MeshRenderer gameObjectRenderer = cylinder.GetComponent<MeshRenderer>();
					//gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);
					startPoint = hand_model.fingers[1].GetTipPosition();
					cylinder.transform.position = startPoint;
					
					drawObject = empty;
				}
			}
			else{
				draw = true;
				pencilDraw = true;
				type = 1;
				GameObject empty = new GameObject();
				GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				cylinder.transform.localScale = new Vector3(SharedVariables.thickness/20.0f, 0.001f, SharedVariables.thickness/20.0f);
				cylinder.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));

				cylinder.transform.parent = empty.transform;
				empty.transform.name = "empty";
				HandModel hand_model = SharedVariables.handModel;
				Hand leap_hand = hand_model.GetLeapHand();

				if (leap_hand == null)
					return;

				MeshRenderer gameObjectRenderer = cylinder.GetComponent<MeshRenderer>();
				gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);
				
				startPoint = hand_model.fingers[1].GetTipPosition();
				cylinder.transform.position = startPoint;

				drawObject = empty;
			}
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			//line
			if(draw){
				if(type == 2){
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;
					
					Vector3 newPoint = hand_model.fingers[1].GetTipPosition();
					drawObject.transform.position = startPoint + (newPoint - startPoint)/2;

					float distance =Vector3.Distance(newPoint,startPoint);
					Debug.Log("distance: " + distance);
					Debug.Log("thickness: " + SharedVariables.thickness);
					if(distance != 0f){
						drawObject.transform.rotation = Quaternion.Euler( (180.0f/Mathf.PI)* new Vector3(-1*Mathf.Asin((newPoint.z-startPoint.z)/distance),0f,Mathf.Asin((newPoint.x-startPoint.x)/distance)));
					}
					drawObject.transform.localScale = new Vector3(SharedVariables.thickness/50.0f, distance/2, SharedVariables.thickness/50.0f);
				}
				else {
					if(type == 1){
						pencilDraw = false;
						SharedVariables.pencilDraw = false;
					} else if(type == 3){
						drawObject.name = "Cube";
					}
					else if(type == 4){
						drawObject.name = "Sphere";
					} else if(type == 5){
						drawObject.transform.name = "Cone";
					}
					type = 2;

					GameObject line = GameObject.CreatePrimitive(PrimitiveType.Capsule);
					line.transform.name = "name";
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;
					
					startPoint = hand_model.fingers[1].GetTipPosition();
					line.transform.position = startPoint;

					MeshRenderer gameObjectRenderer = line.GetComponent<MeshRenderer>();
					gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

					float distance =Vector3.Distance(startPoint,startPoint);
					line.transform.localScale = new Vector3(SharedVariables.thickness/50.0f, distance, SharedVariables.thickness/50.0f);
					drawObject = line;
				}
			}
			else{
				draw = true;
				type = 2;

				GameObject line = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				line.transform.name = "name";
				
				HandModel hand_model = SharedVariables.handModel;
				Hand leap_hand = hand_model.GetLeapHand();
				
				if (leap_hand == null)
					return;
				
				startPoint = hand_model.fingers[1].GetTipPosition();
				line.transform.position = startPoint;

				MeshRenderer gameObjectRenderer = line.GetComponent<MeshRenderer>();
				gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

				float distance =Vector3.Distance(startPoint,startPoint);
				line.transform.localScale = new Vector3(SharedVariables.thickness/50.0f, distance, SharedVariables.thickness/50.0f);
				drawObject = line;
			}

		} else if (Input.GetKey (KeyCode.Alpha3)) {
			//cube
			Debug.Log("3");
			if (draw) {
				//middle of drawing
				if (type == 3) {
					Debug.Log ("middle");

					//HandModel hand_model = SharedVariables.handModel;
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					if (leap_hand == null)
						return;

					Vector3 newPoint = SharedVariables.handModel.fingers[1].GetTipPosition();
					Debug.Log(newPoint);
					drawObject.transform.localScale = 2*(newPoint - startPoint);

				} else {
					if(type == 1){
						pencilDraw = false;
						SharedVariables.pencilDraw = false;
					} else if (type == 2){
						drawObject.name = "Line";
					}
					else if(type == 4){
						drawObject.name = "Sphere";
					} else if(type == 5){
						drawObject.transform.name = "Cone";
					}
					type = 3;

					GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
					cube.transform.name = "name";

					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();

					if (leap_hand == null)
						return;
					
					startPoint = SharedVariables.handModel.fingers[1].GetTipPosition();

					MeshRenderer gameObjectRenderer = cube.GetComponent<MeshRenderer>();
					gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);
					Debug.Log(startPoint);

					cube.transform.localScale = startPoint - startPoint;
					drawObject = cube;
				}
			} else {
				draw = true;
				type = 3;

				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.name = "name";
				drawObject = cube;

				HandModel hand_model = SharedVariables.handModel;
				Hand leap_hand = hand_model.GetLeapHand();
				Debug.Log("cube");

				if (leap_hand == null) {
					Debug.Log("hand null");
					return;
				}

				MeshRenderer gameObjectRenderer = cube.GetComponent<MeshRenderer>();
				gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

				startPoint = hand_model.fingers[1].GetTipPosition();
				cube.transform.position = startPoint;
				cube.transform.localScale = startPoint - startPoint;
				Debug.Log("startPoint " + startPoint);
			}
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			//sphere
			if (draw) {

				if (type == 4) {
					Debug.Log ("middle");
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null){
						Debug.Log("null");
						return;
					}
					
					Vector3 newPoint = hand_model.fingers[1].GetTipPosition();

					drawObject.transform.localScale = 2*(new Vector3(Vector3.Distance(startPoint,newPoint),Vector3.Distance(startPoint,newPoint),Vector3.Distance(startPoint,newPoint)));
					SphereCollider sphereCollider = drawObject.GetComponent<SphereCollider>();
					sphereCollider.radius = Vector3.Distance(startPoint,newPoint);
					
				} else {
					if(type == 1){
						pencilDraw = false;
						SharedVariables.pencilDraw = false;
					} else if (type == 2){
						drawObject.name = "Line";
					}
					else if(type == 3){
						drawObject.name = "Cube";
					}
					else if(type == 5){
						drawObject.transform.name = "Cone";
					}
					type = 4;

					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
					sphere.transform.name = "name";
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;
					
					startPoint = hand_model.fingers[1].GetTipPosition();
					sphere.transform.position = startPoint;
					
					SphereCollider sphereCollider = sphere.GetComponent<SphereCollider>();
					sphereCollider.radius = Vector3.Distance(startPoint,startPoint);

					MeshRenderer gameObjectRenderer = sphere.GetComponent<MeshRenderer>();
					gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

					drawObject = sphere;
				}
			} else {
				draw = true;
				type = 4;
				GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				sphere.transform.name = "name";

				HandModel hand_model = SharedVariables.handModel;
				Hand leap_hand = hand_model.GetLeapHand();
				
				if (leap_hand == null)
					return;
				
				startPoint = hand_model.fingers[1].GetTipPosition();
				sphere.transform.position = startPoint;

				SphereCollider sphereCollider = sphere.GetComponent<SphereCollider>();
				sphereCollider.radius = Vector3.Distance(startPoint,startPoint);

				MeshRenderer gameObjectRenderer = sphere.GetComponent<MeshRenderer>();
				gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);

				drawObject = sphere;
			}

		} else if (Input.GetKey (KeyCode.Alpha5)) {
			//sphere
			if (draw) {
				
				if (type == 5) {
					Debug.Log ("middle");
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null){
						Debug.Log("null");
						return;
					}
					
					Vector3 newPoint = hand_model.fingers[1].GetTipPosition();
					float distance = Vector2.Distance(new Vector2(startPoint.x, startPoint.z), new Vector2(newPoint.x, newPoint.z));
					drawObject.transform.localScale = (new Vector3(distance,distance, newPoint.y - startPoint.y));

					
				} else {
					if(type == 1){
						pencilDraw = false;
						SharedVariables.pencilDraw = false;
					} else if (type == 2){
						drawObject.name = "Line";
					}
					else if(type == 3){
						drawObject.name = "Cube";
					}
					else if(type == 4){
						drawObject.name = "Sphere";
					}
					type = 5;
					
					HandModel hand_model = SharedVariables.handModel;
					Hand leap_hand = hand_model.GetLeapHand();
					
					if (leap_hand == null)
						return;
					
					
					startPoint = hand_model.fingers[1].GetTipPosition();
					
					GameObject cone = (GameObject) Instantiate(SharedVariables.coneModel, startPoint, Quaternion.Euler(new Vector3(90,0,180)));
					cone.transform.name = "name";
					cone.transform.localScale = new Vector3(0,0,0);

					MeshCollider meshCollider = cone.AddComponent<MeshCollider>();
					meshCollider.sharedMesh = cone.GetComponent<MeshFilter>().mesh;

					MeshRenderer gameObjectRenderer = cone.GetComponent<MeshRenderer>();
					gameObjectRenderer.material = material;
					gameObjectRenderer.material.color = new Color(SharedVariables.red/255, SharedVariables.green/255, SharedVariables.blue/255);
					
					drawObject = cone;
				}
			} else {
				draw = true;
				type = 5;

				HandModel hand_model = SharedVariables.handModel;
				Hand leap_hand = hand_model.GetLeapHand();
				
				if (leap_hand == null)
					return;


				startPoint = hand_model.fingers[1].GetTipPosition();

				GameObject cone = (GameObject) Instantiate(SharedVariables.coneModel, startPoint, Quaternion.Euler(new Vector3(90,0,180)));
				cone.transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
				cone.transform.name = "name";
				cone.transform.localScale = new Vector3(0,0,0);

				MeshCollider meshCollider = cone.AddComponent<MeshCollider>();
				meshCollider.sharedMesh = cone.GetComponent<MeshFilter>().mesh;

				MeshRenderer gameObjectRenderer = cone.GetComponent<MeshRenderer>();
				gameObjectRenderer.material = material;
				gameObjectRenderer.material.color = new Color(SharedVariables.red/255f, SharedVariables.green/255f, SharedVariables.blue/255f);
				
				drawObject = cone;
			}
			
		} else {
			if (draw) {
				draw = false;
				if(drawObject){
					if(type == 1){
						//combine drawObject
						//delete drawObject
						//call new object "pencil"
						drawObject.transform.name = "Pencil";
					} else if(type == 2){
					drawObject.name = "Line";
					} else if(type == 3){
					drawObject.transform.name = "Cube";
					} else if(type == 4){
					drawObject.transform.name = "Sphere";
					}
					else if(type == 5){
						drawObject.transform.name = "Cone";
					}
				}
				drawObject = null;
				type = -1;
			}
		}
	}
}