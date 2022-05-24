using System;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Platformer
{
    public class BloodFloor : MonoBehaviour
    {
        [SerializeField] private float bloodPerSecond;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == null)
                return;

            PlayerPreferences.CurrentBlood += bloodPerSecond * Time.deltaTime;
            InGameUi.OnBloodIncrease.Invoke(bloodPerSecond * Time.deltaTime);
        }
    }
}