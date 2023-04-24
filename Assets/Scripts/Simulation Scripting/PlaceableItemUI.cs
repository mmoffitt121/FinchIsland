using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableItemUI : MonoBehaviour
{
    // Contains the name of the item.
    public TextMeshProUGUI itemNameField;
    // Button that the user can select to place the object.
    public Button placeButton;

    // Two images that will be used to show the user whether this object is being placed or not.
    public GameObject placing;
    public int index;

    public void SetName(string name)
    {
        itemNameField.text = name;
    }

    public void Select()
    {
        placing.SetActive(true);
        FindObjectOfType<SimulationResourceHolder>().SetSelected(index);
    }

    public void Deselect()
    {
        placing.SetActive(false);
    }
}
