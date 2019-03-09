using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int DbuffDamage_Lv1 = 5;
    public int ABuffsDamage_Lv1 = 2;
    public int HP_RecoverBuff = 5;
    public int HealthPoints = 100;
    public int ManaPoints = 0;
    public float MovementSpeed = 3f;
    public string PlayerName;
    public Rigidbody myRigidBody;
    public Joystick joystick;
    public PlayerUIs PlayerUIs;
    public PlayerConvoManager PlayerConvoM;
    public ScreenEffectsManager SEM;
    private TurretManager _mountTurret;
    private int _HealthPoints;
    private float Default_MovementSpeed;

    private void Awake()
    {
        PlayerAbilitiesManager.PAM.OnEnteringGameLevels();
    }

    private void Start()
    {
        CharacterChecks();

        Default_MovementSpeed = MovementSpeed;
        //HP
        PlayerUIs.HealthPoints = HealthPoints;

        _HealthPoints = HealthPoints;

        if(SEM == null)
        {
            SEM = Behaviour.FindObjectOfType<ScreenEffectsManager>();
        }

        //Name'
        if(Game_GlobalInfo.singleton)
        {
            PlayerName = Game_GlobalInfo.singleton.Player_Username;
        }
        else
        {
            PlayerName = "Error404";
        }
        

        PlayerUIs.PlayerName_Text.text = PlayerName;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Movements();
        }
        
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    OnTakenDamage(5);
        //}
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
        PlayerConvoM.OnPlayerConvo();

        HealthPoints -= _DamageAmount;

        SEM.ActivateScreenRed();

        #region SE
        SpecialEffectsManager _se;

        foreach (var item in GameManager.singleton.SpecialEffects_Pools)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                _se = item.GetComponent<SpecialEffectsManager>();

                if (_se.SpecialEffectsCode == 5)
                {
                    _se.OnEndPlayingSpecialEffects(transform.position);

                    break;
                }
            }
        }
        #endregion

        if (HealthPoints <= 0)
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
            HealthPoints = _HealthPoints;
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
                    if(BossManager_Level1.singleton)
                    {
                        BossManager_Level1.singleton.OnTakingDamage(ABuffsDamage_Lv1);
                    }
                    else
                    {
                        BossManager_Level2.singleton.OnTakingDamage(ABuffsDamage_Lv1);
                    }

                    ManaPoints++;

                    PlayerAbilitiesManager.PAM.OnUpdateQuantityText();

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
        MovementSpeed = Default_MovementSpeed;

        //Debug.Log(transform.name +_mountTurret.name);

        _mountTurret.DeactivateTurret();

        //_mountTurret = null;
    }
    
    public void CharacterChecks()
    {
        if(Game_GlobalInfo.singleton.Player_SelectedCharacter == 0)
        {
            HealthPoints += 25;
        }
        else if(Game_GlobalInfo.singleton.Player_SelectedCharacter == 1)
        {
            HealthPoints += 55;
        }

        Debug.Log("Character chosen = " + Game_GlobalInfo.singleton.Player_SelectedCharacter);
    }

}
