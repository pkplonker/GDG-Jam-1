using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CopyYRot : MonoBehaviour
{

    [SerializeField] Transform TargetObject;
    
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = TargetObject.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rot);
    }
}
