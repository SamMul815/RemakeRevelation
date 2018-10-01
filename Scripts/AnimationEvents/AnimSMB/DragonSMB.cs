using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonSMB : BaseSMB{

    public override void Awake()
    {
        base.Awake();
    }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!beginExit)
        {
            if (animator.IsInTransition(layerIndex))
            {
                if (!waitingToBegin)
                {
                    if (onStateExitEventListener != null)
                    {
                        onStateExitEventListener(StateExitEvnData);
                    }
                    beginExit = true;
                }

            }
            else if (waitingToBegin)
            {
                if (onStateEnterEventListener != null)
                {
                    onStateEnterEventListener(StateEnterEvnData);
                }
                waitingToBegin = false;
            }
        }

        if (onStateTimeEventListener != null)
        {
            for (int i = 0; i < onStateTimeEventListener.Count; i++)
            {
                float aniTime = Mathf.Round((stateInfo.normalizedTime) * 1000.0f) / 1000f;

                if (aniTime >= StateTimeEvent[i].RunTime)
                {
                    bool isRun = IsRunning[i];

                    if (!isRun)
                    {
                        onStateTimeEventListener[i](StateTimeEvent[i]);
                        isRunning[i] = true;
                    }
                }
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

}
