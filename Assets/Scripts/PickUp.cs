using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject.FindWithTag("Player").GetComponent<Inventary>().Key = true;
        Destroy(gameObject);
        
    }
}
