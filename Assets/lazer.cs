using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public Transform laz, tar,laz2;
    Vector3 size,targ;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        size = laz.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
       if(Physics.Raycast(transform.position, transform.forward, out hit,1000f))
            {
            targ = hit.point;
            }
       else
            {
            targ = transform.forward * 1000f;
            }
        laz2.position = (transform.position + targ) / 2;
        laz.localScale = new Vector3(size.x,size.y * Vector3.Distance(targ, transform.position),size.z)/2;
        laz2.LookAt(transform.position);
    }
}
