using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float left, right;
    public float speed;
    public bool isRight;
    public Animator ani;


    public Sprite newBlock;
    private bool isAlive;
    public float speedUp, height;
    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        isRight= false;
        ani = GetComponent<Animator>();
        isAlive=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return; // nếu chết thì đứng yên

        ani.SetBool("runningMush", true);
        ani.Play("MushroomMove1");
        Vector3 vector3;
        if (isRight)
        {
            vector3 = new Vector3(1,0,0);
        }
        else
        {
            vector3= new Vector3(-1,0,0);
        }
        transform.Translate(vector3 * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            // bị đạp
            if (Mathf.Round(direction.y) == -1)
            {
                GetComponent<SpriteRenderer>().sprite = newBlock;
                // tắt animation và chuyển động
                GetComponent<Animator>().enabled = false;
                GetComponent<BoxCollider2D>().isTrigger = true;
                isAlive = false;
                // nảy lên rớt
                originalPosition = transform.position;
                StartCoroutine(GoUp());
                //biến mất
                Destroy(gameObject, 1);
            }
        }else if (collision.gameObject.CompareTag("fireBall"))
        {
            GetComponent<SpriteRenderer>().sprite = newBlock;
            // tắt animation và chuyển động
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            isAlive = false;
            // nảy lên rớt
            originalPosition = transform.position;
            StartCoroutine(GoUp());
            //biến mất
            Destroy(gameObject, 1);
        }
    }

    IEnumerator GoUp()
    {
        while (true)
        {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speedUp * Time.deltaTime);
            if (transform.position.y > originalPosition.y + height) break;
            yield return null;
        }
    }
}
