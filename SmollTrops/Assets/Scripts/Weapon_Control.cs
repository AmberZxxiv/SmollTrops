using UnityEngine;

public class Weapon_Control : MonoBehaviour
{
    public WeaponType weapon;
    public enum WeaponType
    {
        Kick,
        Punch,
        Gun,
        Magic
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&(other.gameObject.CompareTag("enemy")))
        {
           //esto no va asi lelo y lo sabes jaja
        }
    }
}
