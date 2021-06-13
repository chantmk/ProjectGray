using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class CatDialogueManager : MonoBehaviour
{
	[SerializeField]
	private float delayTime = 0.0f;
	[SerializeField]
	private float endTime = 0.0f;

	private static bool waitForAction = false;
	private Text catText;
    private Animator animator;
	private Queue<CatSentence> sentences;
	private bool isFinish = false;
	private float timeLeft = 0.0f;

	void Start()
    {
	}

    private void Awake()
    {
		catText = transform.Find("CatDialogue").GetComponent<Text>();
		animator = transform.GetComponent<Animator>();
		sentences = new Queue<CatSentence>();
	}

    private void Update()
    {
        if(isFinish && !waitForAction)
        {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0.0)
            {
				StopDialogue();
				timeLeft = endTime;
				isFinish = false;
            }
        }
    }
    private void PlayDialogue(CatDialogue catDialogue)
	{
		if (catDialogue.catSentences.Length == 0)
		{
			StopDialogue();
		}
		if(animator == null)
        {
			animator = transform.GetComponent<Animator>();
        }
		animator.SetBool(AnimatorParams.IsOpen, true);
		sentences.Clear();

		foreach (CatSentence catSentence in catDialogue.catSentences)
		{
			sentences.Enqueue(catSentence);
		}
		waitForAction = catDialogue.isWait;
		if(waitForAction && catDialogue.timeOut >= 0.0f)
        {
			timeLeft = catDialogue.timeOut;
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			isFinish = true;
			return;
		}

		CatSentence sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence.sentence, DisplayNextSentence));
	}

	private IEnumerator TypeSentence(string sentence, System.Action callback)
	{
		catText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			catText.text += letter;
			yield return null;
		}
		yield return new WaitForSeconds(delayTime);
        if (callback != null) callback();
    }

	public void StartDialogue(CatDialogue catDialogue)
	{
		PlayDialogue(catDialogue);
	}

	public void StopDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

	public static void TriggerAction()
    {
		waitForAction = false;
    }
}
