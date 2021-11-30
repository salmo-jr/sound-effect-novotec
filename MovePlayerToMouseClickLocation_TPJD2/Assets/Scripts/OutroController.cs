using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limite
{
    public float xMin, xMax;
}

public class OutroController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Limite boundary;

    private Animator anim;
    private Vector3 target;
    private float inputH, inputV;
    private float dt;

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
            anim.SetBool("movimento", false);
        }
        else
        {
            anim.SetBool("movimento", true);
        }

        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }
}
