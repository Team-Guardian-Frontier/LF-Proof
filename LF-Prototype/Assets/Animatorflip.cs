﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Animatorflip : StateMachineBehaviour
{
    private Transform transform;
    private bool flipval;
    private Vector3 TempScale;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get info from object and transform.
        transform = animator.GetComponent<Transform>();
        TempScale = transform.localScale;
        flipval = animator.GetBool("flipdir");

        //Flip object appropriately, once no l  onger transitioning
        if (flipval)
        {
            TempScale.x = -Mathf.Abs(TempScale.x);
        }
        else
        {
            TempScale.x = Mathf.Abs(TempScale.x);
        }
        transform.localScale = TempScale;
    }

    /*
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    */
}