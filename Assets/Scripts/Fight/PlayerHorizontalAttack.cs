using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class PlayerHorizontalAttack : MonoBehaviour
    {
        public static UnityEvent OnEnemyHorizontalHit = new UnityEvent();
        
        [SerializeField] private Transform attackStartPoint;
        [SerializeField] protected LayerMask enemyLayer;
        //[SerializeField] private float attackDuration = 0.1f;
        [SerializeField] protected float attackCooldown = 0.4f;
        [SerializeField] protected float rangeX;
        [SerializeField] protected float rangeY;
        [SerializeField] protected GameObject attackEffect;
        protected bool CanAttack = true;

        public static UnityEvent<bool> OnHorizontalCanAttack = new UnityEvent<bool>();
        
        private void Awake()
        {
            PlayerInput.OnAttackKeyDown.AddListener(() => StartCoroutine(Attack()));
        }

        private IEnumerator Attack()
        {
            if (CanAttack == false || PlayerInput.VerticalRaw != 0)
                yield break;    

            OnHorizontalCanAttack.Invoke(false);
            CanAttack = false;
            StartCoroutine(ShowAttackLine());
            
            var enemiesInRange =
                Physics2D.OverlapBoxAll(attackStartPoint.position, new Vector2(rangeX, rangeY), 0, enemyLayer);
            
            if (enemiesInRange.Length != 0)
                OnEnemyHorizontalHit.Invoke();
            
            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().TakeDamage(PlayerPreferences.Damage);
            }

            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
            
            OnHorizontalCanAttack.Invoke(true);
            // _canAttack = false;
            // attackTrigger.SetActive(true);
            // yield return new WaitForSeconds(attackDuration);
            // attackTrigger.SetActive(false);
            // yield return new WaitForSeconds(attackCooldown);
            // _canAttack = true;
            // OnAttack.Invoke(true);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackStartPoint.position, new Vector3(rangeX, rangeY, 1));
        }

        private IEnumerator ShowAttackLine()
        {
            attackEffect.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            attackEffect.SetActive(false);
        }
    }
}