using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TextStuff
{
    public class MonologueManager : MonoBehaviour
    {
        private Transform _character;
        private GameObject _monologuePanel;
        private Text _monologueText;
        private Queue<string> _sentences;

        private void Awake()
        {
            _monologuePanel = GameObject.Find("Replic");
            _monologueText = _monologuePanel.GetComponentInChildren<Text>();
            
            if (_character == null)
                _character = FindObjectOfType<PlayerController>().transform;
            _sentences = new Queue<string>(); 
            MonologueTrigger.OnMonologueTriggered.AddListener(StartMonologue);
        }

        private void StartMonologue(Monologue monologue)
        {
            _sentences.Clear();
            _monologuePanel.GetComponent<Image>().enabled = true;
            _monologuePanel.GetComponentInChildren<Text>().enabled = true;

            foreach (var sentence in monologue.sentences)
            {
                _sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        }

        private void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndMonologue();
                return;
            }

            var sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private IEnumerator TypeSentence(string sentence)
        {
            _monologueText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                _monologueText.text += letter;
                yield return new WaitForSeconds(0.05f); 
            }

            yield return new WaitForSeconds(3f);
            DisplayNextSentence();
        }

        private void EndMonologue()
        {
            _monologuePanel.GetComponent<Image>().enabled = false;
            _monologuePanel.GetComponentInChildren<Text>().enabled = false;
        }
        
        private void LateUpdate()
        {
            if (_character == null)
                _character = FindObjectOfType<PlayerController>().transform;
            
            FollowCharacter();
        }

        private void FollowCharacter()
        {
            _monologuePanel.transform.position = new Vector3(_character.position.x, _character.transform.position.y + 2,
                _monologuePanel.transform.position.z);
        }
    }
}