using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace DefaultNamespace
{
    public class SpeedrunTimer : MonoBehaviour
    {
        public float SecondsPassed;
        
        private bool _timerEnabled;
        private Text _text;

        private LeaderboardYG leaderboardYG;

        private void Start()
        {
            FirstRoomCutscene.OnPlayBegin.AddListener(CheckSpeedrun);
            Credits.OnGameEnd.AddListener(CheckSpeedrunEnd);
            
            _text = GetComponent<Text>();
            leaderboardYG = GetComponent<LeaderboardYG>();
        }

        private void Update()
        {
            if (!_timerEnabled)
                return;

            SecondsPassed += Time.deltaTime;

            _text.text = ((int)(SecondsPassed * 1000)).ToString();
        }

        private void CheckSpeedrun()
        {
            if (GetComponent<Text>().enabled)
                StartTimer();
        }
            
        public void StartTimer()
        {
            SecondsPassed = 0;
            _timerEnabled = true;
        }

        private void CheckSpeedrunEnd()
        {
            if (GetComponent<Text>().enabled)
                StopTimer();
        }

        public void StopTimer()
        {
            YandexGame.SaveProgress();
            var yg = GameObject.Find("YandexGame").GetComponent<YandexGame>();
            yg._AuthorizationCheck();
            _timerEnabled = false;
            leaderboardYG.NewScore((int)(SecondsPassed * 1000));
        }

        public void SetSpeedrunMode(bool value)
        {
            GetComponent<Text>().enabled = value;
        }
    }
}