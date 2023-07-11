using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "Player")
        {
            Destroy(player.gameObject);
        }
    }

    private void Update() {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}
