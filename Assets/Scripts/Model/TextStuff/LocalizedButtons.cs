using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TextStuff
{
    public class LocalizedButtons : MonoBehaviour
    {
        [SerializeField] private Text description;
        [SerializeField] private Text title;
        
        public void SetText(LeanPhrase phrase)
        {
            description.text = phrase.Entries
                .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text;
        }

        public void SetTitle(LeanPhrase titlePhrase)
        {
            title.text = titlePhrase.Entries
                .Find(a => a.Language == Lean.Localization.LeanLocalization.GetFirstCurrentLanguage()).Text;
        }
    }
}