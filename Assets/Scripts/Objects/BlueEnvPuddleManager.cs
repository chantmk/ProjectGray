using UnityEngine;
using Utils;
using MathUtils = Utils.MathUtils;
using Random = UnityEngine.Random;

namespace Objects
{
    public class BlueEnvPuddleManager : MonoBehaviour
    {
        public GameObject puddleSplashPrefabs;
        public Animator animator;
        public bool shouldRemove;

        private float charge;
        private readonly float chargeStep = 0.4f;
        private readonly float maxCharge = 1;
        
        [SerializeField] private float dischargeRate; 
        private bool isAddCharge;

        private float dischargeCountDown;
        [SerializeField] private float maxDischargeCountDown;
        
        private SpriteRenderer spriteRenderer;
        
        private StateMachine<EnvStateEnum> stateMachine;

        public void AddChargeOneStep()
        {
            isAddCharge = true;
        }

        void Start()
        {
            stateMachine = new StateMachine<EnvStateEnum>(EnvStateEnum.Charging);

            shouldRemove = false;
            charge = chargeStep;
            dischargeCountDown = maxDischargeCountDown;
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            animator.SetFloat(AnimatorParams.AnimSpeed, Random.Range(0.1f,0.3f));
            ChangeAlpha();
        }
        
        private void ChangeCharge(float amount)
        {
            charge += amount;
            charge = Mathf.Max(Mathf.Min(charge, maxCharge), 0f);
            ChangeAlpha();
        }

        private void ChangeAlpha()
        {
            var color = spriteRenderer.color;
            color.a = charge;
            spriteRenderer.color = color;
        }
        
        private void Expire()
        {
            Die();
        }
        
        private void ActivatePuddle()
        {
            // Instantiate(puddleSplashPrefabs, transform.position, Quaternion.Euler(Vector3.zero));
            Die();
        }

        private void Die()
        {
            shouldRemove = true;
        }
        
        
        void FixedUpdate()
        {
            if (!shouldRemove)
            {
                switch (stateMachine.CurrentState)
                            {
                                case EnvStateEnum.ZeroCharge:
                                    break;
                                case EnvStateEnum.Charging:
                                    if (MathUtils.IsFloatEqual(1f, charge) && isAddCharge)
                                    {
                                        stateMachine.SetNextState(EnvStateEnum.Activataed);
                                    }
                                    else if (dischargeCountDown <= 0f)
                                    {
                                        stateMachine.SetNextState(EnvStateEnum.Discharging);
                                    }
                                    break;
                                case EnvStateEnum.Discharging:
                                    if (charge == 0f)
                                    {
                                        stateMachine.SetNextState(EnvStateEnum.ZeroCharge);
                                    }
                                    if (isAddCharge)
                                    {
                                        dischargeCountDown = maxDischargeCountDown;
                                        stateMachine.SetNextState(EnvStateEnum.Charging);
                                    }
                                    break;
                            }
                            
                            if (isAddCharge)
                            {
                                ChangeCharge(chargeStep); // 1/2 charge
                            }
                            
                            stateMachine.ChangeState();
                            
                            switch (stateMachine.CurrentState)
                            {
                                case EnvStateEnum.ZeroCharge:
                                    Expire();
                                    break;
                                case EnvStateEnum.Charging:
                                    dischargeCountDown -= Time.fixedDeltaTime;
                                    break;
                                case EnvStateEnum.Discharging:
                                    ChangeCharge( -dischargeRate*Time.fixedDeltaTime);
                                    break;
                                case EnvStateEnum.Activataed:
                                    if (stateMachine.PreviousState != EnvStateEnum.Activataed)
                                    {
                                        ActivatePuddle();
                                    }
                                    break;
                            }
                
                            isAddCharge = false;
            }
        }
    }
}