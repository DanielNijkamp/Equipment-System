using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipSystem : MonoBehaviour
{
    public GameObject prefab;
    private UI _ui;
    [Serializable]private struct BodyPoint
    {
        [field: SerializeField]public Transform Transform { get; set;}
        [field: SerializeField]public bool Occupied { get; set;}
        [field: SerializeField]public GameObject EquippedItem { get; set;}
    }
    [SerializeField]private BodyPoint[] bodyPoints;

    private void Start()
    {
        _ui = FindObjectOfType<UI>();
        //_inv = FindObjectOfType<Inventory>();
    }

    public void test()
    {
        Equip(prefab, 0);
    }
    public void DeAquip()
    {
        Destroy(bodyPoints[0].EquippedItem);
        _ui.RemoveItemUI(0);
        bodyPoints[0].Occupied = false;
    }
    public void Equip(GameObject targetobject, int bodyType)
    {
        if (bodyPoints[bodyType].Occupied) return;
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
        }
        var parent = GameObject.FindGameObjectWithTag("Player");
        var newobject = Instantiate(targetobject, bodytransform.position, bodytransform.rotation, parent.transform);
        bodyPoints[index].EquippedItem = newobject;
        _ui.CreateItemUI(newobject.GetComponent<Item>());
        //_inv.AddToInventory(newobject.GetComponent<Item>());
    }
}
