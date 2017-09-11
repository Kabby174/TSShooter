using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	private Ray ray;
	public GameObject selectedObject;
	//Scripts
	private GameCamera cameraScript;
	private PlayerController mainPlayer;
	void Start(){
		Camera cam = Camera.main;
		cameraScript = cam.GetComponent<GameCamera> ();
		GameObject playerObject = GameObject.FindGameObjectWithTag("MainPlayer");
		mainPlayer = playerObject.GetComponent<PlayerController>();
	}
	// Update is called once per frame
	void Update () {
		// ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		// RaycastHit hitInfo;

		// if (Physics.Raycast (ray, out hitInfo)) {
		// 	GameObject hitObject = hitInfo.transform.root.gameObject;
		// 	SelectObject (hitObject);
		// }

        if(Input.GetMouseButtonDown(1)){
			GameObject selectedItem = getMouseTarget();
			if(selectedItem){
				switch(selectedItem.tag){
					case "ResourceSolid":
						mainPlayer.startMining(selectedItem.
						GetComponent<Resources>());
						break;
				}
			}
        }
		/* 
		//Just testing
		if (Input.GetButtonDown ("Next Unit")) {
			GameObject unSelectedUnit = GameObject.FindGameObjectWithTag("Player");
			GameObject lastSelectedUnit = GameObject.FindGameObjectWithTag("MainPlayer");

			unSelectedUnit.gameObject.tag = "MainPlayer";
			lastSelectedUnit.gameObject.tag = "Player";
			if (cameraScript) {
				cameraScript.SetTarget (unSelectedUnit);
			}
		}
		///* */
	}
	public GameObject getMouseTarget(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo)) {
			GameObject hitObject = hitInfo.transform.root.gameObject;
			return hitObject;
		} else{
			return null;
		}
	}
	void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if(obj == selectedObject)
				return;

			//ClearSelection();
		}

		selectedObject = obj;
		/*
		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs) {
			Material m = r.material;
			m.color = Color.green;
			r.material = m;
		}
		//*/
	}
	void ClearSelection() {
		if(selectedObject == null)
			return;

		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs) {
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}


		selectedObject = null;
	}
}
