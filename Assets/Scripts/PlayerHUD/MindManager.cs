using System;
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
    private Image blackWing;
    private bool isBlackGuard = false;

    private GameObject blueMind;
    private GameObject blueGuardian;
    private Image blueBar;
    private Image blueWing;
    private bool isBlueGuard = false;
    private Animator mindAnim;
    private Color blackOgCol;
    private Color blueOgCol;
    private Color blackBarOgCol;
    private Color blueBarOgCol;

    // Start is called before the first frame update
    void Start()
    {
        blackMind = transform.Find("BlackMindContainer").gameObject;
        blackGuardian = blackMind.transform.Find("Guardian").gameObject;
        blackBar = blackMind.transform.Find("BlackBar").GetComponent<Image>();
        blackWing = blackMind.transform.Find("Icon").GetComponent<Image>();
        blackBar.fillAmount = 0.0f;
        blackOgCol = blackWing.canvasRenderer.GetColor();

        blueMind = transform.Find("BlueMindContainer").gameObject;
        blueGuardian = blueMind.transform.Find("Guardian").gameObject;
        blueBar = blueMind.transform.Find("BlueBar").GetComponent<Image>();
        blueWing = blueMind.transform.Find("Icon").GetComponent<Image>();
        blueBar.fillAmount = 0.0f;
        blueOgCol = blueWing.canvasRenderer.GetColor();
        
        mindAnim = GetComponent<Animator>();
        EventPublisher.SetGuardianUI += SetGuardian;
    }

    private void OnDestroy()
    {
        EventPublisher.SetGuardianUI -= SetGuardian;
    }

    // Update is called once per frame
    void Update()
    {
        
        blackGuardian.SetActive(isBlackGuard);
        blueGuardian.SetActive(isBlueGuard);

        if (PlayerConfig.HaveResemblanceDict[ColorEnum.Blue] != blueWing.enabled)
        {
            blueWing.enabled = PlayerConfig.HaveResemblanceDict[ColorEnum.Blue];
        }

        if (PlayerConfig.HaveResemblanceDict[ColorEnum.Black] != blackWing.enabled)
        {
            blackWing.enabled = PlayerConfig.HaveResemblanceDict[ColorEnum.Black];
        }

        if (PlayerConfig.HaveResemblanceDict[ColorEnum.Blue] && blueWing.enabled)
        {
            if (isBlueGuard)
                blueWing.color = blueOgCol;
            else
                blueWing.color = Color.black;
        }
        
        if (PlayerConfig.HaveResemblanceDict[ColorEnum.Black] && blackWing.enabled)
        {
            if (isBlackGuard)
                blackWing.color = blackOgCol;
            else
                blackWing.color = Color.black;
        }
        
        
        
    }

    public void UpdateBar(ColorEnum colorEnum, float ratio)
    {
        switch(colorEnum)
        {
            case ColorEnum.Black:
                blackBar.fillAmount = ratio;
                mindAnim.SetBool("isBlackFull", ratio >= 0.8f);
                break;
            case ColorEnum.Blue:
                blueBar.fillAmount = ratio;
                mindAnim.SetBool("isBlueFull", ratio >= 0.8f);
                break;
        }
    }

    public void SetGuardian(ColorEnum colorEnum, bool enable)
    {
        switch(colorEnum)
        {
            case ColorEnum.Black:
                if (isBlackGuard != enable)
                    isBlackGuard = enable;
                break;
            case ColorEnum.Blue:
                if (isBlueGuard != enable)
                    isBlueGuard = enable;
                break;
        }
    }
}
