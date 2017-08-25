using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	
	//Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;
	public Gun gun;
	public bool mainPlayer = false;

	//System 
	private Quaternion targetRotation;

	//Components
	private CharacterController controller;
	private Camera cam;

	//Scripts
	private GameCamera cameraScript;

	void Start () {
		controller = GetComponent<CharacterController>();
		cam = Camera.main;

		cameraScript = cam.GetComponent<GameCamera>();
	}
	void Update () {
		if(gameObject.tag == "MainPlayer"){
			ControlMouse();

			if(Input.GetButtonDown("Shoot")){
				gun.Shoot();
			}else if(Input.GetButton("Shoot")){
				gun.ShootContinuous();
			}
			// ControlWASD();
		}
	}
	void OnMouseDown() {
		if (gameObject.tag != "MainPlayer") {
			GameObject lastSelectedUnit = GameObject.FindGameObjectWithTag("MainPlayer");
			lastSelectedUnit.gameObject.tag = "Player";

			gameObject.tag = "MainPlayer";
			if (cameraScript) {
				cameraScript.SetTarget (gameObject);
			}
		}
	}
	void ControlMouse(){
		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint( new Vector3(
			mousePos.x, 
			mousePos.y, 
			cam.transform.position.y - transform.position.y
		));
		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * 
			Mathf.MoveTowardsAngle(
				transform.eulerAngles.y, 
				targetRotation.eulerAngles.y,
				rotationSpeed * Time.deltaTime
			);

		Vector3 input = new Vector3(
			Input.GetAxisRaw("Horizontal"),
			0,
			Input.GetAxisRaw("Vertical")
		);

		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
		motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
		
		//Gravity system
		motion += Vector3.up * -8;

		controller.Move(motion * Time.deltaTime);
	}
	void ControlWASD(){
		Vector3 input = new Vector3(
			Input.GetAxisRaw("Horizontal"),
			0,
			Input.GetAxisRaw("Vertical")
		);

		if(input != Vector3.zero){
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * 
				Mathf.MoveTowardsAngle(
					transform.eulerAngles.y, 
					targetRotation.eulerAngles.y,
					rotationSpeed * Time.deltaTime
				);
		}

		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
		motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
		
		//Gravity system
		motion += Vector3.up * -8;

		controller.Move(motion * Time.deltaTime);
	}
}
