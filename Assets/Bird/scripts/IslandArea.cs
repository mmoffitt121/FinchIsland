using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandArea : MonoBehaviour
{
    //How big the island is. IM PICKING A RANDOM NUMBER PLEASE CHANGE
    public const float areaDiameter = 23f;

    private List<GameObject> rocks;

    //Need the script for rock food, thats why its underlined
    private Dictionary<Collider, Rock> rockDictionary;
}
