using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClip : Item, IInteractable
{
    public int clipAmount;
    public void Interact()
    {
        StartCoroutine(Gun.instance.Reload(this));
    }
}
