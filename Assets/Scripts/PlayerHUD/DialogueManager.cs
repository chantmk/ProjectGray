using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogueManager : MonoBehaviour
{

	public static DialogueStateEnum currentDialogueState = DialogueStateEnum.Enter;
	private GameObject dialogueContainer;
	private GameObject leftProfile;
	private Image leftProfileImage;
	private GameObject rightProfile;
	private Image rightProfileImage;
	private Text nameText;
	private Text dialogueText;
	private GameObject nextButton;
	private GameObject decisionBox;
	private GameObject mercyButton;
	private GameObject killButton;
	private Animator animator;
	private Queue<Sentence> sentences;
	
	private bool isDecision = false;
	private CharacterNameEnum holderName;

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

		animator = transform.GetComponent<Animator>();

		dialogueContainer = transform.Find("DialogueContainer").gameObject;
		nameText = dialogueContainer.transform.Find("Name").GetComponent<Text>();
		dialogueText = dialogueContainer.transform.Find("DialogueText").GetComponent<Text>();
		nextButton = dialogueContainer.transform.Find("NextButton").gameObject;

		decisionBox = transform.parent.Find("DecisionBox").gameObject;
		killButton = decisionBox.transform.Find("KillButton").gameObject;
		mercyButton = decisionBox.transform.Find("MercyButton").gameObject;

		audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();

		EventPublisher.PlayCutscene += Hide;
	}

    private void OnDestroy()
    {
		EventPublisher.PlayCutscene -= Hide;
    }

    public void Hide()
    {
		animator.SetBool(AnimatorParams.IsOpen, false);
    }

	private void PlayDialogue(Dialogue dialogue)
    {
		if (dialogue.sentences.Length == 0)
        {
			StopDialogue();
        }
		animator.SetBool(AnimatorParams.IsOpen, true);
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
			decisionBox.SetActive(true);
		}
		if (sentences.Count == 0)
		{
			StopDialogue();
			return;
		}

		Sentence sentence = sentences.Dequeue();
		nameText.text = CharacterName.GetName(sentence.name);
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
		decisionBox.SetActive(false);
		currentDialogueState = dialogueState;
		PlayDialogue(dialogue);
		EventPublisher.TriggerDialogueStart();
	}

	public void StopDialogue()
	{
		// Call event to invoke other that may subscribing this event
		nextButton.SetActive(false);
		decisionBox.SetActive(false);
		animator.SetBool("IsOpen", false);
		EventPublisher.TriggerDialogueDone();
		PauseManager.ResumeTime();
	}

	public void setHolderName(CharacterNameEnum holderEnum)
    {
		holderName = holderEnum;
    }

    public void Mercy()
    {
        audioSrc.PlayOneShot(mercySound, mercyVolume);
        StopDialogue();
        EventPublisher.TriggerDecisionMake(DecisionEnum.Mercy, holderName);
    }

    public void Kill()
    {
        audioSrc.PlayOneShot(killSound, killVolume);
        StopDialogue();
        EventPublisher.TriggerDecisionMake(DecisionEnum.Kill, holderName);
    }
}
