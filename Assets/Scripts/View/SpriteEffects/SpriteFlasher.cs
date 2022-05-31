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
            var sprites = obj.GetComponentsInChildren<SpriteRenderer>();
            if (sprites == null || sprites.Length == 0)
                yield break;
            
            if (_originalMaterial == null)
                _originalMaterial = sprites[0].material;

            foreach (var sprite in sprites)
            {
                sprite.material = flashMaterial;
            }
            
            yield return new WaitForSeconds(flashDuration);
            if (sprites[0] != null)
            {
                foreach (var sprite in sprites)
                {
                    sprite.material = _originalMaterial;
                }
            }
        }
    }
}