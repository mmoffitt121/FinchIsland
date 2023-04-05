using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIslandArea : MonoBehaviour
{
    public const float AreaDiameter = 5f;

    //list of all nuts in area with dictionary
    private List<GameObject> nuts;
    private Dictionary<Collider, WallNUT> nutDictionary;

    //list of all wallnuts
    public List<WallNUT> Wallnuts { get; private set; }

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


    public WallNUT GetFood (Collider foodCollider)
    {
        return nutDictionary[foodCollider];
    }

    private void Awake()
    {
        nuts = new List<GameObject>();
        nutDictionary = new Dictionary<Collider, WallNUT>();
        Wallnuts = new List<WallNUT>();
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
            if (child.CompareTag("foodB"))
            {
                nuts.Add(child.gameObject);
                FindChildFood(child);
            }
            else
            {
                WallNUT wallnut = child.GetComponent<WallNUT>();
                if(wallnut != null)
                {
                    Wallnuts.Add(wallnut);
                    nutDictionary.Add(wallnut.foodCollider, wallnut);
                }
                else
                {
                    FindChildFood(child);
                }

            }
        }
    }

}
