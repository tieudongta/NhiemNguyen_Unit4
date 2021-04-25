using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float powerUpSpeed = 10.0f;
    Rigidbody rbPlayer;
    Renderer rdPlayer;
    GameObject focalPoint;
    bool hasPowerUp = false;
    public GameObject powerUpInd;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rdPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Force);
        if (forwardInput > 0)
        {
            rdPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        }
        else {
            rdPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
        }
        powerUpInd.transform.position = transform.position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
            powerUpInd.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with " + collision.gameObject + " with powerup set to " + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirection = collision.gameObject.transform.position - transform.position;
            rbEnemy.AddForce(awayDirection * powerUpSpeed, ForceMode.Impulse);
        }
    }
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerUpInd.SetActive(false);
    }
}
