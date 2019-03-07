using System;
using UnityEngine;
using Wokarol.PlayerInput;
using Wokarol.StateSystem;

internal class RunningState : State
{
    private readonly ReferencedVariable<Vector2> velocity;
    private readonly Func<float> horizontalInputEvaluator;

    public RunningState(string name, ReferencedVariable<Vector2> velocity, Func<float> horizontalInputEvaluator) {
        Name = name;
        this.velocity = velocity;
        this.horizontalInputEvaluator = horizontalInputEvaluator;
    }

    public override bool CanTransitionToSelf => false;

    protected override void EnterProcess(StateMachine stateMachine) {
    }

    protected override void ExitProcess(StateMachine stateMachine) {
    }

    protected override State Process() {
        Vector2 v = velocity;
        v.y = -0.1f;
        v.x = horizontalInputEvaluator();
        velocity.Value = v;
        return null;
    }
}