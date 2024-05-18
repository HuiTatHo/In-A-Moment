using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Tiny;

public class EnemyAI_Range : MonoBehaviour
{
    [Header("General")]
    public float chaseRadius = 10f;
    public float attackRadius = 2f;
    public float attackCooldown = 2f;
    public float rotationSpeed = 5f;
    public float moveSpeed = 3f;
    private Transform player;
    public AudioSource audioSource;
    public static int enemyRangeCount = 0;

    private EnemyHP enemyHP;

    [Header("Combat")]
    public GameObject enemyBullet;

    public Transform bulletSpawnPoint;

    public AudioClip bulletSpawnSFX;

    public AudioClip bulletHit;
    public AudioClip meleeHit;

    [Header("Animation")]
    private Animator animator;
    private NavMeshAgent agent;

    private bool isChase;
    private bool isAttack;
    private bool isDead;

    [Header("DamageFlash")]
    SkinnedMeshRenderer meshRenderer;
    Color orignColor;
    float flashTime = 0.15f;
    public Material mat;

    private enum State
    {
        Idle,
        Chase,
        Attack,
        Dead
    }


    private State currentState;
    private bool isCooldown = false;
    private bool facingRight = true;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHP = GetComponent<EnemyHP>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        orignColor = meshRenderer.material.color;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Idle;
        agent.SetDestination(transform.position);
    }

    private void Update()
    {
        SwitchStates();
        if (enemyHP.enmeyCurrentHealth <= 0)
        {
            currentState = State.Dead;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            enemyHP.TakeDamage(6);
            FLashStart();
            audioSource.PlayOneShot(bulletHit);
            Destroy(other.gameObject);

        }
        if (other.CompareTag("melee"))
        {
            if (other.GetComponent<Trail>().enabled)
            {
                enemyHP.TakeDamage(30);
                FLashStart();
                audioSource.PlayOneShot(meleeHit);
            }
            else
            {
                enemyHP.TakeDamage(11);
                FLashStart();
                audioSource.PlayOneShot(meleeHit);
            }

        }

    }

    void SwitchStates()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Attack:
                if (!isCooldown) StartAttack();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    private void Idle()
    {
        if (isDead) return;
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
        {
            currentState = State.Chase;
        }
    }

    private void ChasePlayer()
    {
        if (isDead) return;
        isChase = true;
        animator.SetBool("IsChase", isChase);
        if (Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            currentState = State.Attack;
            isChase = false;
        }
        else
        {
            agent.SetDestination(player.position);
            UpdateFacingDirection(player.position - transform.position);
        }
    }

    private void StartAttack()
    {
        if (isDead) return;
        agent.SetDestination(transform.position);
        isAttack = true;
        isCooldown = true;
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(bulletSpawnSFX);
        GameObject bullet = Instantiate(enemyBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        StartCoroutine(AttackCooldown());
    }

    private void Dead()
    {
        if (isDead) return;
        isDead = true;
        isAttack = false;
        isChase = false;
        enemyRangeCount++;
        animator.SetBool("Dead", isDead);
        ReplaceMaterials();
        StartCoroutine(DestroyEnemyAfterDelay(1f));
    }

    void FLashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        meshRenderer.material.color = orignColor;
    }

    public void AttackAnimationFinished()
    {
        isAttack = false;
        currentState = State.Chase;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isCooldown = false;
        currentState = State.Chase;
    }

    private void UpdateFacingDirection(Vector3 direction)
    {
        facingRight = direction.x >= 0;
        transform.rotation = facingRight ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
    }

    private void UpdateFacingDirectionPartol(Vector3 direction)
    {
        facingRight = direction.x >= 0;
        transform.rotation = facingRight ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        // chasa
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        // attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


    private IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
    private void ReplaceMaterials()
    {
        Material newMat = new Material(mat);
        Material[] materials = meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = newMat;
        }

        meshRenderer.materials = materials;
    }
}