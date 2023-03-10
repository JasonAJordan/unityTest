using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    // public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;

    public Transform powerup;
    AudioSource audio;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {

        if (gm.gameOver) {
            rb.velocity = Vector2.zero;
            return;
        } 
        if (!gm.inPlay){
            transform.position = paddle.position;
        }    

        if (Input.GetButtonDown("Jump") && !gm.inPlay) {
            gm.inPlay = true;
            rb.AddForce (Vector2.up * speed);

        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("bottom")) {
            Debug.Log("Ball hit the bottom of the screen");
            rb.velocity = Vector2.zero;
            gm.inPlay = false; 
            gm.UpdateLives(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.transform.CompareTag("brick")){
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();

            if (brickScript.hitsToBreak > 1) {
                brickScript.BreakBrick();
            } else {
                int randChance = Random.Range(1,101);
                if(randChance < 50){
                    Instantiate(powerup, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gm.UpdateScore(brickScript.points );
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }

            audio.Play();
        }
    } 
    public void RestBall(){
        gm.inPlay = false;
    }
}
