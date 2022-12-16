using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupsystem : MonoBehaviour
{
    [SerializeField] private Vector3 displacementPosition;
    private Inventory _inv;

    private void Start()
    {
        _inv = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Item item)) return;
        var obj = item.gameObject;
        item.itemcollider.enabled = false;
        print($"Picked up {item.ItemName}");
        _inv.AddToInventory(item);
        Displace(obj);
    }

    private void Displace(GameObject targetObj)
    {
        targetObj.transform.position = displacementPosition;
    }
}
