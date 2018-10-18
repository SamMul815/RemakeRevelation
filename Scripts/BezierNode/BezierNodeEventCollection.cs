using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class BezierNodeEventCollection : MonoBehaviour
{
    private MovementManager _manager;

    private void Awake()
    {
        _manager = MovementManager.Instance;
    }

    private void AniGliding()
    {
        DragonAniManager.SwicthAnimation("Dragon_Gliding");
    }

    private void AniFlying()
    {
        DragonAniManager.SwicthAnimation("Dragon_Flying");
    }
}
