using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EquipSystem : MonoBehaviour
{
    [SerializeField] private Transform dropPoint;
    private UI _ui;
    private Inventory _inventory;
    [Serializable]private struct BodyPoint
    {
        [field: SerializeField] public Transform Transform { get; set;}
        [field: SerializeField] public bool Occupied { get; set;}
        [field: SerializeField] public GameObject EquippedItem { get; set;}
    }
    [SerializeField] private BodyPoint[] bodyPoints;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // interact left hand
        {
            InteractWithItem(0);
        }

        if (Input.GetKeyDown(KeyCode.G)) // interact with right hand
        {
            InteractWithItem(1);
        }

        if (Input.GetKey(KeyCode.Q)) //drop current item
        {
            if (bodyPoints.Length <= _ui.currentItem) return;
            if (bodyPoints[_ui.currentItem].EquippedItem == null) return;
            Drop();
        }
    }

    private void InteractWithItem(int s)
    {
        if (bodyPoints.Length <= s) return;
        if (bodyPoints[s].EquippedItem == null) return;
        if (bodyPoints[s].EquippedItem.GetComponent<Item>() is IInteractable interactable) // current item
        {
            interactable.Interact();
        }
    }

    private void Start()
    {
        _ui = FindObjectOfType<UI>();
        _inventory = FindObjectOfType<Inventory>();
    }
    public IEnumerator Drop()
    {
        // get item from item holder reference and search it in inventory
        var item = _ui._itemholders[_ui.currentItem].itemreference;
        if (item == null)
        {
            print("What are you even dropping?");
            yield break;
        }
        _ui.SetItemHolderColor(_ui.currentItem, 3);
        _ui.RemoveItemUI(_ui.currentItem);
        item.transform.parent = null;
        
        _inventory.RemoveFromInventory(item);
        
        item.transform.position = dropPoint.position; 
        
        var l = bodyPoints.Length;
        for (int i = 0; i < l; i++)
        {
            if (bodyPoints[i].EquippedItem != item.gameObject) continue;
            bodyPoints[i].Occupied = false;
            bodyPoints[i].EquippedItem = null;
        }// position

        yield return new WaitForSeconds(0.5f);
        item.itemcollider.enabled = true;
    }
    public IEnumerator DropSpecific(Item item)
    {
        // ipv item zelf item refence veranderen
        var uiIndex = Array.FindIndex(_ui._itemholders, s => s.itemreference == item);
        //var uiItem = _ui.currentItem;
        
        var uIitem = _ui._itemholders[uiIndex].itemreference;
            
        _ui.RemoveItemUI(uiIndex);
        _ui.SetItemHolderColor(uiIndex, 3);
        _inventory.RemoveFromInventory(uIitem);
        
        item.transform.parent = null;
        
        var l = bodyPoints.Length;
        for (int i = 0; i < l; i++)
        {
            if (bodyPoints[i].EquippedItem != item.gameObject) continue;
            bodyPoints[i].Occupied = false;
            bodyPoints[i].EquippedItem = null;
        }// position

        yield return new WaitForSeconds(0.5f);
        item.itemcollider.enabled = true;
    }
    public void Equip(int bodyType)
    {
        var item = _ui._itemholders[_ui.currentItem].itemreference;
        if (bodyPoints[bodyType].Occupied) return;
        if (_inventory.inventory == null || _inventory.inventory.Count == 0)
        {
            print("Nothing inside inventory");
            return;
        }
        if (_ui.currentItem >= _inventory.inventory.Count) return;
        if (_ui._itemholders[_ui.currentItem].itemreference == null) return;
        
        //check IEquiped and equip to head, else equip to either left or right hand
        if (_inventory.inventory[_ui.currentItem] is IEquipable)
        {
            bodyType = 2;
        }
        
        
        Transform bodytransform = null;
        bodytransform = bodyPoints[bodyType].Transform;
        bodyPoints[bodyType].Occupied = true; //  set item to bodypoint and set to occupied
        
        // check if item is equipable
        var parent = GameObject.FindGameObjectWithTag("Player"); // naar field verplaatsen
        
        //pak ipv item in inventory de item via UI
        //var invItem = _inventory.inventory[_ui.currentItem];

        var uiItem = Array.Find(_ui._itemholders, s => s.itemreference.GetInstanceID() == item.GetInstanceID()).itemreference;
        
        var pos = uiItem.transform;
        pos.position = bodytransform.position;
        pos.parent = parent.transform;
        pos.rotation = parent.transform.rotation;
        
        bodyPoints[bodyType].EquippedItem = uiItem.gameObject; // attach to player
        _ui.SetItemHolderColor(_ui.currentItem, bodyType); // set item color
    }
}
