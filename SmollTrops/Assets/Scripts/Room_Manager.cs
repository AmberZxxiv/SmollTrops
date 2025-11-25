using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    // Listas donde declaro las salas desde el _ROOM_LIST_ del inspector
    public GameObject[] room1Z;
    public GameObject[] room1X;
    public GameObject[] room0z;
    public GameObject[] room0x;
    public GameObject closedRoom;

    // conteo del tamaño de la dungeon
    public int roomSpawned = 0;
    public int maxRooms = 11;

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
}
