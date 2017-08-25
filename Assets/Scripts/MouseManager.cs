using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	private Ray ray;
	public GameObject selectedObject;
	//Scripts
	private GameCamera cameraScript;

	void Start(){
		Camera cam = Camera.main;
		cameraScript = cam.GetComponent<GameCamera> ();
	}
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo)) {
			GameObject hitObject = hitInfo.transform.root.gameObject;
			SelectObject (hitObject);
		}

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
