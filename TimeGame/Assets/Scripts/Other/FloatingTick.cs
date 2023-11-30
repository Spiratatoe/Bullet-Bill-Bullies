using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTick : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private TextMeshProUGUI tickText;

    public float initialYVelocity = 7f;
    public float initialXVelocityRange = 3f;
    public float lifeTime = 0.8f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        rb.velocity = new Vector2(Random.Range(-initialXVelocityRange, initialXVelocityRange), initialYVelocity);
        Destroy(gameObject, lifeTime);
    }

    public void SetMessage(string msg) {
        tickText.text = msg;
    }

}
