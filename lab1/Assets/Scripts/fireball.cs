using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private float speedUp;

    public Sprite newBlock;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speedUp, 0);
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("san"))
        {
            speedUp = speedUp* 0.95f;
            rigidbody2D.velocity = new Vector2(speedUp, Mathf.Abs(3));
        }
        else if (collision.gameObject.CompareTag("rot"))
        {
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("rua"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("Mushroom"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("cot"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("flower"))
        {GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("gach"))
        {GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("question"))
        {GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("bros"))
        {GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("lua"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("cau"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = newBlock;
            Destroy(gameObject, 0.1f);
        }
    }

    public void SetSpeed(float value)
    {
        speedUp = value;
        
    }
}
