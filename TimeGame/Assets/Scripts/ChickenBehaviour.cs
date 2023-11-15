using UnityEngine;

public class ChickenBehaviour : MonoBehaviour
{
    private float speed = 0.02f;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
