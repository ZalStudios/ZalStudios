using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{

    public _Muscle[] muscles;

    public bool Right;
    public bool Left;

    public Rigidbody2D rbRIGHT;
    public Rigidbody2D rbLEFT;

    public Vector2 WalkRightVector;
    public Vector2 WalkLeftVector;

    private float MoveDelayPointer;
    public float MoveDelay;


    // Update is called once per frame
    private void Update()
    {
        foreach (_Muscle muscle in muscles)
        {
            muscle.ActivateMuscle();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Right = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Left = true;
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Left = false;
            Right = false;
        }


        while (Right == true && Left == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Right", 0f);
            Invoke("Step2Right", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }

        while (Left == true && Right == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Left", 0f);
            Invoke("Step2Left", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
    }

    public void Step1Right()
    {
        rbRIGHT.AddForce(WalkRightVector, ForceMode2D.Impulse);
        rbLEFT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step2Right()
    {
        rbLEFT.AddForce(WalkRightVector, ForceMode2D.Impulse);
        rbRIGHT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step1Left()
    {
        rbRIGHT.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        rbLEFT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step2Left()
    {
        rbLEFT.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        rbRIGHT.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
    }
}
[System.Serializable]
public class _Muscle
{
    public Rigidbody2D bone;
    public float restRotation;
    public float force;

    public void ActivateMuscle()
    {
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime));
    }

}