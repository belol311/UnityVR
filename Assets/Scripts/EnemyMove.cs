using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public Animator anim;
    public AudioSource RunAudio;
    public AudioSource DyingAudio;
    public AudioSource AttackAudio;

    [HideInInspector]
    public int hp = 3;
   
    public enum States
    {
        run,
        attack,
        dying
    }

    States state;
    bool cancelWait;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent의 컴포넌트를 가져옴
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = States.run;
        StartCoroutine(StateMachine());
        //AttackAudio = GetComponent<AudioSource>();
        //RunAudio = GetComponent<AudioSource>();
        //DyingAudio = GetComponent<AudioSource>();
    }

    IEnumerator StateMachine()
    {
        while(state != States.dying)
        {
            yield return StartCoroutine(state.ToString());
            //다음 프레임까지 대기
        }
        yield return StartCoroutine(States.dying.ToString());
    }

    IEnumerator CancelableWait(float t)
    {
        cancelWait = false;
        while (t > 0 && cancelWait == false)
        {
            t -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator run()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if(curAnimStateInfo.IsName("run") == false)
        {
            anim.Play("run", -1, 0);
            RunAudio.Play();
            yield return null;
        }

        //플레이어까지의 거리가 스탑거리보다 작거나 같으면 공격 애니메이션 실행
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            ChangeState(States.attack);
        }
        
        else
        {
            //run 애니메이션의 한 사이클 동안 대기, 무슨 의미인지 파악필요
            yield return StartCoroutine(CancelableWait(0.5f));
        }
    }

    IEnumerator attack()
    {
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        anim.Play("attack", -1, 0);
        AttackAudio.Play();

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            ChangeState(States.run);
        }
        else
        {
            //attack 애니메이션의 2배 대기, 조절해서 공격 간격 조정가능
            yield return StartCoroutine(CancelableWait(curAnimStateInfo.length));
        }
    }

    IEnumerator dying()
    {
        target = null;
        anim.Play("dying", -1, 0);
        DyingAudio.Play();
        yield return null;
    }

    //상태 변경 함수
    void ChangeState(States newState)
    {
        state = newState;
    }
    
    //플레이어 감지, 피격시 hp감소, 적이 죽는 부분
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
        }
        if (hp <= 0)
        {
            ChangeState(States.dying);
            Invoke("DestroyEnemy", 5f);
        }
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (target == null) return;

        //공격 상태일 때 타겟을 바라보게 함
        if (state == States.attack)
        {
            transform.LookAt(target);
        }
        agent.SetDestination(target.position);
    }

    /*
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
    */
}
