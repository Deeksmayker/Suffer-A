using System;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class BloodFloor : MonoBehaviour
    {
        [SerializeField] private float bloodPerSecond;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null)
                return;

            PlayerPreferences.CurrentBlood += bloodPerSecond * Time.deltaTime;
            InGameUi.OnBloodIncrease.Invoke(bloodPerSecond * Time.deltaTime);
        }
    }
}