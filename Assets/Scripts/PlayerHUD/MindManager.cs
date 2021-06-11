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
    private Animator mindAnim;

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

        mindAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        blackGuardian.SetActive(isBlackGuard);
        blueGuardian.SetActive(isBlueGuard);
    }

    public void UpdateBar(ColorEnum colorEnum, float ratio)
    {
        switch(colorEnum)
        {
            case ColorEnum.Black:
                blackBar.fillAmount = ratio;
                mindAnim.SetBool("isBlackFull", ratio == 1f);
               
                
                break;
            case ColorEnum.Blue:
                blueBar.fillAmount = ratio;
                mindAnim.SetBool("isBlueFull", ratio == 1f);
                break;
        }
    }
    

}
