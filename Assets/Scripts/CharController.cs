using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Coded by Nicholas Perell
/// 
/// Movement/Physics Code is based on the player controller 
/// Perell made in High Tide (https://chortling-chickadee.itch.io/high-tide)
/// to allow Perell to focus on the changing states.
/// 
/// Not TOO concerned about the script length right now 
/// (Celeste's Player.cs is 5471 lines), but I'm gonna try 
/// my best to make it something that could be easy to 
/// elaborate on.
/// </summary>

[System.Serializable]
public class PassableCharacterData
{
    [Header("For State Reference")]
    public Rigidbody2D rigidBody;
    public Vector2 desiredMovement;
    public float timeInState;

    [Header("Charging Stats")]
    public float timeToReachCharge;
    public float timeCharging;
    public bool currentlyCharging;

    [Header("Base Movement Stats")]
    public float groundRunForce = 40.0f;
    public float groundMaxRunSpeed = 5.5f;
    public float groundLinearDrag = .99f;
    public float groundGravityScale = 2.0f;
    public float jumpForce = 870.0f;
    public float jumpCutForce = 10.0f;

    [Header("Physics Checks")]
    public float checkRadius = 0.1f;
    [Space]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool onGround;
    [Space]
    public Transform[] leftWallCheck;
    public Transform[] rightWallCheck;
    public LayerMask wallLayer;
    public bool walledLeft = false;
    public bool walledRight = false;

    [HideInInspector]
    public CharState charState;

    public void SetState(CharacterStateID stateID)
    {
        switch (stateID)
        {
            case CharacterStateID.IDLE_STATE:
                charState = new IdleState();
                break;
            case CharacterStateID.WALK_STATE:
                break;
            case CharacterStateID.RUN_STATE:
                break;
            case CharacterStateID.DUCK_STATE:
                break;
            case CharacterStateID.JUMP_STATE:
                break;
            case CharacterStateID.LAND_STATE:
                break;
            case CharacterStateID.ROLLING_STATE:
                break;
            case CharacterStateID.AIR_ATTACK_STATE:
                break;
            case CharacterStateID.ATTACK_1_STATE:
                charState = new Attack1State();
                break;
            case CharacterStateID.ATTACK_2_STATE:
                break;
            case CharacterStateID.ATTACK_3_STATE:
                break;
        }
    }
}

public class CharController : MonoBehaviour
{
    [SerializeField] PassableCharacterData data;

    PlayerControls inputs;

    void Awake()
    {
        data.charState = new IdleState();
        inputs = new PlayerControls();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.PlayerCharacter.Attack.performed += ctx => HandleInput(ref ctx);
        inputs.PlayerCharacter.Charge.performed += ctx => HandleInput(ref ctx);
        inputs.PlayerCharacter.ReleaseCharge.performed += ctx => HandleInput(ref ctx);
        inputs.PlayerCharacter.MovementKeys.performed += ctx => HandleInput(ref ctx);
        inputs.PlayerCharacter.Roll.performed += ctx => HandleInput(ref ctx);
        inputs.PlayerCharacter.Jump.performed += ctx => HandleInput(ref ctx);
    }

    void Update()
    {
        data.charState.Update(ref data);
    }

    void FixedUpdate()
    {
        data.charState.FixedUpdate(ref data);
    }

    void HandleInput(ref InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.action.name);
        data.charState.HandleInput(ref data, ref ctx);
    }

}