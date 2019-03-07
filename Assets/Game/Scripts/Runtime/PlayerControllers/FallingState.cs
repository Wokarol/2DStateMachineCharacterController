using System;
using UnityEngine;
using Wokarol.PlayerInput;
using Wokarol.StateSystem;

internal class FallingState : State
{
    private readonly ReferencedVariable<Vector2> velocity;
    private readonly float gravity;
    private readonly Func<float> horizontalInputEvaluator;

    public FallingState(string name, ReferencedVariable<Vector2> velocity, float gravity, Func<float> horizontalInputEvaluator) {
        Name = name;
        this.velocity = velocity;
        this.gravity = gravity;
        this.horizontalInputEvaluator = horizontalInputEvaluator;
    }

    public override bool CanTransitionToSelf => false;

    protected override void EnterProcess(StateMachine stateMachine) {
    }

    protected override void ExitProcess(StateMachine stateMachine) {
    }

    protected override State Process() {
        Vector2 v = velocity;
        v.y += gravity * Time.deltaTime;
        v.x = horizontalInputEvaluator();
        velocity.Value = v;
        return null;
    }
}