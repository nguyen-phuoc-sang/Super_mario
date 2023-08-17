using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class fireballboss : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float speedUp;

    // Start is called before the first frame update
    void Start()
    {
        Rotation();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speedUp, 0);
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cot"))
        {
            Destroy(gameObject, 0);
        }
    }

    public void SetSpeed(float value)
    {
        speedUp = value;

    }

    // xoay hình
    public void Rotation()
    {
        Vector2 scale = transform.localScale;
        if (speedUp > 0)
        {          
            scale.x *= scale.x > 0 ? 1 : -1;           
        }
        else
        {
            scale.x *= scale.x > 0 ? -1 : 1;          
        }
        transform.localScale = scale;
    } 
}
