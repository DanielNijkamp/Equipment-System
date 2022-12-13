using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEquipable
{
    public virtual void Equip(){}
    public virtual void OnEquip(){}
    public virtual void OnUnEquip(){}
}
