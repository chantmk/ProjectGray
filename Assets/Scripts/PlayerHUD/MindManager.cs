using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MindManager : MonoBehaviour
{
    private GameObject blackMind;
    private GameObject blackGuardian;
    private Image blackBar;
    private bool isBlackGuard = false;

    private GameObject blueMind;
    private GameObject blueGuardian;
    private Image blueBar;
    private bool isBlueGuard = false;

    // Start is called before the first frame update
    void Start()
    {
        blackMind = transform.Find("BlackMindContainer").gameObject;
        blackGuardian = blackMind.transform.Find("Guardian").gameObject;
        blackBar = blackMind.transform.Find("BlackBar").GetComponent<Image>();
        blackBar.fillAmount = 0.0f;

        blueMind = transform.Find("BlueMindContainer").gameObject;
        blueGuardian = blueMind.transform.Find("Guardian").gameObject;
        blueBar = blueMind.transform.Find("BlueBar").GetComponent<Image>();
        blueBar.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        blackGuardian.SetActive(isBlackGuard);
        blueGuardian.SetActive(isBlueGuard);
    }

    public void UpdateBar(CharacterNameEnum characterEnum, float ratio)
    {
        switch(characterEnum)
        {
            case CharacterNameEnum.Black:
                blackBar.fillAmount = ratio;
                break;
            case CharacterNameEnum.Blue:
                blueBar.fillAmount = ratio;
                break;
        }
    }
}
