using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class CatDialogueManager : MonoBehaviour
{
	[SerializeField]
	private float delayTime = 0.0f;

	private Text catText;
    private Animator animator;
	private Queue<Sentence> sentences;
	private bool isFinish = false;
	private float timeLeft = 0.0f;

	void Start()
    {
        catText = transform.Find("CatDialogue").GetComponent<Text>();
        animator = transform.GetComponent<Animator>();
		sentences = new Queue<Sentence>();
		timeLeft = delayTime;
	}

    private void Update()
    {
        if(isFinish)
        {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0.0)
            {
				StopDialogue();
				timeLeft = delayTime;
				isFinish = false;
            }
        }
    }
    private void PlayDialogue(Dialogue dialogue)
	{
		if (dialogue.sentences.Length == 0)
		{
			StopDialogue();
		}
		animator.SetBool(AnimatorParams.IsOpen, true);
		sentences.Clear();

		foreach (Sentence sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
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

		Sentence sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence.sentence));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		catText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			catText.text += letter;
			yield return null;
		}
		DisplayNextSentence();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		PlayDialogue(dialogue);
	}

	public void StopDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}
