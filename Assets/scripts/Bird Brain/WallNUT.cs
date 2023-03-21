using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNUT : MonoBehaviour
{
    public Collider food;
    public Material foodMaterial;

    //Start of finding food?

    public Vector3 foodVectorUP
    {
        get
        {
            return food.transform.up;
        }
    }

    public Vector3 foodCenter
    {
        get
        {
            return food.transform.position;
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
        float eaten = Mathf.Clamp(amount, 0f, amount);
        checkNut -= amount;
        if(checkNut <= 0)
        {
            checkNut = 0;

            //Disable collider eventually
            food.gameObject.SetActive(false);


            //printing to see if eaten
            print("gobbled the nut");
        }

        return eaten;
    }

    public void ResetNut()
    {
        checkNut = 1f;

        //enable collider eventually
        food.gameObject.SetActive(true);

        print("food is full");
    }

    private void Awake()
    {
        //Finding mesh rendered and  material 
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        foodMaterial = meshRenderer.material;

        //finding wallnut collider
        food = transform.Find("TempFood").GetComponent<Collider>();
    }


}
