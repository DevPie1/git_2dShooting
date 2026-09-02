using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    public float Speed = 0.05f;
    
    
    private void Start()
    {
        
    }

    private void Update()
    {   
        //Vector2 direction = new Vector2(-1, 0);
        //1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(h, v);
        
        Vector2 normalizedSpeed = (direction * Speed).normalized;
            //Debug.Log("왼쪽 방향키를 누르는 중");
            //매직 넘버란? : 보는 사람에 따라 의미가 달라질 수 있는 숫자 
            transform.Translate(direction * normalizedSpeed*Time.deltaTime);
        
        
    }
}