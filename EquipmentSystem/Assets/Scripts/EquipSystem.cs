using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EquipSystem : MonoBehaviour
{
    [SerializeField] private Transform dropPoint;
    public GameObject prefab;
    private UI _ui;
    private Inventory _inventory;
    [Serializable]private struct BodyPoint
    {
        [field: SerializeField] public Transform Transform { get; set;}
        [field: SerializeField] public bool Occupied { get; set;}
        [field: SerializeField] public GameObject EquippedItem { get; set;}
    }
    [SerializeField] private BodyPoint[] bodyPoints;
    
    private void Start()
    {
        _ui = FindObjectOfType<UI>();
        _inventory = FindObjectOfType<Inventory>();
    }
    public void DeAquip()
    {
        try
        {
            StartCoroutine(DropItem());
        }
        catch (ArgumentOutOfRangeException)
        {
            print("Item is not in inventory");
        }
        
    }
    
    public void Drop()
    {
        DeAquip();
    }
    public IEnumerator DropItem()
    {
        var indexOfObj = _ui._itemholders[_ui.currentItem];
        var targetObj = indexOfObj.itemreference;
        if (targetObj == null)
        {
            print("What are you even dropping?");
            yield break;
        }
        var index = Array.IndexOf(_inventory.inventory.ToArray(), targetObj);
        _ui.SetItemHolderColor(_ui.currentItem, 3);
        _ui.RemoveItemUI(_ui.currentItem);
        targetObj.transform.parent = null;
        _inventory.RemoveFromInventory(targetObj);
        targetObj.transform.position = dropPoint.position;
        yield return new WaitForSeconds(1f);
        targetObj.itemcollider.enabled = true;
        
        // update body point
        var l = bodyPoints.Length;
        for (int i = 0; i < l; i++)
        {
            if (bodyPoints[i].EquippedItem == targetObj.gameObject)
            {
                bodyPoints[i].Occupied = false;
                bodyPoints[i].EquippedItem = null;
                yield break;
            }
        }
    }
    public void Equip(int invIndex, int bodyType)
    {
        if (bodyPoints[bodyType].Occupied) return;
        if (_inventory.inventory == null || _inventory.inventory.Count == 0)
        {
            print("Nothing inside inventory");
            return;
        }
        Transform bodytransform = null;
        int index = 0;
        switch (bodyType)
        {
            case 0:
                bodytransform = bodyPoints[0].Transform;
                index = 0;
                bodyPoints[0].Occupied = true;
                break;
            case 1:
                bodytransform = bodyPoints[1].Transform;
                index = 1;
                bodyPoints[1].Occupied = true;
                break;
            case 2:
                bodytransform = bodyPoints[2].Transform;
                index = 2;
                bodyPoints[2].Occupied = true;
                break;
        } // get body type
        // check if item is equipable

        var parent = GameObject.FindGameObjectWithTag("Player"); 
        var newobject =  _inventory.inventory[invIndex];
        var pos = newobject.transform;
        pos.position = bodytransform.position;
        pos.parent = parent.transform;
        bodyPoints[index].EquippedItem = newobject.gameObject; // attach to player
        //newobject.IsEquipped = (true, parent);
        _ui.SetItemHolderColor(invIndex, bodyType);
    }
}
