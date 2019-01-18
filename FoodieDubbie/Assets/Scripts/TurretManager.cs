using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
    public Image BatteryBar;

    public Gradient BatteryGradientColor;

    public Transform Seat;

    public Transform ShootPoint1;

    public Transform ShootPoint2;

    public PlayerManager PlayerManage;

    public bool isActivated;

    public bool isRecharging;

    public float BatteryLife = 10;

    public float RechargingSpeed = 3.5f;

    public int TurretDamage=1; 

    [Range(0,10)]
    public float LoopTime = 1f;

    private float _looptime;

    private float _batterylife;

    private void Start()
    {
        _looptime = LoopTime;
        _batterylife = BatteryLife;

        Color32 c = BatteryGradientColor.Evaluate(1);

        BatteryBar.color = c;
    }

    private void Update()
    {
        if(isRecharging)
        {
            float i = Mathf.Clamp((_batterylife / BatteryLife), 0f, 1f);

            Color32 c = BatteryGradientColor.Evaluate(i);

            BatteryBar.color = c;

            if(_batterylife >= BatteryLife)
            {
                isRecharging = false;
            }
            else
            {
                _batterylife += Time.deltaTime/RechargingSpeed;
            }
        }

        if(isActivated)
        {
            if (_looptime > 0)
            {
                _looptime -= Time.deltaTime;
            }
            else
            {
                _batterylife -= 1;

                float i = Mathf.Clamp((_batterylife / BatteryLife), 0f, 1f);

                Color32 c = BatteryGradientColor.Evaluate(i);

                BatteryBar.color = c;

                if (_batterylife<=0)
                {
                    GameManager.singleton.OnDismountTurret();
                }
                else
                {
                    SpawnSpecialEffects();

                    if(BossManager_Level1.singleton != null)
                    {
                        BossManager_Level1.singleton.OnTakingDamage(TurretDamage);
                    }
                    else if(BossManager_Level2.singleton != null)
                    {
                        BossManager_Level2.singleton.OnTakingDamage(TurretDamage);
                    }

                    _looptime = LoopTime;
                }
            }
        }
    }

    void SpawnSpecialEffects()
    {
        SpecialEffectsManager _se;

        foreach (var item in GameManager.singleton.SpecialEffects_Pools)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                _se = item.GetComponent<SpecialEffectsManager>();

                if (_se.SpecialEffectsCode == 3)
                {
                    _se.OnEndPlayingSpecialEffects(ShootPoint1.transform.position);

                    break;
                }
            }
        }
    }

    void Recharge()
    {
        //Debug.Log("A");
        isRecharging = true;
    }

    public void ActivateTurret()
    {
        //Debug.Log("Activate Turret");

        isActivated = true;
    }

    public void DeactivateTurret()
    {
        //Debug.Log("Deactivate Turret");
        //PlayerManage = null;

        isActivated = false;

        Recharge();

    }

    void MountTurret()
    {
        Debug.Log(PlayerManage +  " " + Seat);
        PlayerManage.MountTurret(Seat.position, this);

        GameManager.singleton.Button_Mount.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_batterylife >= BatteryLife)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerManage = other.GetComponent<PlayerManager>();

                GameManager.singleton.Button_Mount.gameObject.SetActive(true);

                GameManager.singleton.Button_Mount.onClick.AddListener(MountTurret);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_batterylife >= BatteryLife)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerManage = null;

                GameManager.singleton.Button_Mount.gameObject.SetActive(false);

                GameManager.singleton.Button_Mount.onClick.RemoveListener(MountTurret);
            }
        }
    }
}
