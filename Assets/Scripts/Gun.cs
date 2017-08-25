using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {

	public float rpm;
	public enum GunType { Semi, Burst, Auto };
	public GunType gunType;

	//Components
	public Transform spawn;
	private LineRenderer tracer;

	//System
	private float secondsBetweenShots;
	private float nextPossibleShootTime;
	private AudioSource audio;

	void Start(){
		secondsBetweenShots = 60/rpm;
		audio = GetComponent<AudioSource>();

		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer> ();
		}
	}
	public void Shoot() {
		if(CanShoot()){
			Ray ray = new Ray(spawn.position, spawn.forward);
			RaycastHit hit;

			float shotDistance = 20;
			if(Physics.Raycast(ray, out hit, shotDistance)){
				shotDistance = hit.distance;
			}

			nextPossibleShootTime = Time.time + secondsBetweenShots;
			audio.Play();

			if (tracer) {
				StartCoroutine ("RenderTracer", ray.direction * shotDistance);

			}
			//Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
		}
	}

	public void ShootContinuous() {
		switch(gunType){
			case GunType.Auto:
				Shoot();
				break;
		}
	}

	private bool CanShoot(){
		bool canShoot = true;

		if(Time.time < nextPossibleShootTime){
			canShoot = false;
		}
		return canShoot;
	}
	IEnumerator RenderTracer(Vector3 hitPoint){
		tracer.enabled = true;
		tracer.SetPosition (0,spawn.position);
		tracer.SetPosition (1,spawn.position + hitPoint);
		yield return null;
		tracer.enabled = false;
	}
}
