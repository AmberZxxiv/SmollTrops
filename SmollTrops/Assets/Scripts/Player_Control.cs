using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    #region /// PLAYER MOVEMENT ///
    Rigidbody _rb;
    public float movSpeed;
    float _movLateral;
    float _movFrontal;
    #endregion

    public GameObject startDungeon;

    void Start()
    {
        Time.timeScale = 1;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // aquí cogemos los controles del movimiento
        _movLateral = Input.GetAxisRaw("Horizontal");
        _movFrontal = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // aquí damos los valores del movimiento
        Vector3 playerMovement = (transform.right * _movLateral + transform.forward * _movFrontal);
        Vector3 playerSpeed = new Vector3(playerMovement.x * movSpeed, _rb.velocity.y, playerMovement.z * movSpeed);
        _rb.velocity = playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("portal"))
        {
            transform.position = startDungeon.transform.position;
        }
    }
}
