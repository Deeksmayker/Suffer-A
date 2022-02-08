using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class Thrower : MonoBehaviour
    {
        [SerializeField] private Vector3 startPoint, endPoint;
        [SerializeField] private float speed;
        [SerializeField] private float delay = 1;
        [SerializeField] private GameObject obj = null;

        private void Awake()
        {
            StartCoroutine(Throw());
        }

        private IEnumerator Throw()
        {
            while (true)
            {
                StartCoroutine(Spawn());
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator Spawn()
        {
            var current = Instantiate(obj, startPoint, Quaternion.identity);
            yield return Utils.MoveToTarget(current.transform, endPoint, speed);
            Destroy(current);
        }
    }
}