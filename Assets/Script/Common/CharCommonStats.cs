using UnityEngine;

public class CharCommonStats : MonoBehaviour
{
    public int currHP;

    [Header("Major Stats")]
    public Stats strength;
    public Stats agility;
    public Stats intelligence;
    public Stats vitality;

    [Header("Defensive Stats")]
    public Stats health;
    public Stats armor;
    public Stats evasion;
    public Stats magicResistance;

    [Header("Damage Stats")]
    public Stats physicDamage;
    public Stats criticalRate;
    public Stats criticalDamage;

    [Header("Magic Stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    private float igniteDuration = 5f;
    public float igniteTimer;
    public GameObject igniteFXPrefabs;
    private int igniteDamage;
    private float chillDuration = 3f;
    private float chillTimer;
    private float shockDuration = 1.5f;
    private float shockTimer;
    private int igniteDamageSpeed = 1;

    public System.Action onHealthChanged;

    private EntityFX fx;

    protected virtual void Start()
    {
        currHP = SetUpCurrentHealth();
        criticalDamage.SetDefaultValue(150);
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        if(isIgnited)
        {
            HandleIgniteDuration();
        }
        if(isChilled)
        {
            HandleChillDuration();
        }
        if(isShocked)
        {
            HandleShockDuration();
        }
    }

    public virtual void HandleDamage(CharCommonStats _targetStats)
    {
        var totalDamage = 0;
        if (!CheckCanAvoid(_targetStats))
        {
            totalDamage = physicDamage.GetValue() + strength.GetValue();
            totalDamage = HandleArmor(totalDamage);
        }
        if (CheckCanCritical())
        {
            Debug.Log("Crit Damage");
            totalDamage = HandleCriticalDamage(totalDamage);
        }
        _targetStats.TakeDamageHP(totalDamage);
        //HandleMagicalDamage(_targetStats);
    }

    public virtual void HandleMagicalDamage(CharCommonStats _targetStats)
    {
        var _fireDamage = fireDamage.GetValue();
        var _iceDamage = iceDamage.GetValue();
        var _lightingDamage = lightingDamage.GetValue();
        var totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = HandleMagicalResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamageHP(totalMagicalDamage);
        CheckCanApplyAilment(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }

    public virtual void TakeDamageHP(int _damage)
    {
        DecreaseHealth(_damage);

        if (currHP <= 0)
        {
            HandleDie();
        }
    }

    protected virtual void DecreaseHealth(int _damage)
    {
        currHP -= _damage;
        if(onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void HandleDie()
    {
        Debug.Log("Player Died");
    }


    private int HandleArmor(int totalDamage)
    {
        totalDamage -= armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private int HandleMagicalResistance(CharCommonStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 2);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    private int HandleCriticalDamage(int _totalDamage)
    {
        var critDamage = criticalDamage.GetValue() + strength.GetValue();
        var totalCritDamage = _totalDamage * critDamage / 100;
        Debug.Log(totalCritDamage);
        return totalCritDamage;
    }


    private void HandleIgniteDuration()
    {
        igniteTimer += Time.deltaTime;
        if (igniteTimer > igniteDamageSpeed && isIgnited)
        {
            Debug.Log("Ignite Damage : " + igniteDamage);
            DecreaseHealth(igniteDamage);
            igniteDamageSpeed ++;
        }
        if (igniteTimer > igniteDuration)
        {
            isIgnited = false;
            igniteDamageSpeed = 1;
            igniteTimer = 0;
        }
    }

    private void HandleIgniteFX()
    {
        fx.IgniteEffect(igniteDuration);
        var igniteFX = Instantiate(igniteFXPrefabs, transform.position, Quaternion.identity);
        var igniteFXContr = igniteFX.GetComponent<IgniteFxController>();
        igniteFXContr.SetUpIgnite(transform, igniteDuration);
    }

    private void HandleChillDuration()
    {
        chillTimer += Time.deltaTime;
        if(chillTimer > chillDuration)
        {
            isChilled = false;
            chillTimer = 0;
        }
        //if isChilled true Slowdown the character
    }

    private void HandleShockDuration()
    { 
        shockTimer += Time.deltaTime;
        if(shockTimer > shockDuration)
        {
            isShocked = false;
            shockTimer = 0;
        }
        //if isShocked true Stunned the character
    }


    private bool CheckCanCritical()
    {
        int totalCriticalChance = criticalRate.GetValue() + agility.GetValue();
        if(Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private bool CheckCanAvoid(CharCommonStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    private void CheckCanApplyAilment(CharCommonStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        /*if( !canApplyIgnite && !canApplyChill && !canApplyShock )
        {
            var randomValue = Random.Range(0, 90);
            if( randomValue <= 30 )
            {
                canApplyIgnite = true;
                _targetStats.SetUpStatusAilment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Fire");
                return;
            }
            else if( 30 < randomValue && randomValue <= 60)
            {
                canApplyChill = true;
                _targetStats.SetUpStatusAilment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Chill");
                return;
            }
            else if( 60 < randomValue && randomValue <= 90)
            {
                canApplyShock = true;
                _targetStats.SetUpStatusAilment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Shock");
                return;
            }
        }*/

        if (canApplyIgnite)
        {
            _targetStats.SetUpIgniteDamage(_fireDamage * 35 / 100);
        }

        _targetStats.SetUpStatusAilment(canApplyIgnite, canApplyChill, canApplyShock);
    }


    public virtual void SetUpStatusAilment(bool _ignited, bool _chilled, bool _shocked)
    {
        if(isIgnited || isChilled || isShocked)
        {
            return;
        }
        
        if(_ignited)
        {
            isIgnited = _ignited;
            HandleIgniteFX();
        }
        isChilled = _chilled;
        isShocked = _shocked;
    }

    public virtual void SetUpIgniteDamage(int _igniteDamage) => igniteDamage = _igniteDamage;

    public virtual int SetUpCurrentHealth() => health.GetValue() + (vitality.GetValue() * 5);

}
