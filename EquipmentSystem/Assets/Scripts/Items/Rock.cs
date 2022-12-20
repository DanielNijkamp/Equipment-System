using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rock : Item, IInteractable
{
    [SerializeField] private float throwDistance;
    private Rigidbody _rigidbody;
    private EquipSystem _equipSystem;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _equipSystem = FindObjectOfType<EquipSystem>();
    }

    public void Interact()
    {
        StartCoroutine(_equipSystem.DropSpecific(this));
        StartCoroutine(Throw());
    }

    public IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.2f);
        _rigidbody.velocity = transform.forward * throwDistance;
        _rigidbody.isKinematic = false;
        yield return new WaitForSeconds(0.3f);
        _rigidbody.isKinematic = true;
    }
}
