using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.CharacterController;
using Unity.Physics.Authoring;
using UnityEngine.Serialization;

[Serializable]
public struct ThirdPersonCharacterComponent : IComponentData
{
    public float RotationSharpness;
    public float GroundMaxSpeed;
    public float GroundedMovementSharpness;
    public float AirAcceleration;
    public float AirMaxSpeed;
    public float AirDrag;
    public float JumpSpeed;
    public float3 Gravity;
    public bool PreventAirAccelerationAgainstUngroundedHits;
    public BasicStepAndSlopeHandlingParameters StepAndSlopeHandling;
    public int MaxAirJumps;
    [UnityEngine.HideInInspector] 
    public int CurrentAirJumps;
    [FormerlySerializedAs("SprintSpeedMultiplayer")] public float SprintSpeedMultiplier;

    public CustomPhysicsBodyTags IgnoredPhysicsTags;
}

[Serializable]
public struct ThirdPersonCharacterControl : IComponentData
{
    public float3 MoveVector;
    public bool Jump;
    public bool Sprint;
}
