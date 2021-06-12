using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CreateLine : MonoBehaviour
{
    public Transform _pa;
    public Transform _kid;
    private Transform _line;
    Vector2 _center;
    public float distanceFactor = 1;
    public float thicknessFactor = 1;
    private float _angle;
    private float Dir_angle;
    public static int _score;
    public static int co_score;
    public static int smile_count;
    string s_score;
    public GameObject scoreText;
    public GameObject FinalScoreText;

    public GameObject CoScoreText;
    public GameObject FinalCoScoreText;

    public GameObject Player1;
    public GameObject Player2;



    // Start is called before the first frame update
    void Start()
    {
        _line = this.transform;
        _angle = Vector2.Angle(Vector2.right, _pa.position - _kid.position);
        _line.Rotate(0, 0, _angle);
    }

    // Update is called once per frame
    void Update()
    {
        _center = (_pa.position + _kid.position)/2;
         _line.position = _center;
        _angle = Vector2.Angle(Vector2.right, _pa.position - _kid.position);
        //Dir_angle = Vector2.up ( _pa.position - _kid.position);
        Dir_angle = Vector2.Dot(Vector2.up, _pa.position - _kid.position);

        if (Dir_angle >= 0)
        {
            _line.rotation = Quaternion.Euler(0, 0, _angle);
        }
        if (Dir_angle < 0)
        {
            _line.rotation = Quaternion.Euler(0, 0, -_angle);
        }

        _line.localScale = new Vector3(Vector2.Distance(_pa.position, _kid.position)*distanceFactor, thicknessFactor / Vector2.Distance(_pa.position, _kid.position) * distanceFactor, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       if(collision.gameObject.tag == "Food")
        {
            _score+=1;
            Player1.GetComponent<PlayerController1>().EatFood();
            Player2.GetComponent<PlayerController2>().EatFood();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            _score -=1;
            Player1.GetComponent<PlayerController1>().EatBullet();
            Player2.GetComponent<PlayerController2>().EatBullet();
        }
        if (collision.gameObject.tag == "BigFood")
        {
            co_score += 10;
            smile_count += 1;
            Player1.GetComponent<PlayerController1>().EatBigFood();
            Player2.GetComponent<PlayerController2>().EatBigFood();

        }
        if (collision.gameObject.tag == "BigBullet")
        {
            co_score -= 10;
            smile_count -= 1;
            Player1.GetComponent<PlayerController1>().EatBigBullet();
            Player2.GetComponent<PlayerController2>().EatBigBullet();
        }

        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.GetComponent<TMP_Text>().text = _score.ToString();
        FinalScoreText.GetComponent<TMP_Text>().text = _score.ToString();
        CoScoreText.GetComponent<TMP_Text>().text = co_score.ToString();
        FinalCoScoreText.GetComponent<TMP_Text>().text = co_score.ToString();
    }
}
