using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public float MoveStep = 5f;
    public float RollBoots = 2f;
    private float rollTime;
    public float RollTime;
    bool RollOnce = false;

    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    public SpriteRenderer CharacterRS;
    public Vector3 moveInput;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput * MoveStep * Time.deltaTime;

        _animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.LeftShift) && rollTime <= 0)
        {
            _animator.SetBool("Roll", true);
            MoveStep += RollBoots;
            rollTime = RollTime;
            RollOnce = true;
        }
        if (rollTime > 0)
        {
            rollTime -= Time.deltaTime;
        }
        else if (RollOnce)
        {
            _animator.SetBool("Roll", false);
            MoveStep -= RollBoots;
            RollOnce = false;
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                CharacterRS.transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                CharacterRS.transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }

    public PlayerHealth playerHealth;
    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
}
