using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyAI_Melee_v1 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolIndex;
    public float chaseRadius = 10f;
    public float attackRadius = 2f;
    public float attackCooldown = 2f;
    public float rotationSpeed = 5f;
    public float moveSpeed = 3f;
    public GameObject meleeWeapon;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;

    private enum State
    {
        Patrol,
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
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Patrol;
        currentPatrolIndex = 0;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        UpdateFacingDirection(patrolPoints[currentPatrolIndex].position - transform.position);
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Attack:
                if (!isCooldown) StartAttack();
                break;
            case State.Dead:
                break;
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
        {
            currentState = State.Chase;
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            UpdateFacingDirection(patrolPoints[currentPatrolIndex].position - transform.position);
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            currentState = State.Attack;
        }
        else
        {
            agent.SetDestination(player.position);
            UpdateFacingDirection(player.position - transform.position);
        }
    }

    private void StartAttack()
    {
        isCooldown = true;
        //animator.SetTrigger("Attack");
        StartCoroutine(AttackCooldown());
    }

    public void AttackAnimationFinished()
    {
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
        transform.rotation = facingRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -180, 0);
    }

    private void UpdateFacingDirectionPartol(Vector3 direction)
    {
        facingRight = direction.x >= 0;
        transform.rotation = facingRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -180, 0);
    }
}