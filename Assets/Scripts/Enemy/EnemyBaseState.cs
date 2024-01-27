using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EnemyBaseState: State
{
    protected EnemyStateMachine _stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected readonly int StandardHash = Animator.StringToHash("StandardStateStart");
    protected readonly int FocusHash = Animator.StringToHash("FocusStateStart");

    protected readonly int WalkPercentHash = Animator.StringToHash("WalkPercent");
    
    protected bool IsInRange(Vector3 target, float distance)
    {
        float rangeDistance = Vector3.Distance(target, _stateMachine.transform.position);
        if (rangeDistance <= distance)
            return true;
        return false;
    }

    protected void CalculateMovement(float deltaTime, float speed, Vector3 target = default)
    {
        _stateMachine.NaveMeshAgent.speed = speed;

        if (target == Vector3.zero)
        {
            _stateMachine.NaveMeshAgent.SetDestination(_stateMachine.EndPoint.position);
            return;
        }

        _stateMachine.NaveMeshAgent.SetDestination(target);
    }

    protected void CalculateRotation(float deltaTime, float speed, Vector3 target = default)
    {
        Quaternion desiredRotation =
            Quaternion.LookRotation(target == Vector3.zero ? _stateMachine.NaveMeshAgent.velocity.normalized : target - _stateMachine.transform.position);
        _stateMachine.transform.rotation =
            Quaternion.Slerp(_stateMachine.transform.rotation, desiredRotation, deltaTime * speed);
    }
    protected void CalculateRotation(float deltaTime, float speed, out float angleDiffer, out bool isNegative, Vector3 target = default)
    {
        Quaternion desiredRotation =
            Quaternion.LookRotation(target == Vector3.zero ? _stateMachine.NaveMeshAgent.velocity.normalized : target - _stateMachine.transform.position);
        _stateMachine.transform.rotation =
            Quaternion.Slerp(_stateMachine.transform.rotation, desiredRotation, deltaTime * speed);

        angleDiffer = Quaternion.Angle(_stateMachine.transform.rotation, desiredRotation);
        isNegative = Vector3.Cross(_stateMachine.transform.rotation * Vector3.forward, desiredRotation * Vector3.forward).y < 0;
    }
}
