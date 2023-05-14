using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent; //enemy
    Animator anim; //animasyon
    Transform target; //oyuncu transformu 
    public bool isDead = false;
    public float turnSpeed;

    public float damage=25f;
    public bool canAttack; //dusman saldırabilir mi bana 

    [SerializeField]
    float attackTimer = 2f;


    void Start()
    {
        canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;


    }

 
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance < 10 && distance > agent.stoppingDistance && !isDead)
        {
            ChasePlayer();

        }

        else if(distance <= agent.stoppingDistance && canAttack && !PlayerHealth.PH.isDead)
        {

            agent.updateRotation = false;
            Vector3 direction = target.position - transform.position; // dusmanın saldırırken bize bakması 
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

            agent.updatePosition = false;
            anim.SetBool("isWalking", false); // yürümesin
            anim.SetBool("Attack", true); //saldırsın
        }

        else if (distance >10)
        {
            StopChase();
        }

    }

    void StopChase()
    {
        agent.updatePosition = false;
        anim.SetBool("isWalking", false); // yürümesin
        anim.SetBool("Attack", false); //saldırsın

    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true; //pozisyonu guncelle
        agent.SetDestination(target.position); //oyuncuya dogru gelecek
        anim.SetBool("isWalking", true); // yürüsün
        anim.SetBool("Attack", false); //saldırmasın
    }

    void AttackPlayer()
    {

        PlayerHealth.PH.DamagePlayer(damage);

        // StartCoroutine(AttackTime());


    }
    public void Hurt()
    {
        agent.enabled = false;
        anim.SetTrigger("Hit");
        StartCoroutine(Nav());

    }

    public void DeadAnim()
    {
        isDead = true;
        anim.SetTrigger("Dead");

    }

    IEnumerator Nav()
    {
        yield return new WaitForSeconds(1.5f);
        agent.enabled = true;
    }

    //IEnumerator AttackTime()
    //{
    //    canAttack = false;
    //    yield return new WaitForSeconds(0.5f);
    //    PlayerHealth.PH.DamagePlayer(damage);
    //    yield return new WaitForSeconds(attackTimer);
    //    canAttack = true;
       
    //}
}
