using System.Collections;
using DefaultNamespace.Fight;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneReloader : MonoBehaviour
    {
        private Animator _blackScreenAnimator;
        
        private void Awake()
        {
            _blackScreenAnimator = GameObject.FindWithTag("DarkScreen").GetComponent<Animator>();
            
            PlayerHealth.OnDie.AddListener(() => StartCoroutine(ReloadSceneOnDie()));
        }

        private IEnumerator ReloadSceneOnDie()
        {
            var scene = SceneManager.GetActiveScene();
            
            _blackScreenAnimator.SetTrigger("Start");

            yield return new WaitForSeconds(PlayerPreferences.RespawnTime);
            
            SceneManager.LoadScene(scene.name);
            PlayerPreferences.CurrentHealth = PlayerPreferences.MaxHealth;
            PlayerPreferences.CurrentBlood = 0;
            PlayerPreferences.CanMove = true;
            PlayerPreferences.EnableControl();
            PlayerPreferences.CanTakeDamage = true;
            GameObject.FindWithTag("Player").transform.position = PlayerPreferences.MajorSpawnPoint;
            _blackScreenAnimator.SetTrigger("Stop");

            yield return new WaitForSeconds(1);
            
            _blackScreenAnimator.SetTrigger("Stop");
        }
    }
}