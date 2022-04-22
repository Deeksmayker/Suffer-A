using UnityEngine;

namespace DefaultNamespace.TextStuff
{
    [System.Serializable]
    public class Monologue
    {
        [TextArea(2, 5)]
        public string[] sentences;
    }
}