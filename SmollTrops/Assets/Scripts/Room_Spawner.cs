using UnityEngine;

public class Room_Spawner : MonoBehaviour
{
    public int openDoorection;
    // 1 = open in 1Z
    // 2 = open in 1X
    // 3 = open in 0z
    // 4 = open in 0x
    public Room_Manager _RM; //singleton de las listas
    public bool spawned = false;

    void Start()
    {
        // pillo el singleton del ROOM_MAN
        _RM = Room_Manager.instance;
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA
        Invoke("SpawnRoom", 0.5f);
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA

    }

    void SpawnRoom()
    {
        if (_RM.roomSpawned >= _RM.maxRooms)
        {
            // si ya se alcanzo el maximo de salas, paro
            spawned = true;
            return;
        }

        if (spawned == false)
        {
            if (openDoorection == 1) //for open in 1Z look for 0z
            {
                Instantiate(_RM.room0z[Random.Range(0, _RM.room0z.Length)], transform.position, transform.rotation);
            }
            if (openDoorection == 2) //for open in 1X look for 0x
            {
                Instantiate(_RM.room0x[Random.Range(0, _RM.room0x.Length)], transform.position, transform.rotation);
            }
            if (openDoorection == 3) //for open in 0z look for 1Z
            {
                Instantiate(_RM.room1Z[Random.Range(0, _RM.room1Z.Length)], transform.position, transform.rotation);
            }
            if (openDoorection == 4) //for open in 0x look for 1X
            {
                Instantiate(_RM.room1X[Random.Range(0, _RM.room1X.Length)], transform.position, transform.rotation);
            }
            _RM.roomSpawned++;
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spawnroom"))
        {
            // no entiendo cuando se cumple esto, pero funciona para evitar salas superpuestas
            if (spawned==false && other.GetComponent<Room_Spawner>().spawned==false)
            {
                // destruyo el trigger antes de que se genere la sala
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
