using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class questionBlock : MonoBehaviour
{
    private Vector2 originalPosition;
    public Sprite newBlock;
    public float speed, height;
    private bool canChange;
    public GameObject item;

    // nhạc
    private AudioSource audioSource;
    void Start()
    {
        // vị trí ban đầu
        originalPosition = transform.position;

        // chạm được 1 lần
        canChange = true;

        // nhạc
        audioSource = GetComponent<AudioSource>();
    }

    //va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = collision.GetContact(0).normal;
        if (!canChange) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (direction.y == 1)
            {
                canChange = false;
                // chuyển thành hình khác
                GetComponent<SpriteRenderer>().sprite = newBlock;
                // tắt animation
                GetComponent<Animator>().enabled = false;
                // nảy lên, rớt xuống
                StartCoroutine(UpandDown());
                // tạo đồng xu
                GameObject newItem = Instantiate<GameObject>(item);
                newItem.transform.position = originalPosition;
                StartCoroutine(ItemGoUp(newItem));
                // phát nhạt 
                PlaySound("sound/smb3_coin");
            }
        }
    }

    IEnumerator ItemGoUp(GameObject newItem)
    {
        while (true)
        {
            newItem.transform.position = new Vector2(
                newItem.transform.position.x,
                newItem.transform.position.y + speed * Time.deltaTime);
            if (newItem.transform.position.y > originalPosition.y + 0.8) break;
            yield return null;
        }
    }

    IEnumerator UpandDown()
    {
        while(true) 
        {
            transform.position = new Vector2(
                transform.position.x, 
                transform.position.y + speed * Time.deltaTime);
            if (transform.position.y > originalPosition.y + height) break;
            yield return null;
        }

        while (true)
        {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime);
            if (transform.position.y < originalPosition.y)
            {
                transform.position = originalPosition;
                break;
            }
            yield return null;
        }
    }

    // nhạc
    private void PlaySound(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }

}
