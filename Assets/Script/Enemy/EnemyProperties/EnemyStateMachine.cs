using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState CurrentState {  get; private set; }

    public void Initialize(EnemyState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
