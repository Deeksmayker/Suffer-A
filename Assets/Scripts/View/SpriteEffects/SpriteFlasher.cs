using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.SpriteEffects
{
    public class SpriteFlasher : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        private Material _originalMaterial;
        [SerializeField] private float flashDuration;

        private void Awake()
        {
            Fight.Enemy.OnEnemyDamaged.AddListener(StartFlash);
            //GlobalEvents.OnPlayerDamaged.AddListener(StartFlash);
        }

        private void StartFlash(GameObject obj)
        {
            StartCoroutine(Flash(obj));
        }
        
        private IEnumerator Flash(GameObject obj)
        {
            var sprite = obj.GetComponent<SpriteRenderer>();
            if (sprite == null)
                yield break;
            
            _originalMaterial = sprite.material;
            
            sprite.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            if (sprite != null)
                sprite.material = _originalMaterial;
        }
    }
}