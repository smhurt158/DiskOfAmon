using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlockBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    int width, height;
    AttackType at;
    Vector3 startPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        width = FireWallInfo.Width;
        height = FireWallInfo.Height;
        at = FireWallInfo.AttackType;
        rb.mass = Mathf.Sqrt(width * height) * 3;
        gameObject.transform.localScale =  new Vector3(width, height, 2) / 2;
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (gameObject.transform.position.x < -1.75f || gameObject.transform.position.x > 5.6 || gameObject.transform.position.y > 2.72 || gameObject.transform.position.y < -2.79)
        {
            gameObject.transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            FireWallInfo.OnSuccess(at);
        }
        else
        {
            gameObject.transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }
}
