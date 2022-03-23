using System;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public class PlayerAttackTrigger : MonoBehaviour
    {
        [SerializeField] private int layerIndex;

        private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.gameObject.layer == layerIndex)
                    Debug.Log("FSD");
            }
    }
} 