using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform _Bg;
    public float BgMoveSpeed;
    void Start()
    {
        _Bg = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

        _Bg.Translate(BgMoveSpeed*Vector3.left);

    }
}
