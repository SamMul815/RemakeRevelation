using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;
using UnityEngine.UI;

public class GameEndManager : Singleton<GameEndManager>
{
    private DragonManager _dragon;
    private Player _player;
    private UIManager _ui;
    private UtilityManager _utility;
    private EffectManager _effect;
    private BulletManager _bullet;

    public DragonManager Dragon { get { return _dragon; } }
    public Player Player { get { return _player; } }
    public UIManager Ui { get { return _ui; } }
    public UtilityManager Utility { get { return _utility; } }
    public EffectManager Effect { get { return _effect; } }
    public BulletManager Bullet { get { return _bullet; } }

    public IEnumerator CreateGameClearCor;
    public IEnumerator CreateGameOverCor;


    private void Awake ()
    {
        _dragon = DragonManager.Instance;
        _player = Player.instance;
        _ui = UIManager.Instance;
        _utility = UtilityManager.Instance;
        _effect = EffectManager.Instance;
        _bullet = BulletManager.Instance;
    }


    public void GameClear ()
    {
        if (_dragon.Stat.HP <= 0.0f)
        {
            Vector3 pos = _player.transform.position + (_player.transform.forward * 10.0f);

            Vector3 forward = pos - _player.transform.position;
            forward.y = 0.0f;

            CreateGameClearCor = CreateGameClearUI(pos, forward, 2.0f);
            CoroutineManager.DoCoroutine(CreateGameClearCor);
        }
    }
    

    public void GameOver ()
    {
        if (_player.playerStat.GetCurrentHP() <= 0.0f)
        {
            Vector3 pos = _player.transform.position + (_player.transform.forward * 0.4f);

            Vector3 forward = pos - _player.transform.position;
            forward.y = 0.0f;

            _player.playerUI.FadeOut(1.5f);

            CreateGameOverCor = CreateGameOverUI(pos, forward, 3.0f);
            CoroutineManager.DoCoroutine(CreateGameOverCor);
        }
    }


    private IEnumerator CreateGameClearUI (Vector3 position, Vector3 dir, float createTime)
    {
        GameObject GameClearUi;
        Quaternion Rot = Quaternion.LookRotation(dir.normalized);
        PoolManager.Instance.PopObject(_ui.GameClearUI, position, Rot, out GameClearUi);
        
        Image [] ChildImg = GameClearUi.GetComponentsInChildren<Image>();
        Color UIColor;

        for (float t = 0; t < createTime; t += Time.deltaTime)
        {
            for (int index = 0; index < ChildImg.Length; index++)
            {
                UIColor = ChildImg [index].color;
                UIColor.a = Mathf.Lerp(0.0f, 1.0f, t / createTime);
                ChildImg [index].color = UIColor;
            }
            yield return CoroutineManager.EndOfFrame;
        }

        for (int index = 0; index < ChildImg.Length; index++)
        {
            UIColor = ChildImg [index].color;
            UIColor.a = 1.0f;
            ChildImg [index].color = UIColor;
            if (ChildImg[index].GetComponent<Collider>())
            {
                ChildImg [index].GetComponent<Collider>().enabled = true;
            }
        }
    }

    private IEnumerator CreateGameOverUI (Vector3 position, Vector3 dir, float createTime)
    {
        GameObject GameOverUI;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        PoolManager.Instance.PopObject(_ui.GameOverUI, position, rot, out GameOverUI);

        Image ChildImg = GameOverUI.GetComponentInChildren<Image>();
        Color ImgColor = ChildImg.color;

        Text [] ChildText = GameOverUI.GetComponentsInChildren<Text>();
        Color TextColor;

        float tt = 0.0f;

        for (float t = 0; t <= createTime * 2f; t += Time.deltaTime)
        {
            for (int index = 0; index < ChildText.Length; index++)
            {
                TextColor = ChildText [index].color;
                TextColor.a = Mathf.Lerp(0.0f, 1.0f, t / (createTime * 0.5f));
                ChildText [index].color = TextColor;
            }
            
            if (t >= createTime * 0.5f)
            {
                ImgColor.a = Mathf.Lerp(0.0f, 1.0f, tt / createTime);
                ChildImg.color = ImgColor;
                tt += Time.deltaTime;
            }

            yield return CoroutineManager.EndOfFrame;
        }

        ImgColor.a = 1.0f;

        if (ChildImg.GetComponent<Collider>())
        {
            ChildImg.GetComponent<BoxCollider>().enabled = true;
        }
        ChildImg.color = ImgColor;

        for (int index = 0; index < ChildText.Length; index++)
        {
            TextColor = ChildText [index].color;
            TextColor.a = 1.0f;
            ChildText [index].color = TextColor;
        }

    }


}
