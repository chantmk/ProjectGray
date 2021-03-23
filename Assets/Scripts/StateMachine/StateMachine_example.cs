using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
// Disclaimer !!!!!!!!!!!!!!!!!!!!!!!
// All naming convention is not for this project
// Please DO NOT reference naming convention from this example
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public float InvulnerableDuration;
    public int MaxJuice = 3;
    public int Juice;
    public int Life = 5;
    public float boostDistance;
    public float FreeCostStateDuration;
    public float SuperStateDuration;
    public int DashJuice;
    public int JumpJuice;

    private bool _isFreeJump;

    public bool IsInvulnerable;

    public bool IsImmortal;

    private Text _stateText;
    // states enum
    private const int NORMAL = 0;
    private const int SUPER = 1;
    private const int FREE_COST = 2;
    private StateMachine stateMachine;

    [SerializeField] private GameObject _barrier;
    [SerializeField] private GameObject _hurtBox;

    private float superStateTimer;
    private float freeCostStateTimer;

    private void Awake()
    {
        _stateText = GetComponentInChildren<Text>();

        Juice = MaxJuice;
    }
    void Start()
    {
        // Add possible state
        stateMachine = new StateMachine();
        stateMachine.AddState(new State(NORMAL, null, null, null));
        stateMachine.AddState(new State(SUPER, SuperBegin, SuperUpdate, SuperEnd));
        stateMachine.AddState(new State(FREE_COST, FreeCostBegin, FreeCostUpdate, null));
        stateMachine.Initialize(NORMAL, NORMAL);
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void SuperBegin()
    {
        superStateTimer = SuperStateDuration;
        SetImmortal(true);
    }
    private void SuperEnd()
    {
        SetImmortal(false);
    }

    private int SuperUpdate()
    {
        superStateTimer -= Time.fixedDeltaTime;
        if (superStateTimer <= 0)
        {
            return NORMAL;
        }
        return SUPER;
    }

    private void FreeCostBegin()
    {
        freeCostStateTimer = FreeCostStateDuration;
    }

    private int FreeCostUpdate()
    {
        RefillJuice();
        freeCostStateTimer -= Time.fixedDeltaTime;
        if (freeCostStateTimer <= 0)
        {
            return NORMAL;
        }
        return FREE_COST;
    }

    private void SetImmortal(bool value)
    {
        if (value)
        {
            IsImmortal = true;

            _barrier.SetActive(true);
            _hurtBox.SetActive(false);
            return;
        }
        IsImmortal = false;
        _barrier.SetActive(false);
        _hurtBox.SetActive(true);
    }

    public void TakeDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
        IsInvulnerable = true;
        Invoke("SetInvulnerableFalse", InvulnerableDuration);
        Life -= 1;
        if (Life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RefillJuice()
    {
        Juice = MaxJuice;
    }

    public void StartSuper()
    {
        stateMachine.SetState(SUPER);
    }    

    public void StartBoost()
    {
        SetImmortal(true);
        PlayerMovement.Boost(boostDistance);
    }

    public void StartFreeCost()
    {
        stateMachine.SetState(FREE_COST);
    }

    void SetInvulnerableFalse()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        IsInvulnerable = false;
    }

    void Update()
    {
        // For Debug
        if (stateMachine.StateId == NORMAL)
            _stateText.text = "Normal";
        else if (stateMachine.StateId == SUPER)
            _stateText.text = "Super (Item3)";
        else if (stateMachine.StateId == FREE_COST)
            _stateText.text = "Free Cost (Item1)";
        else
            _stateText.text = "Unknown";
    }

    private void FixedUpdate()
    {
        stateMachine.Process();
    }

}
