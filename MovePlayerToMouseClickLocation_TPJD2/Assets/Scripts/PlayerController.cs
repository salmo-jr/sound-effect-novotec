using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary2
{
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float initialVolume = 0.4f;
    public Boundary2 boundary;

    private Animator anim;
    private AudioSource audioSource;
    private Vector3 target;
    private float lastX, lastY;
    private float inputH, inputV;
    private float dt;
    private float maiorVolume;
    private float menorVolume = 0;
    private float menorPan = -0.8f;
    private float maiorPan = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        maiorVolume = 1.0f - initialVolume;
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
            if (audioSource.isPlaying) audioSource.Stop();
        }
        else
        {
            lastX = dir.x;
            lastY = dir.y;
            anim.SetBool("Movement", true);
            audioSource.volume = initialVolume + ConvertScale(boundary.xMin, boundary.xMax, Mathf.Abs(transform.position.x), menorVolume, maiorVolume);
            audioSource.panStereo = ConvertScale(boundary.xMin, boundary.xMax, transform.position.x, menorPan, maiorPan);
            if (!audioSource.isPlaying) audioSource.Play();
        }

        anim.SetFloat("DirX", dir.x);
        anim.SetFloat("DirY", dir.y);
    }

    private float ConvertScale(float initial, float end, float value, float initialTarget, float endTarget)
    {
        return ((value - initial) / (end - initial)) * (endTarget - initialTarget) + initialTarget;
    }
}
