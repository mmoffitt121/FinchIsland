using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimulationResource : MonoBehaviour
{
    public GameObject resource;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void SetObject(GameObject go)
    {
        resource = Instantiate(go);
        resource.transform.position = transform.position;
    }

    void OnMouseDown()
    {
        if (FindObjectOfType<SimulationManager>().step != SimulationManager.SimulationStep.SetupEnvironment)
        {
            return;
        }

        SimulationResourceHolder holder = FindObjectOfType<SimulationResourceHolder>();
        if (holder.selected < holder.possibleResources.Length)
        {
            if (resource != null)
            {
                DestroyImmediate(resource);
            }

            GameObject res = Instantiate(holder.possibleResources[holder.selected]);
            res.transform.position = this.transform.position;
            res.transform.parent = this.transform;
            resource = res;
        }
    }
}
