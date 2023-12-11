using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    private Animator anim;
    private int hp = 3;
   
    enum States
    {
        idle,
        run,
        attack,
        dying
    }

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent의 컴포넌트를 가져옴
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //목적지를 타겟으로 계속 설정함
         agent.SetDestination(target.position);

        if(agent.speed != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        if(agent.remainingDistance <= 2f)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}
