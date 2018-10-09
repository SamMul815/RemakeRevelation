using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonController {

    public class DragonStat : MonoBehaviour {

        [Header("Dragon Move Speed")]

        [SerializeField] private float _walkSpeed = 10.0f;
        public float WalkSpeed { get { return _walkSpeed; } }

        [SerializeField] private float _turnSpeed = 360.0f;
        public float TurnSpeed { get { return _turnSpeed; } }

        [SerializeField] private float _flyingSpeed = 100.0f;
        public float FlyingSpeed { get { return _flyingSpeed; } }

        [SerializeField]
        private float _maxSpeed;
        public float MaxSpeed { get { return _maxSpeed; } }

        [SerializeField]
        private float _accSpeed;
        public float AccSpeed { get { return _accSpeed; } }


        [SerializeField] private float _dashSpeed;
        public float DashSpeed { get { return _dashSpeed; } }

        [SerializeField] private float _rushSpeed;
        public float RushSpeed { get { return _rushSpeed; } }

        [SerializeField] private float _landingSpeed;
        public float LandingSpeed { get { return _landingSpeed; } }

        [Space]
        [Header("Dragon HP")]

        [SerializeField] private float _maxHP;
        public float MaxHP { set { _maxHP = value; } get { return _maxHP; } }

        [SerializeField] private float _hp;
        public float HP { set { _hp = value; } get { return _hp; } }

        private float _meteoHP;
        public float MeteoHP { get { return _meteoHP; } }

        private float _meteoSaveHP;
        public float MeteoSaveHP { set { _meteoSaveHP = value; } get { return _meteoSaveHP; } }

        private float _airSpearHP;
        public float AirSpearHP { set { _airSpearHP = value; } get { return _airSpearHP; } }

        private float _airSpearSaveHP;
        public float AirSpearSaveHP { set { _airSpearSaveHP = value; } get { return _airSpearSaveHP; } }

        private float _firstPhaseHP;
        public float FirstPhaseHP { get { return _firstPhaseHP; } }

        private float _secondPhaseHP;
        public float SecondPhaseHP { get { return _secondPhaseHP; } }
        private float _thirdPhaseHP;
        public float ThirdPhaseHP { get { return _thirdPhaseHP; } }


        [Space]
        [Header("Dragon TakeDamage")]

        [SerializeField] private float _saveTakeDamage;
        public float SaveTakeDamage { set { _saveTakeDamage = value; } get { return _saveTakeDamage; } }

        [Space]
        [Header("Dragon Pattern Distance")]

        [SerializeField] private float _dashMoveLimitDistance;
        public float DashMoveLimitDistance { get { return _dashMoveLimitDistance; } }

        [SerializeField] private float _rushMoveLimitDistance;
        public float RushMoveLimitDistance { get { return _rushMoveLimitDistance; } }


        private Vector3 _dashMovePosition;
        public Vector3 DashMovePosition { set { _dashMovePosition = value; } get { return _dashMovePosition; } }

        [Space]
        [Header("Dragon Part Destroy Damage")]

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _destroyPartDamagePercent;
        public float DestroyPartDamagePercent { get { return _destroyPartDamagePercent; } }

        [Space]
        [Header("Dragon Phase HPBar Precent")]

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _firstPhaseHpPercent;
        public float FirstPhaseHpPercent { set { _firstPhaseHpPercent = value; } get { return _firstPhaseHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _secondPhaseHpPercent;
        public float SecondPhaseHpPercent { set { _secondPhaseHpPercent = value; } get { return _secondPhaseHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _thirdPhaseHpPercent;
        public float ThirdPhaseHpPercent { set { _thirdPhaseHpPercent = value; } get { return _thirdPhaseHpPercent; } }

        [Space]
        [Header("Dragon State HPBar Precent")]

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _secondPhaseAirSpearHPPrecent;
        public float SecondPhaseAirSpearHPPrecent { get { return _secondPhaseAirSpearHPPrecent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _thirdPhaseAirSpearHPPrecent;
        public float ThirdPhaseAirSpearHPPrecent { get { return _thirdPhaseAirSpearHPPrecent; } }

        private float _airSpearHPPercent;
        public float AirSpearHPPercent { get { return _airSpearHPPercent; } }


        [Range(0.0f, 1.0f)]
        [SerializeField] private float _meteofHpPercent;
        public float MeteofHpPercent { get { return _meteofHpPercent; } }

        //[Range(0.0f, 1.0f)]
        //[SerializeField] private float _groggyHpPercent;
        //public float GroggyHpPercent { set { _groggyHpPercent = value; } get { return _groggyHpPercent; } }

        //[Range(0.0f, 1.0f)]
        //[SerializeField] private float _damageReceiveHpPercent;
        //public float DamageReceiveHpPejrcent { get { return _damageReceiveHpPercent; } }
        
        public void Awake()
        {
            _airSpearHPPercent = _secondPhaseAirSpearHPPrecent;

            _meteoHP = _maxHP * _meteofHpPercent;
            _airSpearHP = _maxHP * _airSpearHPPercent;

            _airSpearSaveHP = _maxHP;
            _meteoSaveHP = _maxHP;

            _firstPhaseHP = _maxHP * _firstPhaseHpPercent;
            _secondPhaseHP = _maxHP * _secondPhaseHpPercent;
            _thirdPhaseHP = _maxHP * _thirdPhaseHpPercent;

        }

    }
}
