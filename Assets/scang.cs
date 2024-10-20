using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scang : MonoBehaviour
{
    public bool gr = false;
    public ContactPoint2D? points;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("wps"))
            gr = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("wps"))
        {

        gr = false;
        points = null;
        }
    }


    private void OnCollisionStay2D(Collision2D co)
    {
        if (!co.transform.CompareTag("wps"))
            points = co.contacts[0];
    }
}
