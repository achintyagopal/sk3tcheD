using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LMWidgets;

public class SliderDataBinder : DataBinderSlider {
	[SerializeField] 
	
	public float max = 255f;
	public float min;
	public float outputValue;
	public int slider = -1;
	
	override protected void Awake(){
		Mathf.Clamp (outputValue, min, max);
		base.Awake();
		if (slider == 0) {
			outputValue = SharedVariables.red;
		} else if (slider == 1) {
			outputValue = SharedVariables.green;
		} else if (slider == 2) {
			outputValue = SharedVariables.blue;
		} else if (slider == 3) {
			outputValue = SharedVariables.thickness;
		}
	}
	
	override protected void setDataModel(float value) {
		outputValue = value * (max - min) + min;
	}
	
	override public float GetCurrentData() {
		if (slider == 0) {
			SharedVariables.red = outputValue;
		} else if (slider == 1) {
			SharedVariables.green = outputValue;
		} else if (slider == 2) {
			SharedVariables.blue = outputValue;
		} else if (slider == 3) {
			SharedVariables.thickness = outputValue;
		}
		return (outputValue - min)/(max - min);
	}
}