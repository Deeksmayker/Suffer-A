using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Fin : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        private void OnCollisionEnter2D(Collision2D col)
        {
            obj.SetActive(true);
        }
    }
}