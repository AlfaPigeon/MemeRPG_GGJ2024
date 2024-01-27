using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    [field: SerializeField] private EnemyStateMachine EnemyStateMachine { get; set; }

    private string _hash;

    public void HashAssignment(string hash)
    {
        _hash = hash;
    }
    
    public void IntBaseIncrease(string hash)
    {
        this.EnemyStateMachine.Animator.SetInteger(hash, this.EnemyStateMachine.Animator.GetInteger(hash) + 1);
    }

    public void IntSet(int set)
    {
        this.EnemyStateMachine.Animator.SetInteger(_hash, set);
    }

    public void IntPolyIncrease(int range)
    {
        int val = Random.Range(this.EnemyStateMachine.Animator.GetInteger(_hash) + 1,
            this.EnemyStateMachine.Animator.GetInteger(_hash) + range);
        // Debug.Log("inc" + val);
        this.EnemyStateMachine.Animator.SetInteger(_hash, val);
    }
    
    public void IntPolyDecrease(int range)
    {
        int val = Random.Range(this.EnemyStateMachine.Animator.GetInteger(_hash) - range,
            this.EnemyStateMachine.Animator.GetInteger(_hash));
        // Debug.Log("dec" + val);
        this.EnemyStateMachine.Animator.SetInteger(_hash, val);
    }

    public void EnemyIntUp()
    {
        this.EnemyStateMachine.Animator.SetInteger("AttackState",Random.Range(5, 9));
    }

    public void EnemyIntDown()
    {
        this.EnemyStateMachine.Animator.SetInteger("AttackState",Random.Range(1, 5));
    }
}