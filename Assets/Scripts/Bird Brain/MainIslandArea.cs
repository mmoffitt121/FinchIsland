using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIslandArea : MonoBehaviour
{
    //size of the island
    public const float AreaDiameter = 3f;

    //list of all nuts in area with dictionary
    private List<GameObject> nuts;
    private Dictionary<Collider, WallNUT> nutDictionary;
    public WallNUT nutcollider;

    //List of all worms in area with dictionary 
    //private List<GameObject> worms;
    //private Dictionary<Collider, Worms> wormDictionary;

    //list of all wallnuts
    public List<WallNUT> Wallnuts { get; private set; }
    //public List<Worms> Worm { get; private set; }

    //Resets the wallnuts
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

    //Would Reset the worms
   /* public void ResetWorm()
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

    */

    //How it finds food
    public WallNUT GetFood (Collider collider)
    {
          return nutDictionary[collider];
    }

   /* public Worms GetWorms (Collider collider)
    {
        return wormDictionary[collider];
    }
   */

    //Once it starts for both Awake and Start
    private void Awake()
    {
        nuts = new List<GameObject>();
        nutDictionary = new Dictionary<Collider, WallNUT>();
        Wallnuts = new List<WallNUT>();

       /* worms = new List<GameObject>();
        wormDictionary = new Dictionary<Collider, Worms>();
        Worm = new List<Worms>();*/
    }

    private void Start()
    {
        FindChildFood(transform);
    }

    //Finds all the food that is a child of the island
    public void FindChildFood (Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("wallnutCollider"))
            {
                nuts.Add(child.gameObject);
               
                FindChildFood(child);
            }
           /* else if (child.CompareTag("Wood"))
            {
                worms.Add(child.gameObject);
                FindChildFood(child);
            }*/

            else
            {
                WallNUT wallnut = child.GetComponent<WallNUT>();
               // Worms worm = child.GetComponent<Worms>();
                if (wallnut != null)
                {
                    Wallnuts.Add(wallnut);
                    nutDictionary.Add(wallnut.wallnutCollider, wallnut);
                }
      
               /* else if(worm != null)
                {
                    Worm.Add(worm);
                    wormDictionary.Add(worm.WormCollider, worm);
                }*/

                else
                {
                    FindChildFood(child);
                }


            }
        }

    }

}
