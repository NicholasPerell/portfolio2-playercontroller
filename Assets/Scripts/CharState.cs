using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum CharacterStateID
{
    IDLE_STATE,
    WALK_STATE,
    RUN_STATE,
    DUCK_STATE,
    JUMP_STATE,
    LAND_STATE,
    ROLLING_STATE,
    AIR_ATTACK_STATE,
    ATTACK_1_STATE,
    ATTACK_2_STATE,
    ATTACK_3_STATE
}

public abstract class CharState
{
    ~CharState() { }
    public abstract void HandleInput(ref PassableCharacterData characterData, ref InputAction.CallbackContext ctx);
    public abstract void Update(ref PassableCharacterData characterData);

    public abstract void FixedUpdate(ref PassableCharacterData characterData);
}

public class IdleState : CharState
{

    public override void HandleInput(ref PassableCharacterData characterData, ref InputAction.CallbackContext ctx)
    {
        switch (ctx.action.name)
        {
            case "Attack":
                characterData.SetState(CharacterStateID.ATTACK_1_STATE);
                break;
            case "Jump":
                if (characterData.onGround)
                {
                    characterData.SetState(CharacterStateID.JUMP_STATE);
                }
                break;
            case "Charge":
                characterData.currentlyCharging = true;
                break;
            case "Release Charge":
                ReleaseCharge(ref characterData);
                break;
            case "Roll":
                characterData.SetState(CharacterStateID.ROLLING_STATE);
                break;
            case "Movement Keys":
                characterData.desiredMovement = ctx.ReadValue<Vector2>();
                break;
            default:
                break;
        }
    }

    protected void ReleaseCharge(ref PassableCharacterData characterData)
    {
        if (characterData.timeCharging > characterData.timeToReachCharge)
        {
            Debug.Log("FULL Release!");
        }
        else
        {
            Debug.Log("Normal Release");
        }
        characterData.currentlyCharging = false;
        characterData.timeCharging = 0;
    }

    public override void Update(ref PassableCharacterData characterData)
    {
        if(characterData.currentlyCharging)
        {
            characterData.timeCharging += Time.deltaTime;
        }
    }

    public override void FixedUpdate(ref PassableCharacterData characterData)
    {

    }
}

public class Attack1State : CharState
{
    public override void HandleInput(ref PassableCharacterData characterData, ref InputAction.CallbackContext ctx)
    {
        switch (ctx.action.name)
        {
            case "Attack":
                characterData.SetState(CharacterStateID.ATTACK_2_STATE);
                break;
            default:
                break;
        }
    }

    public override void Update(ref PassableCharacterData characterData)
    {

    }

    public override void FixedUpdate(ref PassableCharacterData characterData)
    {
    }
}

public class JumpState : CharState
{
    
    public override void HandleInput(ref PassableCharacterData characterData, ref InputAction.CallbackContext ctx)
    {
        switch (ctx.action.name)
        {
            case "Attack":
                characterData.SetState(CharacterStateID.ATTACK_1_STATE);
                break;
            case "Jump":
                //characterData.SetState(CharacterStateID.JUMP_STATE);
                break;
            case "Charge":
                characterData.currentlyCharging = true;
                break;
            case "Release Charge":
                ReleaseCharge(ref characterData);
                break;
            case "Roll":
                characterData.SetState(CharacterStateID.ROLLING_STATE);
                break;
            case "Movement Keys":
                characterData.desiredMovement = ctx.ReadValue<Vector2>();
                break;
            default:
                break;
        }
    }

    protected void ReleaseCharge(ref PassableCharacterData characterData)
    {
        if (characterData.timeCharging > characterData.timeToReachCharge)
        {
            Debug.Log("FULL Release!");
        }
        else
        {
            Debug.Log("Normal Release");
        }
        characterData.currentlyCharging = false;
        characterData.timeCharging = 0;
    }

    public override void Update(ref PassableCharacterData characterData)
    {
        if (characterData.currentlyCharging)
        {
            characterData.timeCharging += Time.deltaTime;
        }
    }

    public override void FixedUpdate(ref PassableCharacterData characterData)
    {

    }
}