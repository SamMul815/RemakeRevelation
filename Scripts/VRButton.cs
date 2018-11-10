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
    }

    protected void Start ()
    {
        if (Application.isPlaying)
        {
            _leftHend = Player.instance.leftHand;
            _rightHend = Player.instance.rightHand;
        }
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        OnButtonClick(_leftHend, _rayDistance);
        OnButtonClick(_rightHend, _rayDistance);
    }


    private void OnButtonClick (PlayerHand hand, float distance)
    {

        bool _isButtonPoniter =
                Physics.Raycast(hand.transform.position, hand.transform.forward, distance, _uiLayer);

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
                    PlayerHand otherHand = _rightHend;
                    _isButtonPoniter = Physics.Raycast(otherHand.transform.position, otherHand.transform.forward, distance, _uiLayer);
                    if (!_isButtonPoniter)
                    {
                        _buttonImage.sprite = _buttonNormalSprite;
                        _isOver = false;
                        return;
                    }
                    if (otherHand.GetTriggerButtonDown())
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

                    PlayerHand otherHand = _leftHend;
                    _isButtonPoniter = Physics.Raycast(otherHand.transform.position, otherHand.transform.forward, distance, _uiLayer);
                    if (!_isButtonPoniter)
                    {
                        _buttonImage.sprite = _buttonNormalSprite;
                        _isOver = false;
                        return;
                    }
                    if (otherHand.GetTriggerButtonDown())
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
