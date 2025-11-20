using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    // Listas donde declaro las salas desde el _ROOM_LIST_ del inspector
    public GameObject[] room1X;
    public GameObject[] room0X;
    public GameObject[] room1Z;
    public GameObject[] room0Z;
    public GameObject closedRoom;

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
