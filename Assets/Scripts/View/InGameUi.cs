using System;
using DefaultNamespace.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private Slider bloodSlider;
        [SerializeField] private Slider healthSlider;

        private void Awake()
        {
            bloodSlider.value = PlayerPreferences.CurrentBlood;
            healthSlider.value = PlayerPreferences.CurrentHealth;
            PlayerHealth.OnDamageTaken.AddListener(DecreaseHealthValue);
            PlayerHealth.OnHeal.AddListener(IncreaseHealthValue);
            PlayerHealth.OnHeal.AddListener(DecreaseBloodValue);
            PlayerInGameInput.OnAbility.AddListener(DecreaseBloodValue);
            PlayerAttack.OnHit.AddListener(IncreaseBloodValue);
        }

        private void DecreaseHealthValue(int value)
        {
            healthSlider.value -= value;
        }

        private void IncreaseHealthValue()
        {
            healthSlider.value += 1;
        }

        private void DecreaseBloodValue()
        {
            bloodSlider.value -= PlayerPreferences.BloodSpend;
        }

        private void IncreaseBloodValue(float value)
        {
            bloodSlider.value += value;
        }
    }
}