using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Breakable : MonoBehaviour
{
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
        Debug.Log("break");
        broken = true;
        gameObject.SetActive(false);
        Instantiate(brokenVarient, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
