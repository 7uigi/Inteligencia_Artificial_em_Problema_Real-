using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (!Mathf.Approximately(0, transform.position.x))
            transform.rotation = transform.position.x > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }
}
