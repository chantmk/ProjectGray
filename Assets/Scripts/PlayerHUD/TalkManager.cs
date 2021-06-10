using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TalkManager : MonoBehaviour
{

	/**
	 * This class manage bubble and player hud
	 * - instant and destroy bubble
	 * - Call hud and write the text
	 * - Contain Dialogues
	 */

	[Header("Talker name")]
	public CharacterNameEnum holderName;
	[Header("DialogueParameter")]
	public Dialogue enterDialogue;
	public Dialogue lastStandDialogue;
	public Dialogue askForMercyDialogue;
	public Dialogue killDialogue;
	public Dialogue mercyDialogue;
	[Header("Bubble parameter")]
	public GameObject bubble;
	public Vector3 offset;
	public float BubbleDelay;

	private GameObject bubbleComponent;
	private bool isInstanted = false;
	private DialogueManager dialogueManager;
    public void Start()
    {
		EventPublisher.DialogueDone += DestroyBubble;
		EventPublisher.DecisionMake += DecisionHandler;

		dialogueManager = FindObjectOfType<DialogueManager>();
		dialogueManager.setHolderName(holderName);
    }

    private void OnDestroy()
    {
		EventPublisher.DialogueDone -= DestroyBubble;
		EventPublisher.DecisionMake -= DecisionHandler;
    }
    public void TriggerDialogue(DialogueStateEnum dialogueState)
    {
		Debug.Log(dialogueState);
        switch (dialogueState)
        {
			case DialogueStateEnum.Enter:
				dialogueManager.StartDialogue(dialogueState, enterDialogue);
				break;
			case DialogueStateEnum.LastStand:
				dialogueManager.StartDialogue(dialogueState, lastStandDialogue);
				break;
			case DialogueStateEnum.Decision:
				dialogueManager.StartDialogue(dialogueState, askForMercyDialogue);
				break;
			case DialogueStateEnum.Mercy:
				dialogueManager.StartDialogue(dialogueState, mercyDialogue);
				break;
			case DialogueStateEnum.Kill:
				dialogueManager.StartDialogue(dialogueState, killDialogue);
				break;
			default:
				throw new System.NotImplementedException();
        }
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

	public void DecisionHandler(DecisionEnum decision, CharacterNameEnum bossName)
    {
		switch (decision)
        {
			case DecisionEnum.Mercy:
				TriggerDialogue(DialogueStateEnum.Mercy);
				break;
			case DecisionEnum.Kill:
				TriggerDialogue(DialogueStateEnum.Kill);
				break;
			default:
				break;
        }
    }

    private void OnDrawGizmosSelected()
    {
		Gizmos.DrawSphere(transform.position + offset, 0.25f);
    }
}
