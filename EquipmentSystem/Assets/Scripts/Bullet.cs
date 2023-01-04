using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
