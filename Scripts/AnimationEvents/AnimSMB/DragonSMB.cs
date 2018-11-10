using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonSMB : BaseSMB{

    float aniTime = 0.0f;

    public override void Awake()
    {
        base.Awake();
    }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        aniTime = 0.0f;
        maxAniTime = 1.0f;
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

            aniTime = Mathf.Round((stateInfo.normalizedTime) * 1000.0f) / 1000f;

            if (eventIndex < onStateTimeEventListener.Count)
            {
                if (aniTime >= StateTimeEvent[eventIndex].RunTime + (maxAniTime - 1))
                {
                    bool isRun = IsRunning[eventIndex];

                    if (!isRun)
                    {
                        onStateTimeEventListener[eventIndex](StateTimeEvent[eventIndex]);
                        isRunning[eventIndex] = true;
                        eventIndex++;
                    }
                }
            }
            else if(IsLoop)
            {
                if (aniTime >= maxAniTime)
                {
                    maxAniTime += 1.0f;
                    eventIndex = 0;
                    InitRunning(isRunning);
                }
            }

        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        aniTime = 0.0f;
        maxAniTime = 1.0f;
    }

}
