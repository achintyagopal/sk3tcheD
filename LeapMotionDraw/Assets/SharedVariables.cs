using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Leap;

public class SharedVariables {

	// Use this for initialization
	public static HandModel handModel;
	public static bool draw = false;
	public static bool pencilDraw = false;
	public static Mesh mesh;
	public static bool menuOn = false;
	public static int numberOfPencils = 0;
	//public static int[] pencilIds = new int[];
	public static List<int> pencilIds = new List<int>();

	public static GameObject coneModel;

	
	public static float red = 125f;
	public static float green = 125f;
	public static float blue = 125f;
	public static float thickness = 20f;


}
