using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Animator anim;
    private float lastX, lastY;
    private float inputH, inputV;
    private float dt;
    private Vector3 target;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
        target = Calculate(inputH, inputV, dt);
        transform.position += target;

        UpdateAnimation(target);
    }

    public Vector3 Calculate(float h, float v, float deltaTime)
    {
        float dirH = h * moveSpeed * deltaTime;
        float dirV = v * moveSpeed * deltaTime;

        return new Vector3(dirH, dirV, 0);
    }

    private void UpdateAnimation(Vector3 dir)
    {
        if (dir.x == 0f && dir.y == 0f)
        {
            anim.SetFloat("LastX", lastX);
            anim.SetFloat("LastY", lastY);
            anim.SetBool("Movement", false);
        }
        else
        {
            lastX = dir.x;
            lastY = dir.y;
            anim.SetBool("Movement", true);
        }

        anim.SetFloat("DirX", dir.x);
        anim.SetFloat("DirY", dir.y);
    }
}
