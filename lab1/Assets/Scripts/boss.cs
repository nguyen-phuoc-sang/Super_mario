using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public float left, right;
    public float speed;
    public bool isRight;

    public GameObject bossfireball;
    private float timeSpawn;  // thời gian tạo viên đạn
    private float time;

    private float hp;
    // Start is called before the first frame update
    void Start()
    {
        timeSpawn= 2.5f;
        time = timeSpawn;
        hp = 20;
    }

    // Update is called once per frame
    void Update()
    {
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

        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = timeSpawn;
            GameObject fb = Instantiate(bossfireball);
            fb.transform.position = new Vector2(
                transform.position.x + (isRight ? 0.8f : -0.8f),
                transform.position.y
                );
            fb.GetComponent<fireballboss>().SetSpeed(
                isRight ? 8 : -8
                );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fireBall"))
        {
            hp--;
            if (hp == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lua"))
        {
            Destroy(gameObject);
        }
    }
}
