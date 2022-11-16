using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandArea : MonoBehaviour
{
    //How big the island is. IM PICKING A RANDOM NUMBER PLEASE CHANGE
    public const float areaDiameter = 23f;

    //list of all rocks 
    private List<GameObject> allRocks;

    //Need the script for rock food, thats why its underlined
    private Dictionary<Collider, Rock> rockDictionary;

    /// <summary>
    /// List of Rocks in the area, right now we have 1 rock
    /// </summary>
    public List<Rock> Rocks { get; private set; }

    public void resetRock()
    {
        foreach (Rock rock in Rocks)
        {
            rock.resetRock();
        }
    }

    public Rock gettingPebbles(Collider collider)
    {
        return rockDictionary[collider];
    }

    private void Awake()
    {
        allRocks = new List<GameObject>();
        rockDictionary = new Dictionary<Collider, Rock>();
        Rocks = new List<Rock>();
    }

    //Some point we need to figure out how we are going to find food.

}
