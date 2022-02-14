using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform attackStartPoint;
        [SerializeField] private LayerMask enemyLayer;
        //[SerializeField] private float attackDuration = 0.1f;
        [SerializeField] private float attackCooldown = 0.2f;
        [SerializeField] private float rangeX;
        [SerializeField] private float rangeY;
        [SerializeField] private GameObject attackEffect;
        private bool _canAttack = true;

        public static UnityEvent<bool> OnAttack = new UnityEvent<bool>();
        
        private void Awake()
        {
            PlayerInput.OnAttackKeyDown.AddListener(StartAttack);
        }

        private void StartAttack()
        {
            StartCoroutine(Attack());
        }
        
        private IEnumerator Attack()
        {
            if (_canAttack == false)
                yield break;    

            OnAttack.Invoke(false);
            _canAttack = false;
            attackEffect.SetActive(true);
            
            var enemiesInRange =
                Physics2D.OverlapBoxAll(attackStartPoint.position, new Vector2(rangeX, rangeY), 0, enemyLayer);
            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().TakeDamage(PlayerPreferences.Damage);
            }

            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;
            
            OnAttack.Invoke(true);
            // _canAttack = false;
            // attackTrigger.SetActive(true);
            // yield return new WaitForSeconds(attackDuration);
            // attackTrigger.SetActive(false);
            // yield return new WaitForSeconds(attackCooldown);
            // _canAttack = true;
            // OnAttack.Invoke(true);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackStartPoint.position, new Vector3(rangeX, rangeY, 1));
        }
    }
}