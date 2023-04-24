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



    //is walnut empty
    public float checkNut { get; private set; }
  

    public bool AmountinWalnut
    {
        get
        {
            return checkNut > 0f;
        }
    }

    public float Feed(float amount)
    {
        float eaten = Mathf.Clamp(amount, 0f, checkNut);
      
        checkNut -= amount;
        if(checkNut <= 0)
        {
            checkNut = 0;

            //Disable collider eventually
            wallnutCollider.gameObject.SetActive(false);


            //printing to see if eaten
            print("gobbled the nut");

        }


        return eaten;

    }

    public void ResetNut()
    {
        checkNut = 1f;

        //enable collider eventually
        wallnutCollider.gameObject.SetActive(true);

        print("food is full");
    }

    private void Awake()
    {
        //Finding mesh rendered and  material 
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        nutMaterial = meshRenderer.material;

        //finding wallnut collider NEED TO FIX
       // wallnutCollider = transform.Find("wallnutCollider").GetComponent<Collider>();
        //foodBlocker = transform.Find("foodBlocker").GetComponent<Collider>();


    }
}
