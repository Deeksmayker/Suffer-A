using DefaultNamespace;
using UnityEngine;

namespace Mechanics
{
    public class OLDSTUFFPlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp = 3;
        private int currentHp;
        
        public bool IsAlive { get => currentHp > 0; }

        public void AddHp(int value = 1)
        {
            currentHp = Mathf.Clamp(currentHp + value, 0, maxHp);
        }

        public void RemoveHp(int value = 1)
        {
            currentHp = Mathf.Clamp(currentHp - value, 0, maxHp);

            if (currentHp == 0)
            {
                GlobalEvents.OnPlayerDeath?.Invoke();
            }
        }

        private void Awake()
        {
            currentHp = maxHp;
        }
    }
}