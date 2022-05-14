using System.Collections;
using Cinemachine;
using DefaultNamespace.Fight;
using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        [SerializeField] private float hitShakePower;
        [SerializeField] private float hitShakeTime;
        
        [SerializeField] private float blockShakePower;
        [SerializeField] private float blockShakeTime;

        [SerializeField] private float damagedShakePower;
        [SerializeField] private float damagedShakeTime;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            
            PlayerAttack.OnStrongHit.AddListener(() => StartCoroutine(Shake(hitShakePower, hitShakeTime)));
            PlayerHealth.OnDamageTaken.AddListener((a) => StartCoroutine(Shake(damagedShakePower * a, damagedShakeTime)));
        }

        private IEnumerator Shake(float power, float time)
        {
            var cinemachineNoise =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineNoise.m_AmplitudeGain = power;
            yield return new WaitForSeconds(time);

            cinemachineNoise.m_AmplitudeGain = 0;
        }
    }
}