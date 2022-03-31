using UnityEngine;

namespace DefaultNamespace.View.SoundEffects
{
    public class SoundEffects
    {
        public static void PlaySound(AudioSource clip, Transform position)
        {
            GameObject.Instantiate(clip, position);
        }
    }
}