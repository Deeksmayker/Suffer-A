using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector3 playerSpawnPoint;

        private void Awake()
        {
            var playerOnMap = GameObject.FindWithTag("Player");
            if (playerOnMap)
            {
                GetComponent<CinemachineVirtualCamera>().Follow = playerOnMap.transform;
                return;
            }
            
            var player = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
            GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}