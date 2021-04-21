using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResemblanceRedeemManager : Interactable
{
    private int[] checkpointAmount = { 2, 4, 6 };
    public GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnInteract()
    {
        //show ui
        Debug.Log("Interact with resemblance redemption");
        //render ui according to the amount of resemblance player has

        //render redeem button

    }
}
