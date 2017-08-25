using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public Animator animator;
	bool doorOpen;

	void Start(){
		doorOpen = false;
		animator = GetComponent<Animator>();
	}
	void OnTriggerEnter(Collider collider){
		switch (collider.gameObject.tag) {
		case "MainPlayer":
		case "Player":
			SetDoorsOpen (true);
			break;
		}
	}
	void OnTriggerExit(Collider collider){
		if (doorOpen) {
			SetDoorsOpen (false);
		}
	}

	void SetDoorsOpen(bool isOpen){
		doorOpen = isOpen;
		animator.SetTrigger (isOpen ? "Open" : "Close");
	}
}
