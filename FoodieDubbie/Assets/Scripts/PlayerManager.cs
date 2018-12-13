using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int DebuffDamage_Lv1 = 5;
    public int BuffsDamage_Lv1 = 2;
    public int HealthPoints = 100;
    public float MovementSpeed = 3f;
    public string PlayerName;
    public Rigidbody myRigidBody;
    public Joystick joystick;
    public PlayerUIs PlayerUIs;
    private TurretManager _mountTurret;

    #region auto assign
    private void OnValidate()
    {
        if(myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody>();
            Debug.Log("rigidbody is applied.");
        }

        if(joystick == null)
        {
            joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
            Debug.Log("joystick is applied.");
        } 

        if(PlayerUIs == null)
        {
            PlayerUIs = GameObject.FindGameObjectWithTag("playerui").GetComponent<PlayerUIs>();
            Debug.Log("PlayerUIs is applied.");
        }
    }
    #endregion

    private void Start()
    {
        //HP
        PlayerUIs.HealthPoints = HealthPoints;

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

        PlayerUIs.HealthPoints = HealthPoints;

        PlayerUIs.OnHealthPointsChanged();
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
                    BossManager.singleton.OnTakingDamage(BuffsDamage_Lv1);
                }
                
                if(_db.isDisadvantageBuff)
                {
                    OnTakenDamage(DebuffDamage_Lv1);
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
