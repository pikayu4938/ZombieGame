using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private NavMeshAgent nav;

    private Animator animator;

    [SerializeField]
    private GameObject player;

    private BoxCollider attackCollider;

    private float distance;
    public float hAxis, vAxis;

    Vector3 moveVec;

    private bool isDead;

    public float hp;
    private float sight = 10f;

    private float damage = 10f;
    private float timeBetAttack = 0.5f;
    void Start()
    {
        hp = 100f;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 1f;
        animator = GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider>();
    }
    
    void Update()
    {
        if (!isDead)
        {
            FollowPlayer();
            die();
        }
    }

    private void FollowPlayer()
    {
        if (player == null)
            return;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= sight)
        {
            nav.SetDestination(player.transform.position);
        }
        //animator.SetBool("isWalk", nav.remainingDistance != 0);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") { }
            //animator.SetBool("isAttack", true);
    }

    private void damaged(float damage)
    {
        hp -= damage;
    }

    private void die()
    {
        if (hp <= 0)
        {
            nav.Stop();
            isDead = true;
            animator.SetTrigger("isDead");
            
            Destroy(this.gameObject, 2.0f);
        }
    }
}
