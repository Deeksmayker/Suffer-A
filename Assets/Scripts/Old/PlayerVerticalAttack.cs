using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class PlayerVerticalAttack : MonoBehaviour
    {
        public static UnityEvent OnEnemyVerticalHit = new UnityEvent();
        public static UnityEvent OnVerticalCanAttack = new UnityEvent();
        
        [SerializeField] private Transform upAttackStartPoint, downAttackStartPoint;
        [SerializeField] protected LayerMask enemyLayer;
        //[SerializeField] private float attackDuration = 0.1f;
        [SerializeField] protected float attackCooldown = 0.4f;
        [SerializeField] protected float horizontalRangeX, horizontalRangeY;
        [SerializeField] protected float verticalRangeX, verticalRangeY;
        [SerializeField] protected GameObject upAttackEffect, downAttackEffect;
        protected bool CanAttack = true;

        private void Awake()
        {
            PlayerInGameInput.OnAttackKeyDown.AddListener(() => StartCoroutine(Attack()));
        }

        private IEnumerator Attack()
        {
            var IsUpAttack = PlayerInGameInput.VerticalRaw == 1;
            
            if (CanAttack == false || PlayerInGameInput.VerticalRaw == 0 || (!IsUpAttack && PlayerPreferences.IsGrounded))
                yield break;
            
            CanAttack = false;
            
            StartCoroutine(ShowAttackLine(IsUpAttack ? upAttackEffect : downAttackEffect));

            var currentAttackPoint = IsUpAttack ? upAttackStartPoint : downAttackStartPoint;
            
            var enemiesInRange =
                Physics2D.OverlapBoxAll(currentAttackPoint.position, new Vector2(horizontalRangeX, horizontalRangeY), 0, enemyLayer);
            
            if (enemiesInRange.Length != 0)
                OnEnemyVerticalHit.Invoke();
            
            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().TakeDamage(PlayerPreferences.Damage);
            }

            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
        }
        
        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(upAttackStartPoint.position, new Vector3(horizontalRangeX, horizontalRangeY, 1));
            Gizmos.DrawWireCube(downAttackStartPoint.position, new Vector3(horizontalRangeX, horizontalRangeY, 1));
        }

        private IEnumerator ShowAttackLine(GameObject line)
        {
            line.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            line.SetActive(false);
        }
    }
}