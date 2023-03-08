using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

// This will be the place to set up the values for the bird movement
public class MainBrainFrame : Agent {
    public float moveForce = 2f;
    public float pitchSpeed = 100f;
    public float yawSpeed = 100f;
    private float smoothPitchChange = 0f;
    private float smoothYawChange = 0f;
    private const float beakAngle = 80f;
    public Camera geraldPOV;
    public bool Testing;
    new private Rigidbody rigidbody;
    private bool frozen = false;

    public override void Initialize() {
        rigidbody = GetComponent<Rigidbody>();
        if (!Testing) {
            MaxStep = 0;
        }
    }

    public override void OnEpisodeBegin() {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public override void OnActionReceived(ActionBuffers actions) {
        if (frozen) {
            return;
        }

        Vector3 move = new Vector3(actions.ContinuousActions[0], actions.ContinuousActions[1], actions.ContinuousActions[2]);
        rigidbody.AddForce(move * moveForce);
        Vector3 rotationVector = transform.rotation.eulerAngles;
        float pitchChange = actions.ContinuousActions[3];
        float yawChange = actions.ContinuousActions[4];
        smoothPitchChange = Mathf.MoveTowards(smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
        smoothYawChange = Mathf.MoveTowards(smoothYawChange, yawChange, 2f * Time.fixedDeltaTime);
        float pitch = rotationVector.x + smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;

        if (pitch > 180f) {
            pitch -= 360f;
        }

        float yaw = rotationVector.y + smoothYawChange * Time.fixedDeltaTime * yawSpeed;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    /// Make the bird move
    public override void Heuristic(in ActionBuffers actionsOut) {
        var continousActionsOut = actionsOut.ContinuousActions;
        Vector3 forward = Vector3.zero;
        Vector3 left = Vector3.zero;
        Vector3 up = Vector3.zero;
        float pitch = 0f;
        float yaw = 0f;

        // Forward and back
        if (Input.GetKey(KeyCode.W)) forward = -transform.forward;
        else if (Input.GetKey(KeyCode.S)) forward = transform.forward;

        // Left and right
        if (Input.GetKey(KeyCode.A)) left = transform.right;
        else if (Input.GetKey(KeyCode.D)) left = -transform.right;

        //Up and down
        if (Input.GetKey(KeyCode.E)) up = transform.up;
        else if (Input.GetKey(KeyCode.C)) up = -transform.up;

        //Pitch up and down
        if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;

        //Turn left and right
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        Vector3 combined = (forward + left + up).normalized;

        continousActionsOut[0] = combined.x;
        continousActionsOut[1] = combined.y;
        continousActionsOut[2] = combined.z;
        continousActionsOut[3] = pitch;
        continousActionsOut[4] = yaw;
    }

    // Stop the bird
    public void FreezeAgent() {
        frozen = true;
        rigidbody.Sleep();
    }

    // Let the bird move
    public void Unfreeze() {
        frozen = false;
        rigidbody.WakeUp();
    }
}