using UnityEngine;

namespace DefaultNamespace
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector3 playerSpawnPoint;
        
        private void Awake()
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
            }
        }
    }
}