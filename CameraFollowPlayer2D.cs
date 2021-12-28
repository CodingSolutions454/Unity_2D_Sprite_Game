using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2D : MonoBehaviour
{
    // Movement references
    [SerializeField]
    GameObject Player;
    [SerializeField]
    float SpeedOffSet;
    [SerializeField]
    Vector2 PositionOffSet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        // The camerea position
        Vector3 startPos = transform.position;

        // The player psoition
        Vector3 endPos = Player.transform.position;
        endPos.x += PositionOffSet.x;
        endPos.y += PositionOffSet.y;
        endPos.z = -10;

        // Follow main character, target is the player
        transform.position = Vector3.Lerp(startPos, endPos, SpeedOffSet * Time.deltaTime);

        // Follow main character, target is the player. This is the more simple way of having the camera follow the player. Camera is fixed to centre with no lerp delay
       // transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }
}
