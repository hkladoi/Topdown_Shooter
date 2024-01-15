using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    int currentHealth;

    public HealthBar healthBar;

    public bool isDead = false;
    public UnityEvent OnDeath;

    public float SafeTime = 2f;
    private float safeTimeCoolDown;
    private Animator _animator;
    bool Playerhit = false;
    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }
    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    private void Update()
    {
        //safeTimeCoolDown -= Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    Playerhit = true;
        //    TakeDamage(20);
        //}
        if (safeTimeCoolDown > 0)
        {
            safeTimeCoolDown -= Time.deltaTime;
        }
        else if (Playerhit && GameObject.FindGameObjectWithTag("Player"))
        {
            _animator.SetInteger("hit", 0);
            Playerhit = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (safeTimeCoolDown <= 0)
        {
            Playerhit = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                _animator.SetBool("death", true);
                //OnDeath.Invoke();
            }
            else
            {
                _animator.SetInteger("hit", damage);
            }
            safeTimeCoolDown = SafeTime;
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void EnemyTakeDame(int damage)
    {
        if (safeTimeCoolDown <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {
                    Destroy(this.gameObject, 0.125f);
                }
                isDead = true;
            }
            else
            {

            }
            safeTimeCoolDown = SafeTime;
            //healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
