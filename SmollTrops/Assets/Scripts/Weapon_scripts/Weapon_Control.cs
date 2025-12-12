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

    #region /// UI MARKERS ///
    public GameObject uiPower;
    public GameObject kickPow;
    public GameObject punchPow;
    public GameObject shotPow;
    public GameObject magicPow;
    #endregion

    // variables de mele
    public Transform attackOrigin;
    public float meleRange;
    public float meleDamage;
    // variables de ranged
    public GameObject shotPref;
    public GameObject magicPref;
    public float rangedForce;
    public float rangedDamage;

    #region /// COOLDOWN CONTROL ///
    public float attackCooldown;
    public float lastAttackTimer;
    #endregion

    void Awake()
    {// awake para instanciar singleton sin superponer varios
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
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
           case WeaponType.None: return;
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
        // controlo el cooldown
        if (Time.time < lastAttackTimer + attackCooldown) return;
        lastAttackTimer = Time.time;
        // activo el ataque correspondiente al weapon equipado
        switch (weapon)
        {
        case WeaponType.None: return;
        case WeaponType.Kick: DoKick(); break;
        case WeaponType.Punch:DoPunch();break;
        case WeaponType.Shot: DoShot(); break;
        case WeaponType.Magic:DoMagic();break;
        }
    }

    void DoKick()
    {
        // ray desde cam al plano del origen del ataque
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, attackOrigin.position.y, 0));
        if (!plane.Raycast(ray, out float enter)) return;
        Vector3 mouseWorld = ray.GetPoint(enter);

        // dirección desde origen hacia posición del ratón
        Vector3 dir = mouseWorld - attackOrigin.position;
        dir.y = 0; dir.Normalize();

        // marco limites del rectangulo en anchura, altura y largura
        Vector3 halfExtents = new Vector3(1.2f, 1.2f, meleRange);
        // marco centro a mitad de la longitud hacia delante
        Vector3 attackCenter = attackOrigin.position + dir * meleRange;
        attackCenter.y = 0;
        // marco rotacion del box que apunte a dir
        Quaternion attackRot = Quaternion.LookRotation(dir, Vector3.up);
        // genero el collider con todos los datos e impacto
        Collider[] hits = Physics.OverlapBox(attackCenter, halfExtents, attackRot);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("enemy"))
            {
                print("KICKED!");
                hit.GetComponent<Enemy_Control>().TakeDamage(meleDamage);
                Vector3 forceDir = dir;
                forceDir.y = 0.3f; // push pa arriba
                forceDir.Normalize();
                hit.GetComponent<Rigidbody>().AddForce(dir * 25f, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackOrigin == null) return;

        // mismo cálculo para dibujar la zona en el viewer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, attackOrigin.position.y, 0));
        if (!plane.Raycast(ray, out float enter)) return;
        Vector3 mouseWorld = ray.GetPoint(enter);
        Vector3 dir = mouseWorld - attackOrigin.position;
        dir.y = 0; dir.Normalize();
        Vector3 halfExtents = new Vector3(1.2f, 1.2f, meleRange);
        Vector3 attackCenter = attackOrigin.position + dir * meleRange;
        attackCenter.y = 0;
        Quaternion attackRot = Quaternion.LookRotation(dir, Vector3.up);

        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(attackCenter, attackRot, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);
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
