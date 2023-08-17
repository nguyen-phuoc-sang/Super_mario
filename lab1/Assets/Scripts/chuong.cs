using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chuong : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(GameObject.Find("lo"));
            Destroy(GameObject.Find("daucau"));
            Destroy(GameObject.Find("1"));
            Destroy(GameObject.Find("2"));
            Destroy(GameObject.Find("3"));
            Destroy(GameObject.Find("4"));
            Destroy(GameObject.Find("5"));
            Destroy(GameObject.Find("6"));
            Destroy(GameObject.Find("7"));
            Destroy(GameObject.Find("8"));
            Destroy(GameObject.Find("9"));
            Destroy(GameObject.Find("10"));
            Destroy(GameObject.Find("12"));
            Destroy(GameObject.Find("11"));
            Destroy(GameObject.Find("13"));
        }
    }
}
