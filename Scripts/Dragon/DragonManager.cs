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

        [SerializeField]
        private Transform _paw;

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
            _dragonBehaviroTree.Initialize(_dragonBehaviroTree.Root);

        }

        void Start ()
        {
            if (Application.isPlaying)
            {
                _isInit = true;
            } 
	    }


        public void OnDestroyPart(float _damage)
        {
            float FinalDamage = _damage * Stat.DestroyPartDamagePercent;
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
            {
                _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(false);
                _dragonAttackTriggers[_currentAttackTrigger].enabled = false;
            }
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
            if (!_dragonBehaviroTree.Root.Run() && _stat.HP > 0.0f)
                return;
            else
            {
                //죽었을 시...
                Debug.Log("Dead");
            }
        }

        private void OnDrawGizmos()
        {
            float MaxDistance = 25.0f;

            RaycastHit hit;
            bool isHit = Physics.SphereCast(
                _paw.position, transform.lossyScale.x / 2, 
                transform.forward, out hit, MaxDistance, _dragonAvoidLayers);

            Gizmos.color = Color.blue;
            if (isHit)
            {
                Gizmos.DrawRay(_paw.position, transform.forward * hit.distance);
                Gizmos.DrawWireSphere(_paw.position + transform.forward * hit.distance, transform.lossyScale.x / 2);
                Debug.Log("Ground");
            }
            else
            {
                Gizmos.DrawRay(_paw.position, transform.forward * MaxDistance);
            }
        }

    }
}