using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public static event Action ObjectBroken; 
    [SerializeField] GameObject brokenVarient;
    
    [SerializeField] float breakThreshold = 89;

    bool broken = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        if (!broken && ((rot.x > breakThreshold && rot.x < 306 - breakThreshold) || (rot.z > breakThreshold && rot.z < 360 - breakThreshold)))
        {
            Break();
        }
    }

    private void Break()
    {
        ObjectBroken?.Invoke();
        broken = true;
        gameObject.SetActive(false);
        GameObject obj = Instantiate(brokenVarient, transform.position, transform.rotation);
        obj.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
