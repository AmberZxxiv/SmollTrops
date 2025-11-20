using UnityEngine;

public class Room_Spawner : MonoBehaviour
{
    public int doorDirection;
    // 1 = door en 1X
    // 2 = door en 0X
    // 3 = door en 1Z
    // 4 = door en 0Z
    public Room_Manager _RM; //singleton de las listas
    private bool spawned = false;
    // cortar spawn (NO FUNDIONAN MULTI PUERTAS, generan varias)

    void Start()
    {
        // pillo el singleton de las listas
        _RM = Room_Manager.instance;
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA
        Invoke("SpawnRoom", 0.5f);
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA

    }

    void SpawnRoom()
    {
        // si tenemos el maximo de salas colocadas, genera una cerrada
        if (_RM.roomSpawned >= _RM.maxRooms)
        {
            Instantiate(_RM.closedRoom, transform.position, transform.rotation);
            spawned = true;
            return;
        }

        if (spawned == false)
        {
            if (doorDirection == 1) //para puerta en 1X busca puerta en 0X=2
            {
                Instantiate(_RM.room0X[Random.Range(0, _RM.room1X.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 2) //para puerta en 0X busca puerta en 1X=1
            {
                Instantiate(_RM.room1X[Random.Range(0, _RM.room0X.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 3) //para puerta en 1Z busca puerta en 0Z=4
            {
                Instantiate(_RM.room0Z[Random.Range(0, _RM.room1Z.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 4) //para puerta en 0Z busca puerta en 1Z=3
            {
                Instantiate(_RM.room1Z[Random.Range(0, _RM.room0Z.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            _RM.roomSpawned++;
            spawned = true;
        }
    }

    // me aseguro que no se generen una encima de otra
    private void OnTriggerEnter(Collider other)
    {
        var otherSpawner = other.GetComponent<Room_Spawner>();

        // si quito esto se rompe con la sala 1
        if (!other.CompareTag("roomconection"))
            return;


        // Dos spawners chocan antes de generar sala
        if (!spawned && !otherSpawner.spawned)
        {
            // destruir solo este spawner
            Destroy(gameObject);
            print("hace cosa bien");

        }
    }
}
