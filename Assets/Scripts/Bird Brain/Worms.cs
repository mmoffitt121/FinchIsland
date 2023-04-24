using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worms : MonoBehaviour
{
    //colliders for the worm
    public Collider WormCollider;
    public Collider WormBloker;
    public Material wormMaterial;

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
    public float checkWorm { get; private set; }


    public bool AmountinWorm
    {
        get
        {
            return checkWorm > 0f;
        }
    }

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

    public void ResetWorm()
    {
        checkWorm = 1f;
        WormCollider.gameObject.SetActive(true);

        print("worm food is full");
    }

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        wormMaterial = meshRenderer.material;

        //Need to fix
        //WormCollider = transform.Find("Wood").GetComponent<Collider>();
    }


}
