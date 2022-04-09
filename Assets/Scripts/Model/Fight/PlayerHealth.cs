using System.Collections;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHealth : MonoBehaviour//, IDamageble
    {
        [SerializeField] private float timeStopDuration;
        [SerializeField] private float damageImmunityDuration;

        private void Awake()
        {
            GlobalEvents.OnPlayerDamaged.AddListener((value) => StartCoroutine(TakeDamage(value)));
        }
        
        public IEnumerator TakeDamage(int value = 1)
        {
            StartCoroutine(Utils.StopTimeForWhile(timeStopDuration));
            GetComponent<PlayerController>().Bounce(new Vector2(PlayerPreferences.FaceRight ? -1 : 1, 0.3f) * 10);
            
            PlayerPreferences.DisableControl();
            
            PlayerPreferences.CanTakeDamage = false;
            yield return new WaitForSeconds(0.1f);
            
            PlayerPreferences.EnableControl();
            
            yield return new WaitForSeconds(damageImmunityDuration);
            PlayerPreferences.CanTakeDamage = true;
        }

        public void Die()
        {
            
        }
    }
}