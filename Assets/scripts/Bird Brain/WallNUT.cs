using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNUT : MonoBehaviour
{
    //colliders for the wallnut
    public Collider wallnutCollider;
    public Collider wallnutBlocker;
    public Material nutMaterial;



    //Start of finding food

    //Uses Vector UP and Center to find it
    public Vector3 wallnutVectorUP
    {
        get
        {
            return wallnutCollider.transform.up;
        }
    }

    public Vector3 wallnutCenter
    {
        get
        {
            return wallnutCollider.transform.position;
        }
    }



    //Check if the  walnut is empty
    public float checkNut { get; private set; }
  
    //Checks the amount in wallnut
    public bool AmountinWalnut
    {
        get
        {
            return checkNut > 0f;
        }
    }

    //Once the bird eats the wallnut, remove part of the food from wallnut
    public float Feed(float amount)
    {
        float eaten = Mathf.Clamp(amount, 0f, checkNut);
      
        checkNut -= amount;
        if(checkNut <= 0)
        {
            checkNut = 0;

            //Disable collider eventually
            wallnutCollider.gameObject.SetActive(false);
            wallnutBlocker.gameObject.SetActive(false);


            //printing to see if eaten
            print("gobbled the nut");

        }


        return eaten;

    }

    //reset the wallnut
    public void ResetNut()
    {
        checkNut = 1f;

        //enable collider eventually
        wallnutCollider.gameObject.SetActive(true);
        wallnutBlocker.gameObject.SetActive(true);

        print("food is full");
    }

    
    //Once it starts
    private void Awake()
    {
        //Finding mesh rendered and  material 
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        nutMaterial = meshRenderer.material;

        //finding wallnut collider NEED TO FIX
        wallnutCollider = transform.Find("wallnutCollider").GetComponent<Collider>();
        wallnutBlocker = transform.Find("blocker").GetComponent<Collider>();


    }
}
