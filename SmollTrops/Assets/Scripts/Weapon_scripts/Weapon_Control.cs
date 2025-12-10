using UnityEngine;

public class Weapon_Control : MonoBehaviour
{// esto esta en el sprite dentro del player
 // singleton para llamar a este código desde cualquier otro
    public static Weapon_Control instance;

    public WeaponType weapon;
    public enum WeaponType
    {
        None,
        Kick,
        Punch,
        Shot,
        Magic
    }

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
        if(attackOrigin == null)
        {
            attackOrigin = this.transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AttackFunction();
        }
    }
     public void WeaponUp(WeaponType newWeapon)
    {
        weapon = newWeapon;
        print("Powered UP: " + weapon.ToString());
    }

    void AttackFunction()
    {
        lastAttackTimer = Time.time;
        if (Time.time < lastAttackTimer + attackCooldown) return;

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
