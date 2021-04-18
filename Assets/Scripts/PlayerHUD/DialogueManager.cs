using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;
	public Animator animator;

	private Queue<string> sentences;
	private Transform dialogueBox;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
		dialogueBox = transform.Find("DialogueBox");
		//dialogueBox.Find("KillButton").gameObject.SetActive(false);
		//dialogueBox.Find("MercyButton").gameObject.SetActive(false);
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		/*if (sentences.Count == 1)
        {
            dialogueBox.Find("KillButton").gameObject.SetActive(true);
            dialogueBox.Find("MercyButton").gameObject.SetActive(true);
            dialogueBox.Find("NextButton").gameObject.SetActive(false);
        }*/
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		// Call event to invoke other that may subscribing this event
		EventPublisher.TriggerDialogueDone();
		animator.SetBool("IsOpen", false);
	}

}
