using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class Room_Spawner : MonoBehaviour
{ // script en trigger RoomSpawner de cada puerta abierta
    // enum para definir la direccion del spawn
    public RoomDirection direction;
    public enum RoomDirection
    { 
        Zplus,
        Xplus,
        Zminus,
        Xminus
    }

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
        GameObject roomToSpawn = null; // declaro la sala que va a ser
        Vector3 roomPos = Vector3.zero; // declaro la posición de referencia
        switch (direction) // segun el estado del enum RoomDirection
        {
            case RoomDirection.Zplus:// para 1Z need 0z
                roomToSpawn = _RM.room0z[Random.Range(0, _RM.room0z.Length)];
            break;

            case RoomDirection.Xplus:// para 1X need 0x
                roomToSpawn = _RM.room0x[Random.Range(0, _RM.room0x.Length)];
            break;
            
            case RoomDirection.Zminus:// para 0z need 1Z
                roomToSpawn = _RM.room1Z[Random.Range(0, _RM.room1Z.Length)];
            break;
        
            case RoomDirection.Xminus:// para 0x need 1X
                roomToSpawn = _RM.room1X[Random.Range(0, _RM.room1X.Length)];
            break;

            default: // si no encuentra estado asignado, detiene el SCRIPT
            Debug.LogWarning("Room Direction no asignada en Room_Spawner");
            return;
        }
        // al max de salas, sala cerrada en altura
        if (_RM.roomsSpawned >= _RM.maxRooms && spawned==false)
        {
            roomToSpawn = _RM.closedRoom;
            roomPos = Vector3.up *5;
        }
        else // si no, sumo sala spawneda
        {
            _RM.roomsSpawned++;
        }
        // instancio la selección y apago el spawn
        Instantiate(roomToSpawn, transform.position + roomPos, transform.rotation);
        spawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spawnroom"))
        {
            // si dos spawnrooms chocan, se cierra el pasillo y se elimina el spawn
            if (spawned==false && other.GetComponent<Room_Spawner>().spawned==false)
            {
                Instantiate(_RM.closedRoom, transform.position + Vector3.up * 5, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
