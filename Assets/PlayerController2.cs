using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = .5f;
    Rigidbody2D _rig;
    public GameObject createLine;
    public float Yfactor = 1;
    public AudioClip eat_food;
    public AudioClip eat_bullet;
    public AudioSource food_music;
    public AudioSource bullet_music;
    private Animator anim;

    public Sprite[] P2_face;
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        food_music.clip = eat_food;
        bullet_music.clip = eat_bullet;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical1");
        float h = Input.GetAxis("Horizontal1");
        if (transform.position.x >= 8.3f && h>=0)
        {
            h = 0;
        }
        if (transform.position.x <= -8.3f && h<=0)
        {
            h = 0;
        }
        if (transform.position.y >= 4.2f && v>=0)
        {
            v = 0;
        }
        if (transform.position.y <= -4.2f && v<= 0)
        {
            v = 0;
        }
        Vector3 movement = new Vector3(h, v, 0);
        transform.Translate(movement * speed);
    }

    public void setangle(Vector3 _dir)
    {

        if (transform.position.x >= 8.3f && _dir.x >= 0)
        {
            _dir.x = 0;
        }
        if (transform.position.x <= -8.3f && _dir.x <= 0)
        {
            _dir.x = 0;
        }
        if (transform.position.y >= 4.2f && _dir.y >= 0)
        {
            _dir.y = 0;
        }
        if (transform.position.y <= -4.2f && _dir.y <= 0)
        {
            _dir.y = 0;
        }
        if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
        }
        if (transform.position.y > 4.2)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, transform.position.z);
        }
        if (transform.position.x < -8.3)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, transform.position.z);
        }
        if (transform.position.x >8.3)
        {
            transform.position = new Vector3(8.3f, transform.position.y, transform.position.z);
        }
        Vector3 movement = new Vector3(_dir.x, _dir.y, 0);
        transform.Translate(movement * speed);
        _rig.AddForce(movement * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            CreateLine._score += 1;
            EatFood();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            CreateLine._score -= 1;
            EatBullet();
        }
        if (collision.gameObject.tag == "BigFood")
        {
            CreateLine.co_score += 10;
            CreateLine.smile_count += 1;
            CreateLine._score += 3;
            EatBigFood();
        }
        if (collision.gameObject.tag == "BigBullet")
        {
            CreateLine.co_score -= 10;
            CreateLine.smile_count -= 1;
            CreateLine._score -= 3;
            EatBigBullet();
        }
        createLine.GetComponent<CreateLine>().UpdateScore();
    }

    public void EatFood()
    {
        food_music.Play();
        anim.SetBool("EatFood", true);
        StopCoroutine("WhenToStop");
        StartCoroutine("WhenToStop", "EatFood");
    }

    public void EatBullet()
    {
        bullet_music.Play();
        anim.SetBool("EatBullet", true);
        StopCoroutine("WhenToStop");
        StartCoroutine("WhenToStop", "EatBullet");
    }

    public void EatBigFood()
    {
        food_music.Play();
        anim.SetBool("EatFood", true);
        StopCoroutine("WhenToStop");
        StartCoroutine("WhenToStop", "EatFood");
    }

    public void EatBigBullet()
    {
        bullet_music.Play();
        anim.SetBool("EatBullet", true);
        StopCoroutine("WhenToStop");
        StartCoroutine("WhenToStop", "EatBullet");
    }

    private IEnumerator WhenToStop(string EatWhat)
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            if (t > 0.5)
            {
                StopAnimation(EatWhat);
                Debug.Log(EatWhat);
            }

            yield return null;
        }
    }

    private void StopAnimation(string eatWhat)
    {
        anim.SetBool(eatWhat, false);
        StopCoroutine("WhenToStop");
    }

    public void ChooseMyFace(int i)
    {
        GetComponent<SpriteRenderer>().sprite = P2_face[i];
    }
}
