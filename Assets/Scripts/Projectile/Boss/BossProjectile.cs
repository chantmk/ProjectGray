using System.Collections;
using Utils;

public class BossProjectile : EnemyProjectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        EventPublisher.DialogueStart += OnDialogueStart;
        //EventPublisher.DecisionMake += OnDecisionMakeHandler;
    }

    private void OnDestroy()
    {
        EventPublisher.DialogueStart -= OnDialogueStart;
        //EventPublisher.DecisionMake -= OnDecisionMakeHandler;
    }

    public void OnDecisionMakeHandler(DecisionEnum decision)
    {
        Execute();
    }

    public void OnDialogueStart()
    {
        Execute();
    }
}