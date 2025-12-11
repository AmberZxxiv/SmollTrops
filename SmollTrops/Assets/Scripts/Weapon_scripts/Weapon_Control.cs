using UnityEngine;

public class Weapon_Control : MonoBehaviour
{// script en el SPRITE dentro del player
 // SINGLETON script
    public static Weapon_Control instance;
 // SINGLETON script

    public WeaponType weapon;
    public enum WeaponType
    {
        None,
        Kick,
        Punch,
        Shot,
        Magic
    }

    // UI marcador
    public GameObject uiPower;
    public GameObject kickPow;
    public GameObject punchPow;
    public GameObject shotPow;
    public GameObject magicPow;
    // variables de mele
    public Transform attackOrigin;
    public float meleRange;
    public float meleDamage;
    // variables de ranged
    public GameObject shotPref;
    public GameObject magicPref;
    public float rangedForce;
    public float rangedDamage;
    // control de cooldown
    public float attackCooldown;
    public float lastAttackTimer;

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
        // desde donde se van a generar los ataques
        if(attackOrigin == null)
        {
            attackOrigin = this.transform;
        }
    }

    void Update()
    {
        // clic IZD ataca
        if (Input.GetMouseButton(0))
        {
            AttackFunction();
        }
    }

     public void EquipWeapon(WeaponType newWeapon)
    { 
        // igualo mi weapon al del Pow_Giver
        weapon = newWeapon;
        // elimino el marcador del anterior weapon
        foreach (Transform child in uiPower.transform)
        { Destroy(child.gameObject); }
        // selecciono el weapon que voy a instanciar en la UI
        GameObject iconToInstantiate = null;
        switch (newWeapon)
        {
           case WeaponType.None:
                return;
           case WeaponType.Kick:
                iconToInstantiate = kickPow;
                break;
           case WeaponType.Punch:
                iconToInstantiate = punchPow;
                break;
           case WeaponType.Shot:
                iconToInstantiate = shotPow;
                break;
           case WeaponType.Magic:
                iconToInstantiate = magicPow;
                break;
        }
        Instantiate(iconToInstantiate, uiPower.transform);
    }

    void AttackFunction()
    {
        // creo que no funciona el contador del cooldown
        lastAttackTimer = Time.time;
        if (Time.time < lastAttackTimer + attackCooldown) return;
        // activo el ataque correspondiente al weapon equipado
        switch (weapon)
        {
        case WeaponType.None:
             return;
        case WeaponType.Kick:
                   DoKick(); break;
        case WeaponType.Punch:
                   DoPunch(); break;
        case WeaponType.Shot:
                   DoShot(); break;
        case WeaponType.Magic:
                   DoMagic(); break;
        }
    }

    void DoKick()
    {
        print("KICKED");
    }
    void DoPunch()
    {
        print("PUNCHED");
    }
    void DoShot()
    {
        print("SHOTED");
    }
    void DoMagic()
    {
        print("MAGICED");
    }
}
