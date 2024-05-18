using System.Collections;
using System.Collections.Generic;
using Tiny;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody rb;
    private Animator animator;
    public AudioSource audioSource;

    public PlayerHP playerHP;


    public bool isBackpackOpen = false;


    // Move
    private bool isJumping;

    private bool isWalking;
    private bool isGrounded;
    private bool isRunning;
    private bool isAttacking = false;

    [Header("Combat")]
    public GameObject meleeWeapon;
    public AudioClip meleeAttack;

    public bool isCharging;
    public Collider meleeCollider;
    public GameObject gunWeapon;
    public GameObject bulletPrefab;



    public float bulletSpeed = 10f;
    public Transform firePos;

    public AudioClip gunSFX;

    private bool canMove = true;

    [Header("HoldingWeapon")]
    [SerializeField] private bool isMeleeMode = false;
    [SerializeField] private bool isGunMode = false;

    // Additional variable for movement control during attack



    [Header("Backpack")]
    public GameObject myBackpack;
    public Inventory playerInventory;
    public Item gun;
    public Item sword;
    public bool isGunExists = false;
    public bool isSwordExists = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        bool gunExists = CheckIfItemExists(gun);
        bool swordExists = CheckIfItemExists(sword);
        isGunExists = gunExists;
        isSwordExists = swordExists;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        OpenMyBackpck();
        PlayerMovement();
    }

    public void MeleeAttackAnimationStarted()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        canMove = false;
    }
    public void MeleeAttackAnimationFinished()
    {
        isAttacking = false;
        canMove = true;
    }






    void PlayerMovement()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveX, 0f, moveZ);
            movement.Normalize();


            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {

            //     isMeleeMode = !isMeleeMode;

            //     meleeWeapon.SetActive(isMeleeMode);

            //     animator.SetBool("IsMeleeMode", isMeleeMode);
            // }

            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {

            //     isGunMode = !isGunMode;

            //     gunWeapon.SetActive(isGunMode);

            //     animator.SetBool("IsGunMode", isGunMode);
            // }



            if (Input.GetKeyDown(KeyCode.Alpha1) && isSwordExists)
            {
                if (isMeleeMode)
                {
                    isMeleeMode = false;
                }
                else if (!isGunMode)
                {
                    isMeleeMode = true;
                }

                meleeWeapon.SetActive(isMeleeMode);
                gunWeapon.SetActive(isGunMode);
                animator.SetBool("IsMeleeMode", isMeleeMode);
            }

            if (Input.GetKeyDown(KeyCode.K) && playerHP.currentStamina > 0)
            {
                StartCharging();
            }

            if (Input.GetKeyUp(KeyCode.K) || playerHP.currentStamina <= 0)
            {
                StopCharging();
            }

            // if (isCharging)
            // {
            //     // 处理充能逻辑
            // }

            if (Input.GetKeyDown(KeyCode.Alpha2) && isGunExists)
            {
                if (isGunMode)
                {
                    isGunMode = false;
                }
                else if (!isMeleeMode)
                {
                    isGunMode = true;
                }

                meleeWeapon.SetActive(isMeleeMode);
                gunWeapon.SetActive(isGunMode);
                animator.SetBool("IsGunMode", isGunMode);
            }

            if (isMeleeMode && Input.GetKeyDown(KeyCode.J))
            {
                if (!isAttacking)
                {
                    audioSource.PlayOneShot(meleeAttack);
                    MeleeAttackAnimationStarted();
                    if (isCharging)
                    {
                        playerHP.currentStamina -= 200;
                    }
                }
            }

            if (isGunMode && Input.GetKeyDown(KeyCode.J))
            {
                PlaySoundEffect();
                var bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
                bullet.GetComponent<Rigidbody>().linearVelocity = firePos.forward * bulletSpeed;
            }


            if (isMeleeMode == true)
            {
                moveSpeed = 3f;
            }
            else
            {
                moveSpeed = 4f;
            }

            float currentMoveSpeed = moveSpeed;
            if (isRunning)
            {
                currentMoveSpeed *= runSpeedMultiplier;
            }

            rb.linearVelocity = new Vector3(movement.x * currentMoveSpeed, rb.linearVelocity.y, movement.z * currentMoveSpeed);

            if (movement.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            }
            else if (movement.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }

            if (isGrounded)
            {
                if (movement.magnitude > 0)
                {
                    animator.SetFloat("Speed", currentMoveSpeed);

                    if (currentMoveSpeed > moveSpeed)
                    {
                        animator.SetBool("IsRunning", true);
                    }
                    else
                    {
                        animator.SetBool("IsRunning", false);
                    }
                }
                else
                {
                    animator.SetFloat("Speed", 0f);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("Jump");
                }
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && playerHP.currentStamina > 0)
            {
                isRunning = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || playerHP.currentStamina <= 0)
            {
                isRunning = false;
            }

            if (isRunning && playerHP.currentStamina > 0)
            {
                playerHP.currentStamina -= playerHP.staminaConsumptionRate;

            }

            if (!isRunning && playerHP.currentStamina != playerHP.maxStamina)
            {
                playerHP.currentStamina += playerHP.staminaRecoverRate;

            }
        }
    }
    void OpenMyBackpck()
    {
        if (myBackpack.activeInHierarchy == false)
        {
            isBackpackOpen = false;
            //Time.timeScale = 1f;
        }
        else
        {
            isBackpackOpen = true;
            //Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isBackpackOpen = !isBackpackOpen;
            myBackpack.SetActive(isBackpackOpen);
        }

    }

    public bool CheckIfItemExists(Item itemToCheck)
    {
        return playerInventory.itemList.Contains(itemToCheck);
    }

    private void PlaySoundEffect()
    {
        if (gunSFX != null)
        {
            audioSource.PlayOneShot(gunSFX);
        }
    }

    public bool GetRunning()
    { return isRunning; }

    public bool GetWalking()
    {
        isWalking = rb.linearVelocity.magnitude > 1f;
        return isWalking;
    }


    public void ActivateMeleeCollider()
    {
        meleeCollider.enabled = true;
    }

    public void DeactivateMeleeCollider()
    {
        meleeCollider.enabled = false;
    }

    private void StartCharging()
    {
        isCharging = true;
        meleeWeapon.GetComponent<Trail>().enabled = true;
    }

    private void StopCharging()
    {
        isCharging = false;
        meleeWeapon.GetComponent<Trail>().enabled = false;
    }
}