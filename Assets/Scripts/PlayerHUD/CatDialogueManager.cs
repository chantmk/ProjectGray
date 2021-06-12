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
	private static float timeLeft = 0.0f;

	void Start()
    {
        catText = transform.Find("CatDialogue").GetComponent<Text>();
        animator = transform.GetComponent<Animator>();
		sentences = new Queue<CatSentence>();
		timeLeft = endTime;
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
		animator.SetBool(AnimatorParams.IsOpen, true);
		sentences.Clear();

		foreach (CatSentence catSentence in catDialogue.catSentences)
		{
			sentences.Enqueue(catSentence);
		}
		waitForAction = catDialogue.isWait;
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
			Debug.Log("Coroutine");
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

	public static void TriggerAction(float newEndTime = -1.0f)
    {
		waitForAction = false;
		if(newEndTime >= 0.0f)
        {
			timeLeft = newEndTime;
        }
    }
}
