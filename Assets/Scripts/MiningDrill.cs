using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Stats))]
[RequireComponent (typeof (Inventory))]
public class MiningDrill : MonoBehaviour {
	public float miningSpeed = 1;
	public float fuel = 0;
	public float fuelCost = 1;
	public float output = 0;
	public Image progressImage;
	private Stats stats;
	private float miningUnits = 60;
	void Start () {
		stats = GetComponent<Stats>();
		updateProgress();
	}
	
	// Update is called once per frame
	void Update () {
		if(fuel > 0){
			work();
		}
	}
	void work(){
		stats.addProgress( (miningSpeed * 60) / 100 * Time.deltaTime );

		if(stats.getProgress() == 1){
			fuel -= fuelCost;
			output++;
			stats.resetProgress();
		}
		updateProgress();
	}
	void updateProgress(){
		progressImage.fillAmount = stats.progress;
	}
}
