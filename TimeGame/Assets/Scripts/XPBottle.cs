using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBottle : MonoBehaviour
{
    public float value;
    private float originalY;
    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, originalY - Mathf.Cos(Time.time * 4f) * 0.1f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<XPPlayer>().AddXP(value);
            FindObjectOfType<AudioManager>().Play("pickup");
            Destroy(gameObject);
        }
    }
}
