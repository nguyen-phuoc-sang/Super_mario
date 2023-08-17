using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class brick : MonoBehaviour
{
    public UnityEvent _event;
    public GameObject Bom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            if (Mathf.Round(direction.y) == 1) 
            {
                _event.Invoke();
                // tạo hiệu ứng
                GameObject go = Instantiate(Bom, transform.position,Quaternion.identity);
                Destroy(go, 0.7f);
                Destroy(gameObject,0);
            }
        }
    }
}
