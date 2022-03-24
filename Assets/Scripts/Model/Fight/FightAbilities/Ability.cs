using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] protected float timeForCast;
        [SerializeField] protected float cooldown;
        [SerializeField] protected GameObject prefab;

        protected bool _canUse = true;

        protected abstract IEnumerator Cast();

        protected IEnumerator StartCooldown()
        {
            _canUse = false;
            yield return new WaitForSeconds(cooldown);
            _canUse = true;
        }
    }
}