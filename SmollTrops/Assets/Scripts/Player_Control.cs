using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{// script en el empty padre del PLAYER
 // SINGLETON script
    public static Player_Control instance;
 // SINGLETON script
    public Weapon_Control _WC; //pillo SINGLE del WC

    #region /// PLAYER MOVEMENT ///
    Rigidbody _rb;
    public float movSpeed;
    public float dashForce;
    public float dashTimer;
    bool _canDash = true;
    bool _isDashing = false;
    float _movLateral;
    float _movFrontal;
    #endregion

    public GameObject startDungeon;//tp a la sala principal

    #region /// HEALTH STATUS ///
    public float health; //vida del player
    public SpriteRenderer spriteRenderer; //render del sprite
    Color _originalColor;
    #endregion


    void Awake()
    {// awake para instanciar singleton sin superponer varios
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _WC = Weapon_Control.instance; //pillo SINGLE del WC
        _rb = GetComponent<Rigidbody>();
        _originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // aqui cogemos los controles del movimiento
        _movLateral = Input.GetAxis("Horizontal");
        _movFrontal = Input.GetAxis("Vertical");
        // y rotamos el sprite dependiendo de la direccion
        if (_movLateral != 0 )
        {
        transform.localScale = new Vector3(_movLateral > 0 ? -1 : 1, 1, 1);
        }
        // controlamos la habilidad del dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            DoDASH();
        }
    }

    private void FixedUpdate()
    {
        if (!_isDashing)
        {
            // aqui damos los valores del movimiento
            Vector3 playerMovement = (transform.right * _movLateral + transform.forward * _movFrontal);
            Vector3 playerSpeed = new Vector3(playerMovement.x * movSpeed, _rb.linearVelocity.y, playerMovement.z * movSpeed);
            _rb.linearVelocity = playerSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("portal"))
        {
            transform.position = startDungeon.transform.position;
        }

        Power_Giver power = other.GetComponent<Power_Giver>();
        if (power != null)
        {
            _WC.EquipWeapon(power.newWeapon);
            Destroy(other.gameObject);
        }
    }

    public IEnumerator FlashDamage()//lo llaman los enemigos al hitearme
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = _originalColor;
    }

    private void DoDASH()
    {
        // impulso en la direccion del movimiento
        Vector3 dashDirection = (transform.right * _movLateral + transform.forward * _movFrontal).normalized;
        if (dashDirection == Vector3.zero)
        { // sin dirección, dash hacia adelante
            dashDirection = transform.forward; 

        }
        _rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);

        // activamos cooldown
        _isDashing = true;
        _canDash = false;
        Invoke("ResetDash", dashTimer);
    }

    private void ResetDash()
    {
        _canDash = true;
        _isDashing = false;
    }
}
