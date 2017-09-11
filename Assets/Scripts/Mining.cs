using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour {
	public float progress = 0;

	public Dictionary<Resources.ResourceType, int> mine(Resources resource, int quantity){
		if(resource){
            return resource.gather(quantity);
        }else{
            return null;
        }
	}
}