
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class RockThrower : MonoBehaviour
    {
        [SerializeField] private float throwDelay;
        
        [SerializeField] private GameObject rock;

        private void Awake()
        {
            StartCoroutine(ThrowRock());
        }

        private IEnumerator ThrowRock()
        {
            while (true)
            {
                Instantiate(rock, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(throwDelay);
            }
        }
    }
}