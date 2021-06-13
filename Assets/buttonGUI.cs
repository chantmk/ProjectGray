using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class buttonGUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject w;
    public GameObject a;
    public GameObject s;
    public GameObject d;

    public GameObject space;

    public GameObject e;
    public GameObject mouseBase;
    public GameObject mouseL;
    public GameObject mouseR;

    public GameObject q;
    public GameObject f;

    //public SpriteState sprState = new SpriteState();

    private Image button_W;
    private Image button_A;
    private Image button_S;
    private Image button_D;

    private Image button_Space;

    private Image button_E;
    private Image button_ML;

    private Image button_Q;
    private Image button_F;


    private bool bool_W = false;
    private bool bool_A = false;
    private bool bool_S = false;
    private bool bool_D = false;
    private bool bool_Q = false;
    private bool isPicked = false;
    private bool isHealed = false;
    //private bool bool_Space = false;

    private int spaceCounter = 0;
    private int shootCounter = 0;

    CatTalkingManager catTakingManager;
    CatDialogueManager catDialogueManager;

    private int state = 0;

    [SerializeField]
    Sprite hlSprite;

    private void Awake()
    {
        //PlayerConfig.CurrentScene = (SceneEnum)SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        button_W = w.GetComponent<Image>();
        button_A = a.GetComponent<Image>();
        button_S = s.GetComponent<Image>();
        button_D = d.GetComponent<Image>();

        button_Space = space.GetComponent<Image>();

        button_E = e.GetComponent<Image>();
        button_ML = mouseL.GetComponent<Image>();

        button_Q = q.GetComponent<Image>();
        button_F = f.GetComponent<Image>();
        catTakingManager = GameObject.FindGameObjectWithTag("Cat").GetComponent<CatTalkingManager>();
        catDialogueManager = GameObject.FindGameObjectWithTag("CatBox").GetComponent<CatDialogueManager>();
        
/*
        w.SetActive(false);
        a.SetActive(false);
        s.SetActive(false);
        d.SetActive(false);
        space.SetActive(false);
        q.SetActive(false);
        e.SetActive(false);
        f.SetActive(false);
        mouseBase.SetActive(false);
        mouseL.SetActive(false);
        mouseR.SetActive(false);
*/
        if (PlayerConfig.CurrentScene == SceneEnum.BlackEnemyScene)
        {
            state = 0;
            catTakingManager.TriggerDialogue(0);
            w.SetActive(true);
            a.SetActive(true);
            s.SetActive(true);
            d.SetActive(true);
        }
        else if (PlayerConfig.CurrentScene == SceneEnum.BlackBossScene)
        {
            state = 6;
        }
        else
        {
            state = -1;
        }

    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case -1:
                w.SetActive(false);
                a.SetActive(false);
                s.SetActive(false);
                d.SetActive(false);
                space.SetActive(false);
                q.SetActive(false);
                e.SetActive(false);
                f.SetActive(false);
                mouseBase.SetActive(false);
                mouseL.SetActive(false);
                mouseR.SetActive(false);
                break;

            case 0:
                if (bool_W & bool_S & bool_A & bool_D)
                {
                    state = 1;
                    Debug.Log(state);
                    //   w.SetActive(false);
                    //   a.SetActive(false);
                    //   s.SetActive(false);
                    //   d.SetActive(false);
                    
                    catTakingManager.TriggerDialogue(state);

                    space.SetActive(true);
                }
                else
                {
                    if (space.active == true)
                    {
                        space.SetActive(false);
                        q.SetActive(false);
                        e.SetActive(false);
                        f.SetActive(false);
                        mouseBase.SetActive(false);
                        mouseL.SetActive(false);
                        mouseR.SetActive(false);
                    }
                    if (Input.GetKey(KeyCode.W) == true)
                    {
                        button_W.sprite = hlSprite;
                        //Debug.Log("W");
                        bool_W = true;
                    }
                    else if (Input.GetKey(KeyCode.A) == true)
                    {
                        button_A.sprite = hlSprite;
                        bool_A = true;
                    }
                    else if (Input.GetKey(KeyCode.S) == true)
                    {
                        button_S.sprite = hlSprite;
                        bool_S = true;
                    }
                    else if (Input.GetKey(KeyCode.D) == true)
                    {
                        button_D.sprite = hlSprite;
                        bool_D = true;
                    }
                }
                break;

            case 1:
                if (spaceCounter == 3)
                {
                    button_Space.sprite = hlSprite;
                    e.SetActive(true);
                    mouseBase.SetActive(true);
                    mouseL.SetActive(true);
                    mouseR.SetActive(true);
                    state = 2;
                    
                    catTakingManager.TriggerDialogue(state);
                }
                else
                {
                    
                    if (Input.GetKeyDown(KeyCode.Space) == true)
                    {
                        spaceCounter += 1;
                    }
                }
                break;

            case 2:
                if (shootCounter == 5)
                {
                    button_ML.sprite = hlSprite;
                    f.SetActive(true);
                    q.SetActive(true);
                    state = 3;
                    catTakingManager.TriggerDialogue(state);
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) == true)
                    {
                        shootCounter += 1;
                    }
                }
                break;

            case 3:
                
                if (isPicked)
                {
                    state = 4;
                    catTakingManager.TriggerDialogue(state);
                    button_F.sprite = hlSprite;
                }
                break;

            case 4:
                if (isHealed)
                {
                    state = 5;
                    catTakingManager.TriggerDialogue(state);
                    button_Q.sprite = hlSprite;
                }
                break;
            case 5:
                //catDialogueManager.StopDialogue();
                w.SetActive(false);
                a.SetActive(false);
                s.SetActive(false);
                d.SetActive(false);
                space.SetActive(false);
                q.SetActive(false);
                e.SetActive(false);
                f.SetActive(false);
                mouseBase.SetActive(false);
                mouseL.SetActive(false);
                mouseR.SetActive(false);
                break;

            default:
                w.SetActive(false);
                a.SetActive(false);
                s.SetActive(false);
                d.SetActive(false);
                space.SetActive(false);
                q.SetActive(false);
                e.SetActive(false);
                f.SetActive(false);
                mouseBase.SetActive(false);
                mouseL.SetActive(false);
                mouseR.SetActive(false);
                break;
        }
        if (Input.GetKey(KeyCode.W) == true)
        {
            button_W.sprite = hlSprite;
            //Debug.Log("W");
        }
    }

    public void setPick()
    {
        isPicked = true;
    }
    public void setHeal()
    {
        isHealed = true;
    }
}
