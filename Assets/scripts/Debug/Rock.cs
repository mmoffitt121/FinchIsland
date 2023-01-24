using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //changing the rock color so i know it works, we can delete this later
    public Color fullRock = new Color(1f,0f,0f);

    public Color emptyRock = new Color(0f, 1f, 0f);

    //Solid collider representing the rock
    public Collider rockCollider;

    //rock material
    private Material rockMaterial;

    //Vector pointing out of the rock
    public Vector3 rockUpVector
    {
        get
        {
            return rockCollider.transform.up;
        }
    }

    //Center of rock
    public Vector3 rockCenter
    {
        get
        {
            return rockCollider.transform.position;
        }
    }

    /// <summary>
    /// How many pebbles does it take to make a rock? 
    /// </summary>
    public float pebbles { get; private set; }

    public bool hasPepples
    {
        get
        {
            return pebbles > 0f;
        }
    }

    public float Feed(float amount)
    {
        //tracks how many pebbles are swallowed
        float pebblesSwallowed = Mathf.Clamp(amount, 0f, pebbles);

        //subtract pebbles
        pebbles -= amount;

        if(pebbles <= 0)
        {
            //no pebbles remain
            pebbles = 0;

            //turn off rock
            rockCollider.gameObject.SetActive(false);

            //change rock color
            rockMaterial.SetColor("_BaseColor", emptyRock);
        }
        return pebblesSwallowed;  
    }

    public void resetRock()
    {
        //refill the rock
        pebbles = 1f;

        //enable the rock
        rockCollider.gameObject.SetActive(true);

        //change rock color
        rockMaterial.SetColor("_BaseColor", fullRock);
    }

    public void Awake()
    {
        //find the rock mesh and get main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        rockMaterial = meshRenderer.material;

        //rock collider
        rockCollider = transform.Find("rockCollider").GetComponent<Collider>();
    }
}
