using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SimulationResourceHolder : MonoBehaviour
{
    public GameObject itemLayoutGroup;
    public GameObject itemUIPrefab;
    public GameObject[] possibleResources;
    public GameObject bird;
    public int selected;

    public void SetSelected(int selected)
    {
        this.selected = selected;
        foreach (PlaceableItemUI pi in FindObjectsByType<PlaceableItemUI>(FindObjectsSortMode.None))
        {
            if (pi.index != selected)
            {
                pi.Deselect();
            }
        }
    }

    public void LoadObjectOptions()
    {
        int i = 0;
        foreach (GameObject item in possibleResources)
        {
            GameObject itemObject = Instantiate(itemUIPrefab);
            itemObject.transform.SetParent(itemLayoutGroup.transform, false);
            itemObject.GetComponent<PlaceableItemUI>().index = i;
            itemObject.GetComponent<PlaceableItemUI>().SetName(item.name);
            i++;
        }
    }

    private void Start()
    {
        LoadObjectOptions();
    }
}
