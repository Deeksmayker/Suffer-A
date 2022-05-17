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
        private GameObject monologuePanel;
        private Text monologueText;
        private Queue<string> sentences;

        private void Awake()
        {
            monologuePanel = GameObject.FindWithTag("Info");
            monologueText = monologuePanel.GetComponentInChildren<Text>();
            
            if (character == null)
                character = FindObjectOfType<PlayerController>().transform;
            sentences = new Queue<string>(); 
            MonologueTrigger.OnMonologueTriggered.AddListener(StartMonologue);
        }

        private void StartMonologue(Monologue monologue)
        {
            sentences.Clear();
            monologuePanel.GetComponent<Image>().enabled = true;
            monologuePanel.GetComponentInChildren<Text>().enabled = true;

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
            monologuePanel.GetComponent<Image>().enabled = false;
            monologuePanel.GetComponentInChildren<Text>().enabled = false;
        }
        
        private void LateUpdate()
        {
            if (character == null)
                character = FindObjectOfType<PlayerController>().transform;
            
            FollowCharacter();
        }

        private void FollowCharacter()
        {
            monologuePanel.transform.position = new Vector3(character.position.x, character.transform.position.y + 2,
                monologuePanel.transform.position.z);
        }
    }
}