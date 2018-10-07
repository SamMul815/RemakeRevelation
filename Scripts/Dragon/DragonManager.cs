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
    AirSpear
}


namespace DragonController
{
    [RequireComponent(typeof(MovementManager))]
    [RequireComponent(typeof(DragonStat))]
    [RequireComponent(typeof(Rigidbody))]
    public class DragonManager : Singleton<DragonManager> {

        [SerializeField]
        private BehaviorTree _dragonBehaviroTree;
        public BehaviorTree DragonBehaviroTree { get { return _dragonBehaviroTree; } }

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

        private static bool _isAction;
        public static bool IsAction { set { _isAction = value; } get { return _isAction; } }

        private static bool _isTurn;
        public static bool IsTurn { set { _isTurn = value; } get { return _isTurn; } }

        private static bool _flyingOn;
        public static bool FlyingOn { set { _flyingOn = value; } get { return _flyingOn; } }

        private static bool _landingOn;
        public static bool LandingOn { set { _landingOn = value; } get { return _landingOn; } }

        private static  Transform _player;
        public static Transform Player { get { return _player; } }

        IEnumerator _dragonAiCor;

        static bool _isInit;

        private void Awake()
        {
            _dragonGroundCollider = GetComponent<BoxCollider>();

            _stat = GetComponent<DragonStat>();

            _dragonRigidBody = GetComponent<Rigidbody>();

            _player = GameObject.FindWithTag("Player").transform;

            DragonAttackTrigger []triggers = GetComponentsInChildren<DragonAttackTrigger>();

            foreach (DragonAttackTrigger Trigger in triggers)
            {
                _dragonAttackTriggers.Add(Trigger.TriggerTag, Trigger);
                Trigger.enabled = false;
                Trigger.gameObject.SetActive(false);
            }

            IsAction = false;

            _dragonBehaviroTree.Initialize(_dragonBehaviroTree.Root);
            _dragonAiCor = StartDragonAI();

        }

        void Start ()
        {
            if (Application.isPlaying)
            {
                CoroutineManager.DoCoroutine(_dragonAiCor);
                _isInit = true;
            } 
	    }


        public void OnDestroyPart(float _damage)
        {
            float FinalDamage = _damage * Stat.DestroyPartDamagePercent;
            BlackBoard.Instance.IsDestroyPart = true;
            Hit(FinalDamage);
        }

        public static void SetActionTask(ActionTask newActionTask)
        {
            if (_currentActionTask)
            {
                if (_currentActionTask.IsRunning && 
                    _currentActionTask.NodeState != TASKSTATE.FAULURE)
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

        public void AttackOn(DragonAttackTriggers attackTag)
        {
            if (_isInit)
            {
                _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(false);
                _dragonAttackTriggers[_currentAttackTrigger].enabled = false;
            }
            _currentAttackTrigger = attackTag;
            _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(true);
            _dragonAttackTriggers[_currentAttackTrigger].enabled = true;
        }

        public void AttackOff()
        {
            _dragonAttackTriggers[_currentAttackTrigger].enabled = false;
            _dragonAttackTriggers[_currentAttackTrigger].gameObject.SetActive(false);

        }

        IEnumerator StartDragonAI()
        {
            while (!_dragonBehaviroTree.Root.Run())
            {
                yield return CoroutineManager.FiexdUpdate;
            }
        }
    }
}