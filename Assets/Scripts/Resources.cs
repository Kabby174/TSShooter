using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {
    public enum ResourceType { Iron, Stone, Copper, Wood };
    public ResourceType type;
	public int quantity;
    
    public Resources(ResourceType resourceType, int resourceQuantity){
      type = resourceType;
      quantity = resourceQuantity;
   }

    public Dictionary<Resources.ResourceType, int> gather(int num){
        int gathered = quantity > num ? num : quantity;
        quantity = Mathf.Max(quantity - num, 0);

        if(quantity == 0){
            Destroy(gameObject.transform.root.gameObject);
        }

        if(gathered > 0){
            Dictionary<Resources.ResourceType, int> newResource =
            new Dictionary<Resources.ResourceType, int>();
            newResource.Add(type, gathered);
            
            return newResource;
        }else{
            return null;
        }
    }
}