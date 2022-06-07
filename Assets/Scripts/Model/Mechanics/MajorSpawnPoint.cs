using System;
using DefaultNamespace;
using Movement;
using UnityEngine;

namespace Mechanics
{
    public class MajorSpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>() == null)
                return;

            PlayerPreferences.MajorSpawnPoint = transform.position;
            PlayerPrefs.SetInt("attack", PlayerPreferences.AttackAvailable ? 1 : 0);
            PlayerPrefs.SetInt("horizontal", PlayerPreferences.HorizontalAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("up", PlayerPreferences.UpAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("down", PlayerPreferences.DownAbilityAvailable ? 1 : 0);
            PlayerPrefs.SetInt("airLunge", PlayerPreferences.MaxLungeAirCount);
            PlayerPrefs.SetInt("scene", PlayerPreferences.CurrentSceneIndex);
            PlayerPrefs.SetInt("health", PlayerPreferences.CurrentHealth);
            PlayerPrefs.SetFloat("blood", PlayerPreferences.CurrentBlood);
            PlayerPrefs.SetFloat("majorX", PlayerPreferences.MajorSpawnPoint.x);
            PlayerPrefs.SetFloat("majorY", PlayerPreferences.MajorSpawnPoint.y);
            PlayerPrefs.Save();
        }
    }
}