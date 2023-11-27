using UnityEngine;

public class ChickenBehaviour : MonoBehaviour
{
    private float speed = 0.02f;
    [SerializeField] private int damageAmount;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}
