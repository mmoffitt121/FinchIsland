using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIslandArea : MonoBehaviour
{
    public const float AreaDiameter = 3f;

    //list of all nuts in area with dictionary
    private List<GameObject> nuts;
    private Dictionary<Collider, WallNUT> nutDictionary;

    //List of all worms in area with dictionary 
    private List<GameObject> worms;
    private Dictionary<Collider, Worms> wormDictionary;

    //list of all wallnuts
    public List<WallNUT> Wallnuts { get; private set; }
    public List<Worms> Worm { get; private set; }

    public void ResetNut()
    {
        foreach (GameObject nut in nuts)
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);
            nut.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        foreach (WallNUT wallnut in Wallnuts)
        {
            wallnut.ResetNut();
        }
    }

    public void ResetWorm()
    {
        foreach (GameObject worm in worms)
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);
            worm.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        foreach (Worms worm in Worm)
        {
            worm.ResetWorm();
        }
    }


    public WallNUT GetFood (Collider wallnutCollider)
    {
        return nutDictionary[wallnutCollider];
    }

    public Worms GetWorms (Collider WormCollider)
    {
        return wormDictionary[WormCollider];
    }

    private void Awake()
    {
        nuts = new List<GameObject>();
        nutDictionary = new Dictionary<Collider, WallNUT>();
        Wallnuts = new List<WallNUT>();

        worms = new List<GameObject>();
        wormDictionary = new Dictionary<Collider, Worms>();
        Worm = new List<Worms>();
    }

    private void Start()
    {
        FindChildFood(transform);
    }

    private void FindChildFood (Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("wallnutCollider"))
            {
                nuts.Add(child.gameObject);
                FindChildFood(child);
            }
            else if (child.CompareTag("Wood"))
            {
                worms.Add(child.gameObject);
                FindChildFood(child);
            }

            else
            {
                WallNUT wallnut = child.GetComponent<WallNUT>();
                Worms worm = child.GetComponent<Worms>();
                if (wallnut != null)
                {
                    Wallnuts.Add(wallnut);
                    nutDictionary.Add(wallnut.wallnutCollider, wallnut);
                }
      
                else if(worm != null)
                {
                    Worm.Add(worm);
                    wormDictionary.Add(worm.WormCollider, worm);
                }

                else
                {
                    FindChildFood(child);
                }

            }
        }
    }

}
