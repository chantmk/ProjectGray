using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindManager : MonoBehaviour
{
    private GameObject blackMind;
    private GameObject blackGuardian;
    private GameObject blackBar;
    private bool isBlackGuard = false;

    private GameObject blueMind;
    private GameObject blueGuardian;
    private GameObject blueBar;
    private bool isBlueGuard = false;

    // Start is called before the first frame update
    void Start()
    {
        blackMind = transform.Find("BlackMindContainer").gameObject;
        blackGuardian = blackMind.transform.Find("Guardian").gameObject;
        blackBar = blackMind.transform.Find("BlackBar").gameObject;
        blueMind = transform.Find("BlueMindContainer").gameObject;
        blueGuardian = blackMind.transform.Find("Guardian").gameObject;
        blueBar = blackMind.transform.Find("BlackBar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        blackGuardian.SetActive(isBlackGuard);
        blueGuardian.SetActive(isBlueGuard);
    }

    public void updateBar()
    {

    }
}
