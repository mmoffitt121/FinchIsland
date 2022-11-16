using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;


public class birdbrain : Agent
{
    [Tooltip("How fast the boi is going")]
    public float moveForce = 2f;

    [Tooltip("How fast the boi is tilting")]
    public float pitchSpeed = 2f;

    [Tooltip("Speed to rotate around the up axis")]
    public float yAxisSpeesd = 2f;

    [Tooltip("Agents Camera")]
    public Camera agentCamera;

    [Tooltip("Is it in training mode or playing mode")]
    public bool trainingMode;

    //The rigid body of the agent
    new private Rigidbody rigidbody;

    //Need the island area here

    //Need the closest rock for food

    //Smoother Pitches
    private float smoothPitchChange = 0f;

    //Smoother yAxis 
    private float smoothyAxisChange = 0f;

    //Bird cant do backflips anymore :( 
    private const float MaxPitchAngle = 80f;

    //Prevents or enables floating in space
    private bool frozen = false;
}
