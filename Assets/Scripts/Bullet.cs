using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;//총알 에셋 변수 
    public Transform SpawnPoint;//총알 복제 위치 
    public float speed;//총알 속도 변수 
    float triggerValue;//트리거 입력 변수 설정
    public float spawnTime = 0.5f;//총알 복제 시간

    //오디오 소스 변수
    //public AudioSource fireAudio;

    //입력 속성 변수
    public InputActionProperty inputTriggerActionR;
    public InputActionProperty inputTriggerActionL;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //변수 값을 설정
        triggerValue = inputTriggerActionR.action.ReadValue<float>();
        triggerValue = inputTriggerActionL.action.ReadValue<float>();

        //총알 시간 증가
        spawnTime += Time.deltaTime;
        
        if(triggerValue > 0 && spawnTime > 1f) // 트리거 입력과 spawnTime 변수이 1보다 크면 실행
        {
            //bulletObj 게임객체 생성
            GameObject bulletObj = Instantiate(bullet);
            //Pistol 게임객체 오디오 소스 플레이
            //fireAudio.Play();
            //총알 복제 위치 지정
            bulletObj.transform.position = SpawnPoint.position;
            //총알 객체를 z 방향과 속도로 운동에너지 
            bulletObj.GetComponent<Rigidbody>().velocity = SpawnPoint.forward * speed;
            //5초 후 총알 객체를 파괴
            Destroy(bulletObj, 5f);

            //총알 생성 인터벌 시간 초기//트리거 입력 변수 설정
            spawnTime = 0.5f;

        }
        else if (triggerValue > 0 && spawnTime > 1f) // 트리거 입력과 spawnTime 변수이 1보다 크면 실행
        {
            //bulletObj 게임객체 생성
            GameObject bulletObj = Instantiate(bullet);
            //Pistol 게임객체 오디오 소스 플레이
            //fireAudio.Play();
            //총알 복제 위치 지정
            bulletObj.transform.position = SpawnPoint.position;
            //총알 객체를 z 방향과 속도로 운동에너지 
            bulletObj.GetComponent<Rigidbody>().velocity = SpawnPoint.forward * speed;
            //5초 후 총알 객체를 파괴
            Destroy(bulletObj, 5f);

            //총알 생성 인터벌 시간 초기//트리거 입력 변수 설정
            spawnTime = 0.5f;

        }

    }
}
