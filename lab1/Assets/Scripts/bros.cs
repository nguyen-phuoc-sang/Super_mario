using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bros : MonoBehaviour
{
    public float left, right;
    public float speed;
    public bool isRight;
    public Animator ani;

    private bool isAlive;

    public float speedUp, height;
    private Vector2 originalPosition;

    private float timeSpawn;  // thời gian tạo viên đạn
    private float time;

    public GameObject bua;

    void Start()
    {
        isRight = false;
        isAlive = true;
        StartCoroutine(Up());


        timeSpawn = 2.5f;
        time = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        float positionX = transform.position.x;
        if (positionX < left)
        {
            isRight = true;
        }
        else if (positionX > right)
        {
            isRight = false;
        }

        Vector3 vector3;
        if (isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;

            vector3 = new Vector3(1, 0, 0);
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;

            vector3 = new Vector3(-1, 0, 0);
        }
        transform.Translate(vector3 * speed * Time.deltaTime);

        // quăng búa
        
            // chọi
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = timeSpawn;
                GameObject fb = Instantiate(bua);
                fb.transform.position = new Vector2(
                    transform.position.x + (isRight ? 1.5f : 1.5f),
                    transform.position.y + (isRight ? 1 : 1)
                    );
                fb.GetComponent<fireballboss>().SetSpeed(
                    isRight ? 10 : -10
                    );
            }
   

}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // đụng mario
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            // bị đạp
            if (Mathf.Round(direction.y) == -1)
            {
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
        // đụng đạn
        else if (collision.gameObject.CompareTag("fireBall"))
        {
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

    IEnumerator Up()
    {
        while (true)
        {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speedUp * Time.deltaTime);
            if (transform.position.y > originalPosition.y + height) break;
            yield return null;
        }
        StartCoroutine(Down());
    }
    IEnumerator Down()
    {
        bool stop = false;
        while (!stop)
        {
            stop = true;
            
        }

        while (true)
        {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y - speedUp * Time.deltaTime);
            if (transform.position.y < originalPosition.y)
            {
                transform.position = originalPosition;
                break;
            }
            yield return null;
        }

        StartCoroutine(Up());
    }
}
