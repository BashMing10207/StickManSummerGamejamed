using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class summon : MonoBehaviourPunCallbacks
{
    public GameObject guns;
    public bool boomis;
    // Start is called before the first frame update

    private void Awake()
    {
        
        InvokeRepeating("respawn", Random.Range(5,30), Random.Range(25, 30));
    }

    private void respawn()
    {   
        PhotonNetwork.Instantiate(guns.name, transform.position, transform.rotation);
    }
    //void FixedUpdate()
    //{
    //    if (PhotonNetwork.InRoom)
    //    {
    //        PhotonNetwork.Instantiate(guns.name,transform.position, Quaternion.identity);


    //        //Destroy(gameObject);
    //    }

    //}
}
