using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worms : MonoBehaviour
{
    //colliders for the worm
    public Collider WormCollider;
    public Collider WormBloker;
    public Material wormMaterial;


    //How the bird finds food, Vector Up and Center
    public Vector3 wormVectorUP
    {
        get
        {
            return WormCollider.transform.up;
        }
    }

    public Vector3 wormCenter
    {
        get
        {
            return WormCollider.transform.position;
        }
    }

    //Makes sure food is in worm
    public float checkWorm { get; private set; }

    //Checks the amount of food in worm
    public bool AmountinWorm
    {
        get
        {
            return checkWorm > 0f;
        }
    }

    //Once the bird eats, remove values from worm
    public float Feed(float amount)
    {
        float eaten = Mathf.Clamp(amount, 0f, checkWorm);
         if (checkWorm <= 0)
        {
            checkWorm = 0;

            WormCollider.gameObject.SetActive(false);
            print("ate the worm");
        }
        return eaten;

    }

    //resets the worm
    public void ResetWorm()
    {
        checkWorm = 1f;
        WormCollider.gameObject.SetActive(true);

        print("worm food is full");
    }

    //Once it starts
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        wormMaterial = meshRenderer.material;

        //Need to fix
        //WormCollider = transform.Find("Wood").GetComponent<Collider>();
    }


}
