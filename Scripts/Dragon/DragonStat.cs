﻿using System.Collections;
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

        [Space]
        [Header("Dragon HP")]

        [SerializeField] private float _maxHP;
        public float MaxHP { set { _maxHP = value; } get { return _maxHP; } }

        [SerializeField] private float _hp;
        public float HP { set { _hp = value; } get { return _hp; } }

        [SerializeField] private float _meteoHP;
        public float MeteoHP { get { return _meteoHP; } }

        private float _meteoSaveHP;
        public float MeteoSaveHP { set { _meteoSaveHP = value; } get { return _meteoSaveHP; } }

        [SerializeField] private float _descentHP;
        public float DescentHP { get { return _descentHP; } }

        private float _descentSaveHP;
        public float DescentSaveHP { set { _descentSaveHP = value; } get { return _descentSaveHP; } }


        [Space]
        [Header("Dragon TakeDamage")]

        [SerializeField] private float _saveTakeDamage;
        public float SaveTakeDamage { set { _saveTakeDamage = value; } get { return _saveTakeDamage; } }

        [Space]
        [Header("Dragon Pattern Distance")]

        [SerializeField] private float _dashMoveLimitDistance;
        public float DashMoveLimitDistance { get { return _dashMoveLimitDistance; } }

        private Vector3 _dashMovePosition;
        public Vector3 DahsMovePosition { set { _dashMovePosition = value; } get { return _dashMovePosition; } }

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
        [SerializeField] private float _descentHpPercent;
        public float DescentHpPercent { get { return _descentHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _meteofHpPercent;
        public float MeteofHpPercent { get { return _meteofHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _groggyHpPercent;
        public float GroggyHpPercent { set { _groggyHpPercent = value; } get { return _groggyHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _damageReceiveHpPercent;
        public float DamageReceiveHpPejrcent { get { return _damageReceiveHpPercent; } }
        
        public void Awake()
        {
            _meteoHP = _maxHP * _meteofHpPercent;
            _descentHP = _maxHP * _descentHpPercent;

            _descentSaveHP = _maxHP;
            _meteoSaveHP = _maxHP;

            _hp = _maxHP;

        }

    }
}
