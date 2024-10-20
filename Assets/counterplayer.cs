using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class counterplayer : MonoBehaviourPunCallbacks
{
    public static counterplayer Instance;

    public int currentPlayerCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //Debug.Log("Player entered. Current player count: " + currentPlayerCount);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //Debug.Log("Player left. Current player count: " + currentPlayerCount);
    }
}
