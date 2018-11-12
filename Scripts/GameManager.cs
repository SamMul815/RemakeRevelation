using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class GameManager : Singleton<GameManager>
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

    private void Awake ()
    {
        _dragon = DragonManager.Instance;
        _player = Player.instance;
        _ui = UIManager.Instance;
        _utility = UtilityManager.Instance;
        _effect = EffectManager.Instance;
        _bullet = BulletManager.Instance;
    }
	
    public void GameOver (Vector3 createUIPos, Vector3 dir)
    {
        if (_player.playerStat.GetCurrentHP() <= 0.0f)
        {
            _ui.CreateGameOverUI(createUIPos, dir)
        }
    }

    public void GameClear ()
    {
        if (_dragon.Stat.HP <= 0.0f)
        {
            Vector3 pos = _dragon.transform.position + new Vector3(0.0f, 10.0f, 0.0f);
            _ui.CreateGameClearUI(pos, -_dragon.transform.forward);
        }
    }
	
    // Update is called once per frame
	void Update ()
    {
		
	}

}
