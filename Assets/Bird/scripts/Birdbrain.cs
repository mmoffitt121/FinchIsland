using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class Birdbrain : Agent
{
    [Tooltip("How fast the boi is going")]
    public float moveForce = 40f;

    [Tooltip("How fast the boi is tilting")]
    public float pitchSpeed = 40f;

    [Tooltip("Speed to rotate around the up axis")]
    public float yAxisSpeesd = 40f;

    [Tooltip("Agents Camera")]
    public Camera agentCamera;

    [Tooltip("Is it in training mode or playing mode")]
    public bool trainingMode;

    //The rigid body of the agent
    new private Rigidbody rigidbody;

    //Island Area
    private IslandArea islandArea;

    //Need the closest rock for food

    //Smoother Pitches
    private float smoothPitchChange = 0f;

    //Smoother yAxis 
    private float smoothyAxisChange = 0f;

    //Bird cant do backflips anymore :( 
    private const float MaxPitchAngle = 80f;

    //Prevents or enables floating in space
    private bool frozen = false;

    public float pebblesObtained { get; private set; }

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        islandArea = GetComponentInParent<IslandArea>();

        if (!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
      if (trainingMode)
        {
            //reset the rocks
            islandArea.resetRock();
        }

       //reset pebbles
        pebblesObtained = 0f;

        //zero out the velocities so movement stops before new episode
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (frozen) return;

        //math for move
        Vector3 move = new Vector3(actions.ContinuousActions[0], actions.ContinuousActions[1], actions.ContinuousActions[2]);

        rigidbody.AddForce(move * moveForce);

        Vector3 rotationVector = transform.rotation.eulerAngles;

        float pitchChange = actions.ContinuousActions[3];
        float yawChange = actions.ContinuousActions[4];

        smoothPitchChange = Mathf.MoveTowards(smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
        smoothyAxisChange = Mathf.MoveTowards(smoothyAxisChange, yawChange, 2f * Time.fixedDeltaTime);

        float pitch = rotationVector.x + smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;
        if (pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch, -MaxPitchAngle, MaxPitchAngle);

        float yaw = rotationVector.y + smoothyAxisChange * Time.fixedDeltaTime * yAxisSpeesd;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    // at some point we need to collect observations for the bird to see

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        //Some weird placeholder stuff
        Vector3 forward = Vector3.zero;
        Vector3 left = Vector3.zero;
        Vector3 up = Vector3.zero;
        float pitch = 0f;
        float yaw = 0f;

        // Forward/backward
        if (Input.GetKey(KeyCode.UpArrow)) forward = transform.forward;
        else if (Input.GetKey(KeyCode.DownArrow)) forward = -transform.forward;

        // Left/right
        if (Input.GetKey(KeyCode.S)) left = transform.right;
        else if (Input.GetKey(KeyCode.W)) left = -transform.right;

        // Up/down
        if (Input.GetKey(KeyCode.A)) up = transform.up;
        else if (Input.GetKey(KeyCode.D)) up = -transform.up;

        // Pitch up/down
        if (Input.GetKey(KeyCode.E)) pitch = 1f;
        else if (Input.GetKey(KeyCode.C)) pitch = -1f;

        // Turn left/right
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        Vector3 combined = (forward + left + up).normalized;

        continuousActionsOut[0] = combined.x;
        continuousActionsOut[1] = combined.y;
        continuousActionsOut[2] = combined.z;
        continuousActionsOut[3] = pitch;
        continuousActionsOut[4] = yaw;
    }

    //freeze unfreeze
    public void FreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        frozen = true;
        rigidbody.Sleep();
    }

    public void UnFreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        frozen = true;
        rigidbody.WakeUp();
    }

    //colliding on stuff
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterOrStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerEnterOrStay(other);
    }

    private void TriggerEnterOrStay(Collider collider)
    {
        Rock rock = islandArea.gettingPebbles(collider);
        float food = rock.Feed(.01f);    }
    private void OnCollisionEnter(Collision collision)
    {
        if(trainingMode && collision.collider.CompareTag("boundary"))
        {
            AddReward(-.5f);
        }
    }
}



