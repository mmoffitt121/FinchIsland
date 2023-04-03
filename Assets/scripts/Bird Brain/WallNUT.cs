using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNUT : MonoBehaviour
{
    [HideInInspector]
    public Collider foodCollider;
    public Collider foodBlocker;
    public Material foodMaterial;

    //Start of finding food?

    public Vector3 foodVectorUP
    {
        get
        {
            return foodCollider.transform.up;
        }
    }

    public Vector3 foodCenter
    {
        get
        {
            return foodCollider.transform.position;
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
            foodCollider.gameObject.SetActive(false);


            //printing to see if eaten
            print("gobbled the nut");
        }

        return eaten;
    }

    public void ResetNut()
    {
        checkNut = 1f;

        //enable collider eventually
        foodCollider.gameObject.SetActive(true);

        print("food is full");
    }

    private void Awake()
    {
        //Finding mesh rendered and  material 
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        foodMaterial = meshRenderer.material;

        //finding wallnut collider
        foodCollider = transform.Find("foodCollider").GetComponent<Collider>();
        //foodBlocker = transform.Find("foodBlocker").GetComponent<Collider>();


    }
}
