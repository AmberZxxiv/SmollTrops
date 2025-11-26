using UnityEngine;
using System.Collections.Generic;

public class Room_Manager : MonoBehaviour
{ // este script está en el empty ROOM_MAN del inspector

    // Listas donde declaro las salas desde el _ROOM_LIST_ del inspector
    public GameObject[] room1Z;
    public GameObject[] room1X;
    public GameObject[] room0z;
    public GameObject[] room0x;
    public GameObject closedRoom;

    // lista control de las salas en el mapa
    public List<GameObject> roomMap;
    public int roomsSpawned = 0;
    public int maxRooms = 15;

    public GameObject bossBall;
    public GameObject minionBos;

    // singletonpara llamar a este código desde cualquier otro
    public static Room_Manager instance;

    // awake para instanciar singleton sin superponer varios
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke("SpawnEnemy", 5f);
    }

    void SpawnEnemy()
    {
        Instantiate(bossBall, roomMap[roomMap.Count-1].transform.position + Vector3.up * 5, transform.rotation);

        for(int i = 0; i < roomMap.Count-1; i++)
        {
            Instantiate(minionBos, roomMap[i].transform.position + Vector3.up * 5, transform.rotation);
        }
    }
}
