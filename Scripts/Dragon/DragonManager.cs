using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonAttackTriggers
{
    Dash,
    LeftPaw,
    RightPaw,
    NearHowling,
    NearBreath,
    AirSpear,
    Tail,
    Rush
}

namespace DragonController
{
    [RequireComponent(typeof(MovementManager))]
    [RequireComponent(typeof(DragonStat))]
    [RequireComponent(typeof(Rigidbody))]
    public class DragonManager : Singleton<DragonManager> {

        [SerializeField]
        private LayerMask _dragonAvoidLayers;
        public LayerMask DragonAvoidLayers { get { return _dragonAvoidLayers; } }

        [SerializeField]
        private Transform _rayTransfrom;
        public Transform RayTransfrom { get { return _rayTransfrom; } }

        [SerializeField]
        private BehaviorTree _dragonBehaviroTree;
        public BehaviorTree DragonBehaviroTree { get { return _dragonBehaviroTree; } }

        [SerializeField]
        private Transform _leftPawTransform;
        public Transform LeftPawTransform { get { return _leftPawTransform; } }

        [SerializeField]
        private Transform _rightPawTransform;
        public Transform RightPawTransform { get { return _rightPawTransform; } }

        private DragonStat _stat;
        public DragonStat Stat { get { return _stat; } }

        private BoxCollider _dragonGroundCollider;
        public BoxCollider DragonGroundCollider { get { return _dragonGroundCollider; } }

        private Rigidbody _dragonRigidBody;
        public Rigidbody DragonRigidBody { get { return _dragonRigidBody; } }

        private static ActionTask _currentActionTask;
        public static ActionTask CurrentActionTask { get { return _currentActionTask; } }

        private Dictionary<DragonAttackTriggers, DragonAttackTrigger> _dragonAttackTriggers = new Dictionary<DragonAttackTriggers, DragonAttackTrigger>();

        private DragonAttackTriggers _currentAttackTrigger;
        public DragonAttackTriggers CurrentAttackTrigger { get { return _currentAttackTrigger; } }

        private bool _isAction;
        public bool IsAction { set { _isAction = value; } get { return _isAction; } }

        private bool _isTurn;
        public bool IsTurn { set { _isTurn = value; } get { return _isTurn; } }

        private bool _flyingOn;
        public bool FlyingOn { set { _flyingOn = value; } get { return _flyingOn; } }

        private bool _landingOn;
        public bool LandingOn { set { _landingOn = value; } get { return _landingOn; } }
        
        static bool _isInit;

        private void Awake()
        {
            _dragonGroundCollider = GetComponent<BoxCollider>();

            _stat = GetComponent<DragonStat>();
            _dragonRigidBody = GetComponent<Rigidbody>();

            DragonAttackTrigger []triggers = GetComponentsInChildren<DragonAttackTrigger>();

            foreach (DragonAttackTrigger Trigger in triggers)
            {
                _dragonAttackTriggers.Add(Trigger.TriggerTag, Trigger);
                Trigger.enabled = false;
                Trigger.gameObject.SetActive(false);
            }

            IsAction = false;

        }

        void Start ()
        {
            if (Application.isPlaying)
            {
                _dragonBehaviroTree.Initialize(_dragonBehaviroTree.Root);
                _isInit = true;
            } 
	    }

        public void OnDestroyPart(float _damage)
        {
            float FinalDamage = (_damage); /*+ Stat.MaxHP * Stat.ObejctHitDamagePercent;*/
            BlackBoard.Instance.IsDestroyPart = true;
            Hit(FinalDamage);
        }

        public void SetActionTask(ActionTask newActionTask)
        {
            if (_currentActionTask)
            {
                if (_currentActionTask.IsRunning && _currentActionTask.NodeState != TASKSTATE.FAULURE)
                {
                    _currentActionTask.OnEnd();
                }
            }
            _currentActionTask = newActionTask;
            _currentActionTask.OnStart();
        }

        public void Hit(float damage)
        {
            Stat.HP -= damage;
            Debug.Log("Dragon Hit : " + damage);
        }

        public void AttackOn(DragonAttackTriggers attackTrigger)
        {
            if (_isInit)
                AttackOff();

            _currentAttackTrigger = attackTrigger;
            _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(true);
            _dragonAttackTriggers[_currentAttackTrigger].enabled = true;
        }

        public void AttackOff()
        {
            _dragonAttackTriggers[_currentAttackTrigger].enabled = false;
            _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(false);
        }

        

        private void Update()
        {
            if (!_dragonBehaviroTree.Root.Run())
            {
                BlackBoard.Instance.IsAirSpearAttack
                    (_rayTransfrom, 150.0f, _dragonAvoidLayers);
            }
            else
            {
                //죽었을 시...
                AttackOff();
                Debug.Log("AI Dead");
            }
        }

        private void OnDrawGizmos ()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(this.transform.position, 150.0f);

            Gizmos.color = Color.blue;
            Vector3 rayForward = _rayTransfrom.forward;
            rayForward.y = 0.0f;
            Gizmos.DrawRay(_rayTransfrom.position, rayForward * 150.0f);

        }

    }
}