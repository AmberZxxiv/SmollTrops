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
    public float sprintMulti;
    float _movLateral;
    float _movFrontal;
    #endregion

    public GameObject startDungeon;//tp a la sala principal

    #region /// HEALTH STATUS ///
    public float health; //vida del player
    public SpriteRenderer spriteRenderer; //render del sprite
    private Color originalColor;
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
        originalColor = spriteRenderer.color;
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
        spriteRenderer.color = originalColor;
    }
}
