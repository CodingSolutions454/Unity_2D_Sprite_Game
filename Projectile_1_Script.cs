using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_1_Script : MonoBehaviour
{
    // Int references 
    [SerializeField]
    int Damage;

    // Float refernces
    [SerializeField]
    float Speed;
    [SerializeField]
    float DestroyThis = 3;

    // Spawn projectile at direction player is facing
    public void OnSpawn(bool IsFacingLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        if (IsFacingLeft)
        {
            // Rotate this projectile 180 degrees if facing left
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb2d.velocity = new Vector3(-Speed, 0);
        }
        else
        {
            // Or retain the same rotation if we are not facing left
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb2d.velocity = new Vector3(Speed, 0);
        }  
        
        // destroy this actor after 3 seconds.
        Destroy(gameObject, DestroyThis);
    }
}
