using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMove2 : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;
    private bool isRight;
    public bool isJump;
    public Animator ani;

    private AudioSource audioSource;

    // điểm
    private int coin;
    public Text coinText;

    // thời gian
    private int time;
    public Text timeText;
    private bool isAlive;

    public Sprite newBlock;

    // viên đạn
    public GameObject fireBall;

    // menu
    public bool isMenu;
    public GameObject menu;
    public Text score;

    // số mạng
    private int life;
    public Text textLife;

    private Vector2 originalPosition;

    // chạy khói
    public ParticleSystem dust;

    // win

    public bool win;
    public GameObject thanks;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isRight = true;
        isJump = false;
        audioSource = GetComponent<AudioSource>();

        //tiền
        coin = 0;
        //kiểm tra ocnf sống k, còn thì time chạy tiếp
        isAlive = true;
        time = 0;
        timeText.text = time + "s";
        StartCoroutine(UpdateTime());

        // hiện menu
        isMenu = false;

        // số mạng
        life = 3;
        textLife.text = "x " + life;

        // vị trí ban đầu 
        originalPosition = transform.localPosition;

        // win
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("jumping", false);

        if (Input.GetKey(KeyCode.RightArrow))
        {

            //animation
            isRight = true;
            ani.SetBool("running", true);
            ani.Play("run");
            dust.Play(true);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                dust.Play(true);
            }
            else
            {
                dust.Play(false);
            }

            transform.Translate(Time.deltaTime * 5, 0, 0);
            transform.localScale = new Vector3(6F, 5F, 1);
        }
        else
        {
            ani.SetBool("running", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            //animation
            isRight = false;
            ani.SetBool("running", true);
            ani.Play("run");
            dust.Play(true);


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dust.Play(true);
            }
            else
            {
                dust.Play(false);
            }

            transform.Translate(-Time.deltaTime * 5, 0, 0);
            transform.localScale = new Vector3(-6F, 5F, 1F);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlaySound("sound/smb3_jump");
            isJump = true;
            ani.SetBool("running", false);
            ani.Play("jump");

            if (isRight == true)
            {
                transform.Translate(0, Time.deltaTime * 8, 0);
            }
            else
            {
                transform.Translate(0, Time.deltaTime * 8, 0);
            }
        }

        // bắn đạn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject fire = Instantiate(fireBall);
            fire.transform.position = new Vector3(
                transform.position.x + (isRight ? 0.6f : -0.6f),
                transform.position.y,
                transform.position.z);
            fire.GetComponent<fireball>().SetSpeed(isRight ? 12 : -12);
        }

        // hiện menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenu)
            {
                isMenu = true;
                menu.SetActive(true);
                Time.timeScale = 0;
                score.text = "Your Score " + coin;
            }
            else
            {
                isMenu = false;
                menu.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }
    // bắt sự kiện 2 box chạm nhau
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lua"))
        {
            Time.timeScale = 0;
            lifeCheck();
            Time.timeScale = 1;
        }
        else if (collision.gameObject.CompareTag("Mushroom"))
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (direction.x == 1 || direction.x == -1)
            {
                Time.timeScale = 0;
                lifeCheck();

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
            }
        }
        else if (collision.gameObject.CompareTag("flower"))
        {
            Time.timeScale = 0;
            lifeCheck();
            Time.timeScale = 1;
        }
        else if (collision.gameObject.CompareTag("bros"))
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (direction.x == 1 || direction.x == -1)
            {
                Time.timeScale = 0;
                lifeCheck();

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
            }
        }
        else if (collision.gameObject.CompareTag("bua"))
        {
            Time.timeScale = 0;
            lifeCheck();
            Time.timeScale = 1;
        }
        else if (collision.gameObject.CompareTag("danboss"))
        {
            Time.timeScale = 0;
            lifeCheck();
            Time.timeScale = 1;
        }
        else if (collision.gameObject.CompareTag("congchua"))
        {
            win = true;
            thanks.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // bắt sự kiện va chạm is trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Coin"))
        {
            // phát nhạc
            PlaySound("sound/smb3_coin");
            // biến mất
            Destroy(collision.gameObject);
            // tăng điểm
            coin++;
            coinText.text = coin + " x";
        }
        else if (collision.gameObject.CompareTag("boss"))
        {
            /*Time.timeScale = 0;
            Destroy(GameObject.Find("player"), 0);

            if (Time.timeScale == 0)
            {
                SceneManager.LoadScene("man2");
                Time.timeScale = 1;
            }*/
            Time.timeScale = 0;
            lifeCheck();
            Time.timeScale = 1;
        }
    }

    // phát nhạc
    private void PlaySound(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }

    // cập nhập thời gian
    IEnumerator UpdateTime()
    {
        while (isAlive)
        {
            time++;
            timeText.text = time + "s";
            yield return new WaitForSeconds(1);
        }
    }

    public void Quit()
    {
        Application.Quit();  // chỉ chạy trong product
    }

    public void Resume()
    {
        isMenu = false;
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Next()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void lifeCheck()
    {
        if (life >= 1)
        {
            life--;
            textLife.text = "x " + life;
            transform.localPosition = originalPosition;
        }
        else if (life == 0)
        {
            SceneManager.LoadScene("man2");
        }
    }
}
