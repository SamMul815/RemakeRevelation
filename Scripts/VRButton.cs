using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButton : MonoBehaviour 
{
    [SerializeField]
    protected Sprite _buttonOverSprite;
    protected Sprite _buttonNormalSprite;

    protected Image _buttonImage;

    protected PlayerHand _leftHend;
    protected PlayerHand _rightHend;

    protected Transform _leftTransform;
    protected Transform _rightTransform;

    [SerializeField]
    protected float _rayDistance;
    
    protected bool _isOver;
    
    protected bool _isButtonClick;

    public delegate void ButtonEventFunc();
    public ButtonEventFunc ButtonEvent;

    private LayerMask _uiLayer;

    // Use this for initialization
    protected virtual void Start ()
    {
        _buttonImage = GetComponent<Image>();
        _buttonNormalSprite = _buttonImage.sprite;
        _uiLayer = 1 << LayerMask.NameToLayer("UI");

        if (Application.isPlaying)
        {
            _leftHend = Player.instance.leftHand;
            _rightHend = Player.instance.rightHand;

            GameObject leftGun = _leftHend.currentAttachedObject;
            GameObject rightGun = _rightHend.currentAttachedObject;

            if (leftGun && rightGun)
            {
                if (leftGun.GetComponent<Gun>() && rightGun.GetComponent<Gun>())
                {
                    _leftTransform = leftGun.GetComponent<Gun>().firePos;
                    _rightTransform = rightGun.GetComponent<Gun>().firePos;
                }
                else
                {
                    _leftTransform = _leftHend.transform;
                    _rightTransform = _rightHend.transform;
                }
            }
            else
            {
                _leftTransform = _leftHend.transform;
                _rightTransform = _rightHend.transform;
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        OnButtonClick(_rightTransform, _rightHend, _rayDistance);
        OnButtonClick(_leftTransform, _leftHend, _rayDistance);
    }


    protected void OnButtonClick (Transform handTrans, PlayerHand hand, float distance)
    {
        RaycastHit hit;
        bool _isButtonPoniter = 
                Physics.Raycast(handTrans.position, handTrans.forward, out hit, distance, _uiLayer);

        if (hit.collider == this.GetComponent<Collider>())
        {
            if (!_isOver)
            {
                if (_buttonOverSprite)
                {
                    _buttonImage.sprite = _buttonOverSprite;
                }
                _isOver = true;
            }
            if (hand.GetTriggerButtonDown())
            {
                if (!_isButtonClick)
                {
                    if (ButtonEvent != null)
                    {
                        ButtonEvent();
                    }
                    _isButtonClick = true;
                }
            }
        }
        else
        {
            if (_isOver)
            {
                if (_leftHend == hand)
                {
                    _isButtonPoniter =
                    Physics.Raycast(handTrans.position, handTrans.forward, out hit, distance, _uiLayer);

                    if (hit.collider != this.GetComponent<Collider>())
                    {
                        Physics.Raycast(_rightTransform.position, _rightTransform.forward, out hit, distance, _uiLayer);

                        if (hit.collider == this.GetComponent<Collider>())
                        {
                            if (_buttonOverSprite)
                            {
                                _buttonImage.sprite = _buttonOverSprite;
                            }

                            _isOver = true;

                            if (_rightHend.GetTriggerButtonDown())
                            {
                                if (!_isButtonClick)
                                {
                                    if (ButtonEvent != null)
                                        ButtonEvent();
                                    _isButtonClick = true;
                                }
                            }
                        }
                        else
                        {
                            _buttonImage.sprite = _buttonNormalSprite;
                            _isOver = false;
                        }
                    }
                    else
                    {
                        if (_leftHend.GetTriggerButtonDown())
                        {
                            if (!_isButtonClick)
                            {
                                if (ButtonEvent != null)
                                    ButtonEvent();
                                _isButtonClick = true;
                            }
                        }
                    }
                }
                //else
                //{
                //    _isButtonPoniter =
                //        Physics.Raycast(handTrans.position, handTrans.forward, out hit, distance, _uiLayer);

                //    if (hit.collider != this.GetComponent<Collider>())
                //    {
                //        _buttonImage.sprite = _buttonNormalSprite;
                //        _isOver = false;
                //    }
                //    else
                //    {
                //        if (_leftHend.GetTriggerButtonDown())
                //        {
                //            if (!_isButtonClick)
                //            {
                //                if (ButtonEvent != null)
                //                    ButtonEvent();
                //                _isButtonClick = true;
                //            }
                //        }

                //    }
                //}
            }
        }
    }

}
