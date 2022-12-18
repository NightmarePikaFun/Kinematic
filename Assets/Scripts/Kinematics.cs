using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Kinematics : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    private SingleDegreeJoint[] joints;
    public float speed = 2;
    public float[] solution;
    //----//
    private float oldRange = 999999999;
    private Vector3[] startPos;
    [SerializeField]
    private Transform actor;

    // Start is called before the first frame update
    void Start()
    {
        joints = GetComponentsInChildren<SingleDegreeJoint>();
        solution = new float[joints.Length];
        for (var i = 0; i < joints.Length; i++)
        {
            solution[i] = joints[i].GetValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Solve();
        for (var i = 0; i < solution.Length; i++)
        {
            Debug.Log(joints[i].GetPosition());
            //joints[i].SetValue(solution[i]);
        }
    }

    private void Solve()
    {
        var delta = Time.deltaTime * speed;
        if (Mathf.Abs(Vector3.Distance(Vector3.zero,target.position))>9)
        {
            Debug.Log("I can't");
        }
        for (var i = 0; i < solution.Length; i++)
        {
            solution[i] = InRange(solution[i]+(i+1)*delta);
            
        }
    }

    private void Clac()
    {

    }

    private float InRange(float value)
    {
        while (value < 0)
        {
            value += 360;
        }

        while (value > 360)
        {
            value -= 360;
        }

        return value;
    }
}
//https://pastebin.com/sjsHLT5d