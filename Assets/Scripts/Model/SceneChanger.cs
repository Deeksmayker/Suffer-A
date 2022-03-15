using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private Scene nextScene;

        private void OnTriggerEnter2D(Collider2D col)
        {
            
        }
    }
}