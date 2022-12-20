using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item, IInteractable
{
    [SerializeField] private Light _light;
    public void Interact()
    {
        this._light.enabled = !this._light.enabled;
    }
}
