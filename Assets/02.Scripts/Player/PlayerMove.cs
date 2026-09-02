using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    public float Speed = 0.05f;
    // e키 , 업다운 량, 
    public RectTransform restrictArea;
    private void Start()
    {
        
    }

    private void SpeedUp()
    {
        Speed += 0.03f;
    }
    private void SpeedDown()
    {
        Speed -= 0.03f;
    }

    private void Update()
    {
        //Vector2 direction = new Vector2(-1, 0);
        //1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Q))
        {
            SpeedUp();
        }

        if (Input.GetKey(KeyCode.E))
        {
            SpeedDown();
        }

        Vector2 direction = new Vector2(h, v).normalized;
        //Debug.Log("왼쪽 방향키를 누르는 중");
        //매직 넘버란? : 보는 사람에 따라 의미가 달라질 수 있는 숫자 
        transform.Translate(direction * Speed * Time.deltaTime);

       
            Vector3 currentPos = transform.position;
            // UI 이미지의 네 모서리 월드 좌표를 가져옵니다.
            Vector3[] corners = new Vector3[4];
            restrictArea.GetWorldCorners(corners);

         
            float minX = corners[0].x;
            float maxX = corners[2].x;
            float minY = corners[0].y;
            float maxY = corners[2].y;
            
            if (currentPos.x > maxX)
            {
                currentPos.x = minX;
            }
            // 세모가 왼쪽 끝(minX)보다 더 나가면 오른쪽 끝(maxX)으로 이동
            else if (currentPos.x < minX)
            {
                currentPos.x = maxX;
            }

           
            currentPos.y = Mathf.Clamp(currentPos.y, minY , maxY );
            
            transform.position = currentPos;
        
    }
}