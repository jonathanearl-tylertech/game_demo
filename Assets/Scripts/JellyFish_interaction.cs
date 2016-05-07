using UnityEngine;
using System.Collections;

public class JellyFish_interaction : MonoBehaviour {

    public float bounce_force = 40f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody2D hero_rigid = other.GetComponent<Rigidbody2D>();
            hero_rigid.velocity = new Vector3(hero_rigid.velocity.x, 0f, 0f);
            //hero_rigid.velocity = new Vector2(hero_rigid.velocity.x, 0f);
            hero_rigid.AddForce(new Vector3(hero_rigid.velocity.x, bounce_force, 0), ForceMode2D.Impulse);
        }
    }
}
