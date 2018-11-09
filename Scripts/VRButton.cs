using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButton : MonoBehaviour 
{
    [SerializeField]
    protected Sprite _buttonOverSprite;
    protected Sprite _buttonSprite;

    protected Image _buttonImage;

    [SerializeField]
    //protected List PlayerHand _hand;
    protected List<PlayerHand> _hand = new List<PlayerHand>();

    [SerializeField]
    protected float _rayDistance;
    
    protected bool _isOver;
    protected bool _isButtonPoniter;

    public delegate void ButtonEventFunc();
    public ButtonEventFunc ButtonEvent;

    // Use this for initialization
    protected virtual void Awake ()
    {
        _buttonImage = GetComponent<Image>();
        _buttonSprite = _buttonImage.sprite;
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        for (int i = 0; i < _hand.Count; i++)
        {
            OnButtonClick(_hand[i], _rayDistance);
        }
    }


    private void OnButtonClick (PlayerHand hand, float distance)
    {
        LayerMask uiLayer = 1 << LayerMask.NameToLayer("UI");

        if (!_isButtonPoniter)
        {
            _isButtonPoniter =
                Physics.Raycast(hand.transform.position, hand.transform.forward, distance, uiLayer);
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
                    if (ButtonEvent != null)
                    {
                        ButtonEvent();
                    }
                }
            }
            else
            {
                if (_isOver)
                {
                    _buttonImage.sprite = _buttonSprite;
                    _isOver = false;
                }
            }
        }
        else
        {
            _isButtonPoniter = 
                Physics.Raycast(hand.transform.position, hand.transform.forward, distance, uiLayer);
        }

        //else
        //{
        //    if(_isOver)
        //    {
        //        _buttonImage.sprite = _buttonSprite;
        //        _isOver = false;
        //    }
        //    if(!_isOver)
        //    {
        //        if (_buttonOverSprite)
        //        {
        //            _buttonImage.sprite = _buttonOverSprite;
        //        }
        //        _isOver = true;
        //    }

        //    if(hand.GetTriggerButtonDown())
        //    {
        //        if(ButtonEvent != null)
        //        {
        //            ButtonEvent();
        //        }
        //    }
        //}


        //bool isButtonPoniter =
        //    Physics.Raycast(hand.transform.position, hand.transform.forward, distance, uiLayer);
        //if (isButtonPoniter)
        //{
        //    if (!_isOver)
        //    {
        //        if (_buttonOverSprite)
        //        {
        //            _buttonImage.sprite = _buttonOverSprite;
        //        }
        //        _isOver = true;
        //    }
        //    if (hand.GetTriggerButtonDown())
        //    {
        //        if (ButtonEvent != null)
        //        {
        //            ButtonEvent();
        //        }
        //    }
        //}
        //else
        //{
        //    if(_isOver)
        //    {
        //        _buttonImage.sprite = _buttonSprite;
        //        _isOver = false;
        //    }
        //}
    }

    private void OnDrawGizmos ()
    {
        for (int i = 0; i < _hand.Count; i++)
        {
            Gizmos.DrawRay(_hand [i].transform.position, _hand [i].transform.forward * _rayDistance);
        }
    }

}
