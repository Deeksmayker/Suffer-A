using UnityEngine;

namespace DefaultNamespace.View.VisualEffects
{
    public class ParticlesEffects
    {
        public static void StartParticle(ParticleSystem effect, Transform position)
        {
            GameObject.Instantiate(effect, position);
        }
    }
}