using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraldSpin : MonoBehaviour
{
    public float rotationSpeed;
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y + rotationSpeed, 
            transform.rotation.eulerAngles.z));
    }
}
