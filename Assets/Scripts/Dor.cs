using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(GameObject.FindWithTag("Player").GetComponent<Inventary>().Key) 
            Destroy(gameObject);
        
    }
}
