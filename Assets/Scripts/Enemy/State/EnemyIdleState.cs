using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private bool Switcher;
    
    private void Start()
    {
        Switcher = true;
    }

    public override void Enter()
    {
        _stateMachine.Animator.Play(StandardHash);
    }

    public override void Tick(float deltaTime)
    {
        if (IsInRange(_stateMachine.Player.transform.position, _stateMachine.FocusRangeDistance))
            _stateMachine.SwitchState(new EnemyFocusState(_stateMachine));

        if (IsInRange(_stateMachine.EndPoint.position, 0.1f))
            Switcher = !Switcher;
        
        CalculateMovement(deltaTime, _stateMachine.MovementSpeed, Switcher ? _stateMachine.EndPoint.position : _stateMachine.StartPoint.position);
    }

    public override void Exit()
    {
        // _stateMachine.StandardHash = Random.Range(_stateMachine.StandardHash, _stateMachine.StandardHash + 4);
        _stateMachine.OldState = States.IdleState;
    }
}
