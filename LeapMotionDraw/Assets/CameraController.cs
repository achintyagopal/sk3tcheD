using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class CameraController : MonoBehaviour {

	public GameObject handController;
	public GameObject canvas;
	public GameObject button;
	public GameObject coneModel;
	// Use this for initialization
	void Start () {
			//GameObject cone = (GameObject)Instantiate (coneModel, new Vector3 (0, 0, 0), Quaternion.identity);
		SharedVariables.coneModel = coneModel;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && Input.GetKeyUp(KeyCode.Alpha2)) {
			//handController.SetActive(false);
			//canvas.SetActive(false);
			//button.SetActive(false);

			Debug.Log("save");
			string filename = "screenshot" + System.DateTime.Now.ToString();
			filename = filename.Replace("/", "-");	
			filename = filename.Replace(":", "-");

			filename = filename.Replace(" ", "");
			Debug.Log(filename);
			Application.CaptureScreenshot(filename);
			//handController.SetActive(true);
			//canvas.SetActive(true);
			//button.SetActive(true);
		}
		else if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && Input.GetKeyUp(KeyCode.Alpha3)) {
			//handController.SetActive(false);
			//canvas.SetActive(false);
			//button.SetActive(false);

			MeshFilter[] meshTempFilters = UnityEngine.Object.FindObjectsOfType<MeshFilter>();
			MeshFilter[] meshFilters = new MeshFilter[meshTempFilters.Length];
			int j = 0;
			/*CombineInstance[] combine = new CombineInstance[meshFilters.Length];
			
			List<Material> mats = new List<Material> ();
			
			int i = 0;
			*/
			int i = 0;
			while (i < meshTempFilters.Length) {
				if(meshTempFilters[i].gameObject.name == "CombinedPencil" ||  meshTempFilters[i].gameObject.name == "Sphere" 
				   || meshTempFilters[i].gameObject.name == "Cube" || meshTempFilters[i].gameObject.name == "Line" 
				   || meshTempFilters[i].gameObject.name == "Cone"){
					meshFilters[j]= meshTempFilters[i];
					j++;
				}
				i++;
			}
			MeshFilter[] temp = new MeshFilter[j];
			for(i = 0; i < j; i++){
				temp[i] = meshFilters[i];
			}
			meshFilters = temp;
			/*
			Mesh combinedMesh = new Mesh();
			combinedMesh.CombineMeshes(combine);
			string filename = "screenshot3D" + System.DateTime.Now.ToString() + ".obj";
			filename = filename.Replace("/", "-");
			filename = filename.Replace(":", "-");

			filename = filename.Replace(" ", "");

			GameObject combinedObject = new GameObject("CombinedPencil");
			MeshFilter filter = combinedObject.AddComponent<MeshFilter>();
			filter.mesh = combinedMesh;
			MeshRenderer renderer = combinedObject.AddComponent<MeshRenderer>();
			renderer.sharedMaterials = mats.ToArray();*/
			string filename = "screenshot3D" + System.DateTime.Now.ToString() + ".obj";
			filename = filename.Replace("/", "-");
			filename = filename.Replace(":", "-");
			
			filename = filename.Replace(" ", "");
			ObjExporter.MeshToFile(meshFilters, filename);

			//Object.Destroy(combinedObject.gameObject);
			//combine all objects			
		}

		if (Input.GetKeyUp (KeyCode.Z)) {
			foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
			{
				//(gameObj.name == "CombinedPencil") {
					int a = (SharedVariables.pencilIds.ToArray())[SharedVariables.numberOfPencils-1];
					if(gameObj.GetInstanceID() == a){
						SharedVariables.pencilIds.Remove(gameObj.GetInstanceID());
						Object.Destroy(gameObj);
						SharedVariables.numberOfPencils--;
						break;
					}
				}
			//}
		}
		//roate: (0, -1.5, 0);
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z
			//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Time.deltaTime * (new Vector3(1,0,0)));


		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.S)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z


		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.A)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z


		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.D)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z


		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Q)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z

			
		}else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.E)) {
			//vector(0, my position - their position,
			//(transform.position - new Vector3(0,-1.5,0)).y
			//(transform.position - new Vector3(0,-1.5,0)).z

			
		} else if (Input.GetKey (KeyCode.A)) {
			transform.position += new Vector3(-1,0,0)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(1,0,0)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(-1,0,0)*Time.deltaTime;
			canvas.transform.position += new Vector3(-1,0,0)*Time.deltaTime;
			button.transform.position += new Vector3(-1,0,0)*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3(1,0,0)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(-1,0,0)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(1,0,0)*Time.deltaTime;
			button.transform.position += new Vector3(1,0,0)*Time.deltaTime;
			canvas.transform.position += new Vector3(1,0,0)*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.W)) {
			transform.position += new Vector3(0,1,0)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(0,-1,0)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(0,1,0)*Time.deltaTime;
			canvas.transform.position += new Vector3(0,1,0)*Time.deltaTime;
			button.transform.position += new Vector3(0,1,0)*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.S)) {
			transform.position += new Vector3(0,-1,0)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(0,1,0)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(0,-1,0)*Time.deltaTime;
			canvas.transform.position += new Vector3(0,-1,0)*Time.deltaTime;
			button.transform.position += new Vector3(0,-1,0)*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.Q)) {
			transform.position += new Vector3(0,0,-1)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(0,0,1)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(0,0,-1)*Time.deltaTime;
			canvas.transform.position += new Vector3(0,0,-1)*Time.deltaTime;
			button.transform.position += new Vector3(0,0,-1)*Time.deltaTime;
		} else if (Input.GetKey (KeyCode.E)) {
			transform.position += new Vector3(0,0,1)*Time.deltaTime;
			foreach(Transform child in handController.transform){
				child.transform.position += new Vector3(0,0,-1)*Time.deltaTime;
			}
			handController.transform.position += new Vector3(0,0,1)*Time.deltaTime;
			canvas.transform.position += new Vector3(0,0,1)*Time.deltaTime;
			button.transform.position += new Vector3(0,0,1)*Time.deltaTime;
		}

	}
}
