using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

// This will be the place to set up the values for the bird movement
public class MainBrainFrame : Agent {
    // Controls the movemoent of the bird
    public float moveForce = 2f;
    public float pitchSpeed = 100f;
    public float yawSpeed = 100f;

    //Controls the birds beak
    public Transform beakTip;
    public float beakThickness = 0f;

    //bird beak thickness (worm = long thin, wallnut = short thick )

    //Controls the movement of the bird
    private float smoothPitchChange = 0f;
    private float smoothYawChange = 0f;
    private const float MaxPitchAngle = 80f;
    private const float BeakTipRadius = 0.1f;
    //we are using the whole bird. Need to add a beak collider
    // private const float beakAngle = 80f;

    //parts of the bird used for testing
    public Camera geraldPOV;
    public bool Testing;
    new private Rigidbody rigidbody;

    //Calling other scripts
    private MainIslandArea islandArea;
    private WallNUT nearestNut;
   // private Worms nearestworm;

    private bool frozen = false;

    //checks worms and wallnuts
    public float checkNut { get; private set; }
    public float checkWorm { get; private set; }
   //public float BeakThickness { get => beakThickness; set => beakThickness = value; }

    //starting it up
    public override void Initialize() {
        rigidbody = GetComponent<Rigidbody>();
        islandArea = GetComponentInParent<MainIslandArea>();

        if (!Testing) {
            MaxStep = 0;
        }
    }

    //once it starts it runs these
    public override void OnEpisodeBegin() {
        if (Testing) {
            islandArea.ResetNut();
            //islandArea.ResetWorm();
        }

        checkNut = 0f;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        UpdateNearestNut();
       // UpdateNearestWorm();
    }


    public override void OnActionReceived(ActionBuffers actions) {
        if (frozen) {
            return;
        }

        //actions that the bird does in order to moce during ai training, not user input 
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
        pitch = Mathf.Clamp(pitch, -MaxPitchAngle, MaxPitchAngle);
        float yaw = rotationVector.y + smoothYawChange * Time.fixedDeltaTime * yawSpeed;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    //how the bird views the world, Observations are vector lines coming out of the bird
    public override void CollectObservations(VectorSensor sensor) {
        if (nearestNut == null) {
            sensor.AddObservation(new float[10]);
            return;
        }

        //sensors for wallnuts
        sensor.AddObservation(transform.localRotation.normalized);
        Vector3 toNut = nearestNut.wallnutCenter - beakTip.position;
        sensor.AddObservation(toNut.normalized);
        sensor.AddObservation(Vector3.Dot(toNut.normalized, -nearestNut.wallnutVectorUP.normalized));
        sensor.AddObservation(Vector3.Dot(beakTip.forward.normalized, -nearestNut.wallnutVectorUP.normalized));
        sensor.AddObservation(toNut.magnitude / MainIslandArea.AreaDiameter);
    }

    /// Make the bird move using user input
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

   //Checks for the amout of food in the wallnut and looks for the nearest one to the bird
    private void UpdateNearestNut() {
        foreach (WallNUT wallnut in islandArea.Wallnuts) {
            if (nearestNut == null && wallnut.AmountinWalnut) {
                nearestNut = wallnut;
            }
            else if (wallnut.AmountinWalnut) {
                float distanceToNut = Vector3.Distance(wallnut.transform.position, beakTip.position);
                float distCurrent = Vector3.Distance(nearestNut.transform.position, beakTip.position);

                if (!nearestNut.AmountinWalnut || distanceToNut < distCurrent) {
                    nearestNut = wallnut;
                }
            }
        }
    }
    /// <summary>
    /// Would have been code for the worm if it worked
    /// </summary>
    /// <param name="other"></param>
  /*  private void UpdateNearestWorm()
    {
        foreach (Worms worm in islandArea.Worm)
        {
            if(nearestworm == null && worm.AmountinWorm)
            {
                nearestworm = worm;
            }
            else if (worm.AmountinWorm)
            {
                float distanceToWorm = Vector3.Distance(worm.transform.position, beakTip.position);
                float distCurrent = Vector3.Distance(nearestworm.transform.position, beakTip.position);

                if(!nearestworm.AmountinWorm || distanceToWorm < distCurrent)
                {
                    nearestworm = worm;
                }
            }


        }
    }*/
    private void OnTriggerEnter(Collider other) {

        TriggerEnterOrStay(other);
    }

    private void OnTriggerStay(Collider other) {
        TriggerEnterOrStay(other);

    }

    //Once the bird collides, it can distinguish between the wallnut and the worm
    private void TriggerEnterOrStay(Collider collider) {
        //Check if colliding with nut, Continues because of Trigger.

        if (collider.CompareTag("wallnutCollider")) {

            Vector3 closestPointToBeakTip = collider.ClosestPoint(beakTip.position);

            if (Vector3.Distance(beakTip.position, closestPointToBeakTip) < BeakTipRadius) {
                WallNUT wallnut = islandArea.GetFood(collider);
                float eaten = wallnut.Feed(.01f);


                checkNut += eaten;

                if (Testing) {
                    rigidbody.isKinematic = true;

                    float bonus = .02f * Mathf.Clamp01(Vector3.Dot(transform.forward.normalized, -nearestNut.wallnutVectorUP.normalized));
                    //check wallnut thickness and bird beak thick
                    AddReward(.01f + bonus);
                    print("add reward for nut" );
                    // Increase the scale of the bird object (Just for testing).
                    //transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                }

                if (!wallnut.AmountinWalnut) {
                    UpdateNearestNut();
                }

                rigidbody.isKinematic = false;

            }

        }
        //this would have been worm food (or Wood), didnt get to work
       /* else if (collider.CompareTag("Wood"))
        {
            Vector3 closestPointToBeakTip = collider.ClosestPoint(beakTip.position);

            if (Vector3.Distance(beakTip.position, closestPointToBeakTip) < BeakTipRadius)
            {
                Worms worm = islandArea.GetWorms(collider);
                float eaten = worm.Feed(.01f);


                checkNut += eaten;

                if (Testing)
                {
                    rigidbody.isKinematic = true;

                    float bonus = .02f * Mathf.Clamp01(Vector3.Dot(transform.forward.normalized, -nearestworm.wormVectorUP.normalized));
                    //check wallnut thickness and bird beak thick
                    AddReward(.01f + bonus);
                    print("add reward for worm");
                    // Increase the scale of the bird object (Just for testing).
                    //transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                }

                if (!worm.AmountinWorm)
                {
                    UpdateNearestWorm();
                }

                rigidbody.isKinematic = false;

            }

        }*/
    }

    //tags that either dont add a value or remove a reward such as boundary
    private void OnCollisionEnter(Collision collision) {
        if (Testing && collision.collider.CompareTag("SimulationResource"))
        {
            rigidbody.isKinematic = true;
            print("Ignoring simulation resource, no reward taken or recieved");
        }

        else if (Testing && collision.collider.CompareTag("boundary")) {
            rigidbody.isKinematic = true;
            //Need to add reward system
            print("remove reward AND HIT BOUNDARY");
            AddReward(-.5f);
            
        }
       
        rigidbody.isKinematic = false;

    }

    //makes things smoother
    private void Update() {
        if (nearestNut != null) {
            Debug.DrawLine(beakTip.position, nearestNut.wallnutCenter, Color.green);
        }
    }

    private void FixedUpdate() {
        if (nearestNut != null && !nearestNut.AmountinWalnut) {
            UpdateNearestNut();        }
    }
}




