using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    Transform _items;
    public float items_speed;
    // Start is called before the first frame update
    void Start()
    {
        _items = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _items.Translate(items_speed * Vector3.left);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Destroy(this.gameObject);
    }
}
