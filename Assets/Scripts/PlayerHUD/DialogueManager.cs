using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogueManager : MonoBehaviour
{
	private PauseManager pauseManager;
	private Text nameText;
	private Text dialogueText;
	private GameObject nextButton;
	private GameObject mercyButton;
	private GameObject killButton;
	private Animator animator;
	private Queue<string> sentences;
	private bool isDecision = false;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
		nameText = transform.Find("Name").GetComponent<Text>();
		dialogueText = transform.Find("DialogueText").GetComponent<Text>();
		animator = transform.GetComponent<Animator>();
		nextButton = transform.Find("NextButton").gameObject;
		killButton = transform.Find("KillButton").gameObject;
		mercyButton = transform.Find("MercyButton").gameObject;
		pauseManager = transform.parent.GetComponent<PauseManager>();
    }

	void PlayDialogue(Dialogue dialogue)
    {
		animator.SetBool("IsOpen", true);
		pauseManager.PauseTime();
		nameText.text = dialogue.name;
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		isDecision = dialogue.IsDecision;
		DisplayNextSentence();
	}

	void StopDialogue()
	{
		// Call event to invoke other that may subscribing this event
		EventPublisher.TriggerDialogueDone();
		animator.SetBool("IsOpen", false);
		pauseManager.ResumeTime();
	}

	public void DisplayNextSentence()
	{
		if (isDecision && sentences.Count == 1)
        {
			nextButton.SetActive(false);
			mercyButton.SetActive(true);
			killButton.SetActive(true);
		}
		if (sentences.Count == 0)
		{
			StopDialogue();
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

	public void StartDialogue(Dialogue dialogue)
	{
		nextButton.SetActive(true);
		killButton.SetActive(false);
		mercyButton.SetActive(false);
		PlayDialogue(dialogue);
	}

	public void Mercy()
	{
		EventPublisher.TriggerDecisionMake(DecisionEnum.Mercy);
		StopDialogue();
	}

	public void Kill()
	{
		EventPublisher.TriggerDecisionMake(DecisionEnum.Kill);
		StopDialogue();
	}

}
