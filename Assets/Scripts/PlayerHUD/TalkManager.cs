using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

	/**
	 * This class manage bubble and player hud
	 * - instant and destroy bubble
	 * - Call hud and write the text
	 * - Contain Dialogues
	 */

	public Dialogue dialogue;
	public Dialogue mercyDialogue;
	public GameObject bubble;
	public Vector3 offset;
	public float BubbleDelay;

	private GameObject bubbleComponent;
	private bool isInstanted = false;

    public void Start()
    {
		EventPublisher.DialogueDone += DestroyBubble;
    }
    public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void TriggerDecision()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(mercyDialogue);
	}

	public void TriggerDotBubble()
	{
		if (!isInstanted)
		{
			bubbleComponent = Instantiate(bubble, transform.position + offset, Quaternion.Euler(Vector3.zero));
			bubbleComponent.transform.parent = transform;
			bubbleComponent.GetComponent<Animator>().SetBool("IsDotDotDot", true);
			isInstanted = true;
		}
	}

	public void TriggerExclamationBubble()
	{
		if (!isInstanted)
		{
			bubbleComponent = Instantiate(bubble, transform.position + offset, Quaternion.Euler(Vector3.zero));
			bubbleComponent.transform.parent = transform;
			bubbleComponent.GetComponent<Animator>().SetBool("IsExclamation", true);
			isInstanted = true;
		}
	}

	public void DestroyBubble()
	{
		if (isInstanted)
		{
			Destroy(bubbleComponent);
			isInstanted = false;
			EventPublisher.DialogueDone -= DestroyBubble;
		}
	}


    private void OnDrawGizmosSelected()
    {
		Gizmos.DrawSphere(transform.position + offset, 0.25f);
    }
}
