using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int DbuffDamage_Lv1 = 5;
    public int ABuffsDamage_Lv1 = 2;
    public int HP_RecoverBuff = 5;
    public int HealthPoints = 100;
    public float MovementSpeed = 3f;
    public string PlayerName;
    public Rigidbody myRigidBody;
    public Joystick joystick;
    public PlayerUIs PlayerUIs;
    private TurretManager _mountTurret;
    private int _HealthPoints;

    private void Start()
    {
        //HP
        PlayerUIs.HealthPoints = HealthPoints;

        _HealthPoints = HealthPoints;

        //Name
        PlayerUIs.PlayerName_Text.text = PlayerName;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Movements();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnTakenDamage(5);
        }
    }

    void Movements()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical) * MovementSpeed;
        myRigidBody.AddForce(moveVector);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * Time.smoothDeltaTime, Space.World);

        }
    }

    public void OnTakenDamage(int _DamageAmount)
    {
        HealthPoints -= _DamageAmount;

        if(HealthPoints <= 0)
        {
            GameManager.singleton.Game_RoundsEnd(false);

            return;
        }

        PlayerUIs.HealthPoints = HealthPoints;

        PlayerUIs.OnHealthPointsChanged(HealthPoints,_HealthPoints,false);
    }

    public void OnRecoveringHealth()
    {
        HealthPoints += HP_RecoverBuff;

        if (HealthPoints>=_HealthPoints)
        {
            HealthPoints = 100;
        }

        PlayerUIs.OnHealthPointsChanged(HealthPoints, _HealthPoints, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "buffs")
        {
            if(collision.transform.GetComponent<BuffsManager>()!=null)
            {
                BuffsManager _db = collision.transform.GetComponent<BuffsManager>();

                if(_db.isAdvantageBuff)
                {
                    BossManager_Level1.singleton.OnTakingDamage(ABuffsDamage_Lv1);

                    OnRecoveringHealth();
                }
                
                if(_db.isDisadvantageBuff)
                {
                    OnTakenDamage(DbuffDamage_Lv1);
                }

                _db.OnDeactivation();        
            }
        }
    }

    public void MountTurret(Vector3 position, TurretManager _turret)
    {
        MovementSpeed = 0;

        transform.position = position;

        GameManager.singleton.OnMountingTurret();

        _mountTurret = _turret;

        _mountTurret.ActivateTurret();
    }

    public void DismountTurret()
    {
        MovementSpeed = 3;

        _mountTurret.DeactivateTurret();
    }
}
