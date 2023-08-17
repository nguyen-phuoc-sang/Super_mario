using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuclua : MonoBehaviour
{
    private Vector2 originalPosition;
    public float speed, height;
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(Up());
    }


    IEnumerator Up()
    {

        bool stop = false;
        while (!stop)
        {
            stop = true;
            yield return new WaitForSeconds(2.5f);
        }

        while (true)
        {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime);
            if (transform.position.y > originalPosition.y + height) break;
            yield return null;
        }
        StartCoroutine(Down());
    }
    IEnumerator Down()
    {
        /*bool stop = false;
        while (!stop)
        {
            stop = true;
            yield return new WaitForSeconds(2);
        }*/

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
        StartCoroutine(Up());
    }
}
