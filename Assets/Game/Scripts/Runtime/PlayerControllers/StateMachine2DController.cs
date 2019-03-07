using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;
using Wokarol.PlayerControllers;
using Wokarol.PlayerInput;
using Wokarol.StateSystem;

public class StateMachine2DController : BoxController
{
    [Space]
    [SerializeField] InputData input = null;
    [Space]
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float timeToJumpApex = .4f;
    [SerializeField] float speed = 10;
    [SerializeField] float fallingGravityMultiplier = 1.5f;

    public DebugBlock DebugBlock { get; } = new DebugBlock("Controller AI");

    float gravity;
    float jumpVelocity;

    float coyoteTimer = 0;
    const float coyoteTime = 0.07f;

    bool jumpLastPressed;

    StateMachine _controllerBrain;

    ReferencedVariable<Vector2> velocity = new ReferencedVariable<Vector2>(Vector2.zero);
    
    protected override void OnValidate() {
        base.OnValidate();
        RecalculateHiddenValues();
    }

    void RecalculateHiddenValues() {
        gravity = -(2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    protected override void Start() {
        base.Start();
        RecalculateHiddenValues();

        // States
        var falling = new FallingState("Falling", velocity, gravity, () => speed * input.Horizontal);
        var running = new RunningState("Running", velocity, () => speed * input.Horizontal);
        var jumping = new FallingState("Jumping", velocity, gravity, () => speed * input.Horizontal);

        // OnExit/Enter
        falling.OnExit += () => SetVelocityY();

        running.OnExit += () => SetVelocityY();
        running.OnExit += () => coyoteTimer = coyoteTime;

        jumping.OnEnter += () => SetVelocityY(jumpVelocity);

        // States
        falling.AddTransition(() => SurfaceClose(downHit), running);
        falling.AddTransition(() => input.Jump && !jumpLastPressed && coyoteTimer > 0, jumping, 
            () => { Debug.Log("COYOTE TIME!!!"); jumpLastPressed = true; });

        running.AddTransition(() => !SurfaceClose(downHit), falling);
        running.AddTransition(() => input.Jump && !jumpLastPressed, jumping, () => jumpLastPressed = true);

        jumping.AddTransition(() => velocity.Value.y <= 0, falling);
        jumping.AddTransition(() => SurfaceClose(upHit), falling, () => SetVelocityY(gravity * 0.06f));

        // Initialization
        _controllerBrain = new StateMachine(falling, DebugBlock);
    }

    private void SetVelocityY(float y = 0) {
        var v = velocity.Value;
        v.y = y;
        velocity.Value = v;
    }

    bool SurfaceClose(SurfaceCheckerHit hit) {
        return hit.Hitted && hit.ClosestDistance < (skinWidth + 0.05f);
    }

    private void Update() {
        coyoteTimer -= Time.deltaTime;
        if (!input.Jump) jumpLastPressed = false;

        GetSamples(velocity);

        _controllerBrain.Tick();

        GetSamples(velocity);
        var resV = transform.Move(velocity.Value * Time.deltaTime, skinWidth, upHit, downHit, leftHit, rightHit);

        Debug.DrawRay(transform.position, velocity.Value / 10, Color.red);
        Debug.DrawRay(transform.position, resV / Time.deltaTime, Color.green);
    }
}
