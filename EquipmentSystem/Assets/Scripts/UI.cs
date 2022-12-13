using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]private GameObject UIObject;
    [SerializeField]private GameObject ItemHolder;

    [SerializeField] private List<GameObject> itemholders;
    
    public void CreateItemUI(Item targetItem)
    {
        var newitemholder = Instantiate(ItemHolder,  transform.position, Quaternion.identity, UIObject.transform);
        var itemholder = newitemholder.GetComponent<Itemholder>();
        itemholder.itemtext.text = targetItem.ItemName; 
        itemholder.imageholder.sprite = targetItem.Icon;
        itemholders.Add(newitemholder);
    }

    public void RemoveItemUI(int index)
    {
        Destroy(itemholders[index].gameObject);
        itemholders.RemoveAt(index);
    }
}
