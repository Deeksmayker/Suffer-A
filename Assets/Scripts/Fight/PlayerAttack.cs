using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackTrigger;
        [SerializeField] private float attackDuration = 0.1f;
        [SerializeField] private float attackCooldown = 0.2f;
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
            attackTrigger.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            attackTrigger.SetActive(false);
            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;
            OnAttack.Invoke(true);
        }
    }
}