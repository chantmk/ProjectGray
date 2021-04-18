using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Decision
{
    Mercy,
    Kill
}

public class DecisionManager : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }
    public void StartDecision()
    {
        animator.SetBool("IsOpen", true);
    }

    public void EndDecision()
    {
        animator.SetBool("IsOpen", false);

    }

    public void Mercy()
    {
        EventPublisher.TriggerDecisionMake(Decision.Mercy);
        EndDecision();
    }

    public void Kill()
    {
        EventPublisher.TriggerDecisionMake(Decision.Kill);
        EndDecision();
    }
}
