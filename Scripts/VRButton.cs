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
            
            Gun leftGun = _leftHend.currentAttachedObject.GetComponent<Gun>();
            Gun rightGun = _leftHend.currentAttachedObject.GetComponent<Gun>();

            if (leftGun && rightGun)
            {
                _leftTransform = leftGun.transform;
                _rightTransform = rightGun.transform;
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
        OnButtonClick(_leftTransform, _leftHend, _rayDistance);
        OnButtonClick(_rightTransform, _rightHend, _rayDistance);
    }


    private void OnButtonClick (Transform handTrans, PlayerHand hand, float distance)
    {

        bool _isButtonPoniter =
                Physics.Raycast(handTrans.position, handTrans.forward, distance, _uiLayer);

        if (_isButtonPoniter)
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
                        Physics.Raycast(_rightTransform.position, _rightTransform.forward, distance, _uiLayer);
                    if (!_isButtonPoniter)
                    {
                        _buttonImage.sprite = _buttonNormalSprite;
                        _isOver = false;
                        return;
                    }

                    if (_rightHend.GetTriggerButtonDown())
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
                    _isButtonPoniter = 
                        Physics.Raycast(_leftTransform.position, _leftTransform.forward, distance, _uiLayer);
                    if (!_isButtonPoniter)
                    {
                        _buttonImage.sprite = _buttonNormalSprite;
                        _isOver = false;
                        return;
                    }

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
        }
    }

}
