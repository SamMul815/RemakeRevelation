using UnityEngine;
using UnityEngine.UI;

public class VRButton : MonoBehaviour 
{
    [SerializeField]
    protected Sprite _buttonOverSprite;
    protected Sprite _buttonSprite;

    protected Image _buttonImage;

    [SerializeField]
    protected PlayerHand _hand;

    [SerializeField]
    protected float _distance;
    
    protected bool _isOver;

    public delegate void ButtonEventFunc();
    public ButtonEventFunc ButtonEvent;

    // Use this for initialization
    protected virtual void Start ()
    {
        _buttonImage = GetComponent<Image>();
        _buttonSprite = _buttonImage.sprite;
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        OnButtonClick(_hand, _distance);
    }


    private void OnButtonClick (PlayerHand hand, float distance)
    {
        LayerMask uiLayer = 1 << LayerMask.NameToLayer("UI");
        bool isButtonPointer = Physics.Raycast(hand.transform.position, hand.transform.forward, distance, uiLayer);

        if (isButtonPointer)
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
            if(_isOver)
            {
                _buttonImage.sprite = _buttonSprite;
                _isOver = false;
            }
        }
    }

    private void OnDrawGizmos ()
    {
        Gizmos.DrawRay(_hand.transform.position, _hand.transform.forward * _distance);
    }

}
