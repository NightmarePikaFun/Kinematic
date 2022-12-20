using UnityEngine;


public class Kinematics : MonoBehaviour
{
    private SingleDegreeJoint[] joints;

    public Transform target;

    public float speed = 2;
    public float[] solution;

    public float learningRate = 0.5f;
    public float distanceThreshold = 0.5f;

    private Transform actor;



    // Start is called before the first frame update
    void Start()
    {
        joints = GetComponentsInChildren<SingleDegreeJoint>();
        actor = GameObject.FindWithTag("Actor").transform;
        solution = new float[joints.Length];
        for (var i = 0; i < joints.Length; i++)
        {
            solution[i] = joints[i].GetValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        InverseKinematics(target.position);


        for (var i = 0; i < solution.Length; i++)
        {
            joints[i].SetValue(solution[i]);
        }
    }

    public float DistanceFromTarget(Vector3 target)
    {
        Vector3 point = actor.position;
        return Vector3.Distance(point, target);
    }


    public float PartialGradient(Vector3 target, int i, float samplingDistance)
    {

        float angle = joints[i].GetValue();

        float fX = DistanceFromTarget(target);
        joints[i].SetValue(angle + samplingDistance);

        float fD= DistanceFromTarget(target);

        float gradient = (fD - fX) / samplingDistance;

        joints[i].SetValue(angle);
        return gradient;
    }

    public void InverseKinematics(Vector3 target)
    {
        if (DistanceFromTarget(target) < distanceThreshold)
            return;
        

        for (int i = joints.Length - 1; i >= 0; i--)
        {
            var delta = Time.deltaTime * speed;
            float gradient = PartialGradient(target, i, delta);
            solution[i] -= learningRate * gradient;

            // solution[i] = solution[i] < joints[i].min || solution[i] > joints[i].max ? -solution[i] : solution[i];
            
            solution[i] = Mathf.Clamp(solution[i], joints[i].min, joints[i].max);

            if (DistanceFromTarget(target) < distanceThreshold)
                return;
        }
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