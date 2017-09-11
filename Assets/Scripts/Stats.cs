using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
	public float progress = 0;

	public void addProgress(float increase){
		progress += increase;
		progress = Mathf.Min(progress, 1);
	}
	public float getProgress(){
		return progress;
	}
	public void resetProgress(){
		progress = 0;
	}
}
