using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject {

    [Tooltip("List of Actions taken in this state.")]
    [SerializeField]
    private Action[] actionsInState;

    [Tooltip("List of Transitions in this state.")]
    [SerializeField]
    private Transition[] transitionsInState;

    public void UpdateState(EnemyManager enemy)
    {
        DoActions(enemy);
        CheckTransitions(enemy);
    }

    private void DoActions(EnemyManager enemy)
    {
        foreach (Action action in actionsInState)
            action.Act(enemy);
    }

    private void CheckTransitions(EnemyManager enemy)
    {
        foreach(Transition transition in transitionsInState)
        {
            bool decisionSucceeded = transition.decision.Decide(enemy);

            if (decisionSucceeded)
                enemy.TransitionToState(transition.trueState);
            else
                enemy.TransitionToState(transition.falseState);
        }        
    }
}
