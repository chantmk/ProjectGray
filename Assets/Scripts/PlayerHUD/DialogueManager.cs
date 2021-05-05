using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogueManager : MonoBehaviour
{

	private Text nameText;
	private Text dialogueText;
	private Animator animator;
	private Queue<string> sentences;
	private Transform dialogueBox;
	private PauseManager pauseManager;
	private GameObject nextButton;
	private GameObject mercyButton;
	private GameObject killButton;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
		dialogueBox = transform.Find("DialogueBox");
		pauseManager = dialogueBox.parent.GetComponent<PauseManager>();
		nameText = dialogueBox.Find("Name").GetComponent<Text>();
		dialogueText = dialogueBox.Find("DialogueText").GetComponent<Text>();
		animator = dialogueBox.GetComponent<Animator>();

		nextButton = dialogueBox.Find("NextButton").gameObject;
		killButton = dialogueBox.Find("KillButton").gameObject;
		mercyButton = dialogueBox.Find("MercyButton").gameObject;
    }

	void PlayDialogue(Dialogue dialogue)
    {
		animator.SetBool("IsOpen", true);
		pauseManager.Pause();
		nameText.text = dialogue.name;
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence(dialogue.IsDecision);
	}

	void StopDialogue()
	{
		// Call event to invoke other that may subscribing this event
		EventPublisher.TriggerDialogueDone();
		animator.SetBool("IsOpen", false);
		pauseManager.Resume();
	}

	public void DisplayNextSentence(bool isDecision)
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
