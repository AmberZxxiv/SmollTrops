using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{// script en el empty padre del PLAYER

    #region /// PLAYER MOVEMENT ///
    Rigidbody _rb;
    public float movSpeed;
    public float sprintMulti;
    float _movLateral;
    float _movFrontal;
    #endregion

    public float health;
    public GameObject startDungeon;

    // singletonpara llamar a este código desde cualquier otro
    public static Player_Control instance;
    
    void Awake()
    {// awake para instanciar singleton sin superponer varios
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // aqui cogemos los controles del movimiento
        _movLateral = Input.GetAxisRaw("Horizontal");
        _movFrontal = Input.GetAxisRaw("Vertical");
        // y rotamos el sprite dependiendo de la direccion
        if (_movLateral != 0 )
        {
        transform.localScale = new Vector3(_movLateral > 0 ? -1 : 1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        // aqui podría meter un cuarternion para ver si corro o no
        float currentSpeed = Input.GetKey(KeyCode.LeftShift)? movSpeed * sprintMulti : movSpeed;
        // aqui damos los valores del movimiento
        Vector3 playerMovement = (transform.right * _movLateral + transform.forward * _movFrontal);
        Vector3 playerSpeed = new Vector3(playerMovement.x * currentSpeed, _rb.linearVelocity.y, playerMovement.z * currentSpeed);
        _rb.linearVelocity = playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("portal"))
        {
            transform.position = startDungeon.transform.position;
        }
    }
}
