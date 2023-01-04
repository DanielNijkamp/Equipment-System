using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [field: SerializeField] public string ItemName { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public Collider itemcollider { get; set; }
}