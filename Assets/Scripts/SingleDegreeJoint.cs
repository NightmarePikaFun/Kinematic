using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDegreeJoint : MonoBehaviour
{
    public enum JointDegree
    {
        RotateX = 0,
        RotateY = 1,
        RotateZ = 2
    }
    public Vector3 StartOffset;
    public float max=100, min=-100;
    
    public JointDegree degreeOfFreedom;
    private Vector3 _axis;
    void Start()
    {
        _axis = degreeOfFreedom switch
        {
            JointDegree.RotateX => new Vector3(1, 0, 0),
            JointDegree.RotateY => new Vector3(0, 1, 0),
            JointDegree.RotateZ => new Vector3(0, 0, 1),
            _ => _axis
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void SetValue(float value)
    {
        // value = value > max ? max : value;
        // value = value < min ? min : value;
        var transform1 = transform;
        transform1.localEulerAngles = degreeOfFreedom switch
        {
            JointDegree.RotateX => new Vector3(value, 0, 0),
            JointDegree.RotateY => new Vector3(0, value, 0),
            JointDegree.RotateZ => new Vector3(0, 0, value),
            _ => transform1.localEulerAngles
        };
    }

    void Awake ()
    {
        StartOffset = transform.localPosition;
    }
    
    public float GetValue()
    {
        return transform.localEulerAngles[(int) degreeOfFreedom];
    }

    public Vector3 GetPosithon()
    {
        return transform.position;
    }
    
}

