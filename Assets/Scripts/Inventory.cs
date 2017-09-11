using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour {
    public Dictionary<Resources.ResourceType, int> content;
    public int capacity = 1;
    public List<GameObject> items;
	public GameObject InventoryPanel;
	public GameObject debugPanel;
    public GameObject InventorySlot;
    void Start(){
        content = new Dictionary<Resources.ResourceType, int>();

        //If we have a debug panel
		if(debugPanel){
			Destroy(debugPanel);
        }
        if(InventoryPanel){
            //Remove all child objects
            foreach (Transform child in InventoryPanel.transform) {
                Destroy(child.gameObject);
            }
        }
        //Hide panel if the inventory is empty
        checkPanelEmpty();
    }
    public void Update(){
        if(InventoryPanel){
            InventoryPanel.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
	public void add(Dictionary<Resources.ResourceType, int> resourceDictionary){
		if (resourceDictionary.Keys.Count > 0) {
            foreach(KeyValuePair<Resources.ResourceType, int> resource in resourceDictionary){
                int value = 0;
                if(content.ContainsKey(resource.Key)){
                    value += content[resource.Key];
                    content.Remove(resource.Key);
                }else{
                   addItemSlot(resource.Key.ToString());
                }
                value += resource.Value;
                content.Add(resource.Key, value);

                Debug.Log("Player Contains "+value+" "+resource.Key+"(s)");
            }
            checkPanelEmpty();
        }
	}
    void checkPanelEmpty(){
        if(InventoryPanel){
            InventoryPanel.SetActive(content.Count > 0);
        }
    }
    void addItemSlot(string itemName){
        if(InventoryPanel){
            GameObject newSlot = Instantiate(
                InventorySlot
            ) as GameObject;
            GameObject itemSlot = Instantiate(
                items.Find(obj => obj.name == itemName+"Slot")
            ) as GameObject;
            if(newSlot && itemSlot){
                ItemSlot newItemSlot = new ItemSlot(newSlot, itemSlot);
                newItemSlot.SetParent(InventoryPanel.transform);
            }
        }
    }
    public void remove(Resources resource){

    }
}
class ItemSlot {
    GameObject inventorySlot;
    GameObject itemIcon;
    public ItemSlot(GameObject newSlot, GameObject itemSlot){
        inventorySlot = newSlot;
        itemIcon = itemSlot;
        alignIcon();
    }
    void alignIcon(){
        // inventorySlot.transform.SetParent(transform);
        inventorySlot.transform.Rotate(90,0,0);
        inventorySlot.transform.Translate(0,0,-13.34f);

        itemIcon.transform.SetParent(inventorySlot.transform);
        itemIcon.transform.Rotate(90,0,0);
        itemIcon.transform.Translate(0,0,-13.34f);
    }
    public void SetParent(Transform transform){
        inventorySlot.transform.SetParent(transform);
    }
}