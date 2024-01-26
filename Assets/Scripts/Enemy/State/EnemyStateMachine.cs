using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStateMachine : StateMachine
{
    //Components
    [field: SerializeField] public NavMeshAgent NaveMeshAgent { get; private set; }
    
    // Constants
    [field: SerializeField] public float RangeDistance { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }
    
    //Calculated
    [field: SerializeField] public Transform EndPoint { get; set; }
    [field: SerializeField] public Transform StartPoint { get; set; }
    [field: SerializeField] public States StartState { get; set; }
    
    //Common
    public GameObject Player { get; private set; }
    public Transform Camera { get; private set; }
    
    //Cheese
    public States OldState;

    private void Awake()
    {
        transform.position = StartPoint.position;
        transform.rotation = StartPoint.rotation;
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        this.Camera = UnityEngine.Camera.main.transform;
        switch(StartState)
        {
            case States.IdleState:
                SwitchState(new EnemyIdleState(this));
                OldState = States.IdleState;
                break;
            case States.AttackState:
                SwitchState(new EnemyAttackState(this));
                OldState = States.IdleState;
                break;
            case States.TrapState:
                SwitchState(new EnemyTrapState(this));
                OldState = States.IdleState;
                break;
        }
    }
}

public enum States
{
    IdleState = 1,
    AttackState = 2,
    TrapState = 3,
}
