using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TextStuff
{
    public class MonologueManager : MonoBehaviour
    {
        [SerializeField] private Transform character;
        [SerializeField] private GameObject monologuePanel;
        [SerializeField] private Text monologueText;
        private Queue<string> sentences;

        private void Awake()
        {
            if (character == null)
                character = FindObjectOfType<PlayerController>().transform;
            sentences = new Queue<string>(); 
            MonologueTrigger.OnMonologueTriggered.AddListener(StartMonologue);
        }

        private void StartMonologue(Monologue monologue)
        {
            sentences.Clear();
            monologuePanel.SetActive(true);

            foreach (var sentence in monologue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        }

        private void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndMonologue();
                return;
            }

            var sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private IEnumerator TypeSentence(string sentence)
        {
            monologueText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                monologueText.text += letter;
                yield return new WaitForSeconds(0.05f); 
            }

            yield return new WaitForSeconds(3f);
            DisplayNextSentence();
        }

        private void EndMonologue()
        {
            monologuePanel.SetActive(false);
        }
        
        private void LateUpdate()
        {
            FollowCharacter();
        }

        private void FollowCharacter()
        {
            monologuePanel.transform.position = new Vector3(character.position.x, character.transform.position.y + 2,
                monologuePanel.transform.position.z);
        }
    }
}