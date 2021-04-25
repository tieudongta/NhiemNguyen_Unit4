﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rbEnemy;
    GameObject player;
    public float speed = 4.0f;
    private float yBoundary = -15.0f;
    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yBoundary)
        {
            Destroy(gameObject);
        }
        Vector3 seekDirection = (player.transform.position - transform.position).normalized;
        rbEnemy.AddForce(seekDirection * speed * Time.deltaTime);
    }
}
