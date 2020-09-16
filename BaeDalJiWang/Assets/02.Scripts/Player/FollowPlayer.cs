using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothTimeX, smoothTimeY, smoothTimeZ;
    public Vector3 velocity;
    public GameObject player;
    public bool pause;
    float posY;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        var jump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        // Mathf.SmoothDamp 천천히 값을 증가시키는 메서드
        if(!pause)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);

            if (!jump.jumpButton)//점프하는 중이 아닐땐 항상 플레이어 Y축을 따라감
                posY = Mathf.SmoothDamp(transform.position.y, 0.0f, ref velocity.y, smoothTimeY);
            else//점프 중일때는 점프 하는 높이 만큼 화면 확대
            {
                
                if (player.transform.position.y <= 30)
                    posY = Mathf.SmoothDamp(-transform.position.y, -player.transform.position.y, ref velocity.y, smoothTimeY);
                else
                    posY = Mathf.SmoothDamp(-transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            }
                

            float posZ = Mathf.SmoothDamp(transform.position.z, player.transform.position.z, ref velocity.z, smoothTimeZ);

            // 카메라로 이동

            transform.position = new Vector3(posX, posY + 40, posZ);
        }
    }
}
