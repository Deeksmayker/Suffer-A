using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class PanicCutsceen : MonoBehaviour
    {

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.WaitForInitialization();
            VideoAd.Show();
        }
    }
}