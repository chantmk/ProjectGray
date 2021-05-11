using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogueManager : MonoBehaviour
{

	public static DialogueStateEnum currentDialogueState = DialogueStateEnum.Enter;
	private GameObject leftProfile;
	private Image leftProfileImage;
	private GameObject rightProfile;
	private Image rightProfileImage;
	private Text nameText;
	private Text dialogueText;
	private GameObject nextButton;
	private GameObject mercyButton;
	private GameObject killButton;
	private Animator animator;
	private Queue<Sentence> sentences;
	private bool isDecision = false;
	public AudioClip nextSound;
	public float nextVolume = 1f;
	public AudioClip mercySound;
	public float mercyVolume = 1f;
	public AudioClip killSound;
	public float killVolume = 1f;
	private AudioSource audioSrc;

	// Use this for initialization
	private void Start()
	{
		sentences = new Queue<Sentence>();
		
		leftProfile = transform.Find("LeftProfile").gameObject;
		leftProfileImage = leftProfile.GetComponent<Image>();
		rightProfile = transform.Find("RightProfile").gameObject;
		rightProfileImage = rightProfile.GetComponent<Image>();

		nameText = transform.Find("Name").GetComponent<Text>();
		dialogueText = transform.Find("DialogueText").GetComponent<Text>();
		animator = transform.GetComponent<Animator>();

		nextButton = transform.Find("NextButton").gameObject;
		killButton = transform.Find("KillButton").gameObject;
		mercyButton = transform.Find("MercyButton").gameObject;

		audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();
	}

	private void PlayDialogue(Dialogue dialogue)
    {
		if (dialogue.sentences.Length == 0)
        {
			StopDialogue();
        }
		animator.SetBool("IsOpen", true);
		PauseManager.PauseTime();
		sentences.Clear();

		foreach (Sentence sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		isDecision = dialogue.IsDecision;
		DisplayNextSentence();
	}

	public void DisplayNext()
    {
		audioSrc.PlayOneShot(nextSound, nextVolume);
		DisplayNextSentence();

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

		Sentence sentence = sentences.Dequeue();
		nameText.text = sentence.name;
		if (sentence.IsRight)
        {
			rightProfile.SetActive(true);
			leftProfile.SetActive(false);
			rightProfileImage.sprite = sentence.ProfilePicture;
        }
        else
        {
			rightProfile.SetActive(false);
			leftProfile.SetActive(true);
			leftProfileImage.sprite = sentence.ProfilePicture;
        }

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence.sentence));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void StartDialogue(DialogueStateEnum dialogueState, Dialogue dialogue)
	{
		nextButton.SetActive(true);
		killButton.SetActive(false);
		mercyButton.SetActive(false);
		currentDialogueState = dialogueState;
		PlayDialogue(dialogue);
	}

	public void StopDialogue()
	{
		// Call event to invoke other that may subscribing this event
		nextButton.SetActive(false);
		mercyButton.SetActive(false);
		killButton.SetActive(false);
		animator.SetBool("IsOpen", false);
		EventPublisher.TriggerDialogueDone();
		PauseManager.ResumeTime();
	}

	public void Mercy()
	{
		audioSrc.PlayOneShot(mercySound, mercyVolume);
		StopDialogue();
		EventPublisher.TriggerDecisionMake(DecisionEnum.Mercy);
		Debug.Log("Trigger");
	}

	public void Kill()
	{
		audioSrc.PlayOneShot(killSound, killVolume);
		StopDialogue();
		EventPublisher.TriggerDecisionMake(DecisionEnum.Kill);
	}
}
