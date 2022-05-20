using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

public class RemovableTiles : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>() == null)
            return;
        
        Destroy(parentObject);
    }
}
