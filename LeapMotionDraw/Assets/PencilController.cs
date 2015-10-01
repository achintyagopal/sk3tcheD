using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class PencilController : MonoBehaviour {

	private GameObject meshToCombine;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		meshToCombine = GameObject.Find ("Pencil");
		if (meshToCombine != null) {
			MeshFilter[] meshFilters = meshToCombine.GetComponentsInChildren<MeshFilter> ();
			Debug.Log (meshFilters.Length);
			
			CombineInstance[] combine = new CombineInstance[meshFilters.Length];
			
			List<Material> mats = new List<Material> ();

			int i = 0;
			while (i < meshFilters.Length) {
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
				//meshFilters[i].gameObject.active = false;
				Material mat = meshFilters[i].gameObject.GetComponent<MeshRenderer> ().sharedMaterial;	
				if (!(mats.Contains(mat))){
					mats.Add(mat);
				}
				i++;
			}

			SharedVariables.numberOfPencils++;

			GameObject combinedObject = new GameObject("CombinedPencil");
			Debug.Log(combinedObject.GetInstanceID());
			SharedVariables.pencilIds.Add(combinedObject.GetInstanceID());

			Mesh combinedMesh = new Mesh();
			combinedMesh.CombineMeshes(combine);
			//AssetDatabase.CreateAsset(combinedMesh, "Assets/Combined.asset");
			
			MeshFilter filter = combinedObject.AddComponent<MeshFilter>();
			filter.mesh = combinedMesh;//AssetDatabase.LoadAssetAtPath<Mesh> ("Assets/Combined.asset");
			MeshRenderer renderer = combinedObject.AddComponent<MeshRenderer>();
			renderer.sharedMaterials = mats.ToArray();
			
			//Object prefab = PrefabUtility.CreateEmptyPrefab( "Assets/" + "CombinedObject" + ".prefab");
			//PrefabUtility.ReplacePrefab(combinedObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
			
			foreach(Transform child in meshToCombine.transform) {
				Object.Destroy(child.gameObject);
			}

			Material[] material = combinedObject.GetComponent<Renderer>().materials;
			Debug.Log(material.Length);
			combinedObject.GetComponent<Renderer>().materials = new Material[]{material[0]};
			//Materials[] materials = render.materials;
			//Debug.Log(materials.Length);
			//this.transform.rotation = Quaternion.AngleAxis(5.0f, new Vector3(0,0,1));
			//renderer.material.color = new Color(0f, 1.0f, 0f);
			//Object.Destroy(sphere);
			//Object.Destroy(sphere2);
			//combinedObject.

			Object.Destroy(meshToCombine);
			meshToCombine = null;
			
		}
	}
}
