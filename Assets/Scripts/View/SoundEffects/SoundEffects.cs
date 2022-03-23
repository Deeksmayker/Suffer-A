using UnityEngine;

namespace DefaultNamespace.View.SoundEffects
{
    public class SoundEffects
    {
        public static void PlayPreparedChargedAttackSound(AudioSource clip, Transform position)
        {
            GameObject.Instantiate(clip, position);
        }
    }
}