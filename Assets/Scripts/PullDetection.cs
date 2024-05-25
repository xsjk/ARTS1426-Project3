using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PullDetection : MonoBehaviour
{
    [SerializeField]
    Transform[] blockTransforms = new Transform[4];

    [SerializeField]
    TextMeshPro[] textMeshPro = new TextMeshPro[4];

    [SerializeField]
    float pullThreshold = 0.2f;

    [HideInInspector]
    public float[] distances = new float[4];
    [HideInInspector]
    public bool[] isPulling = new bool[4];

    public UnityEvent[] OnPullBegin = new UnityEvent[4];
    public UnityEvent[] OnPullEnd = new UnityEvent[4];

    void Awake() {
        for (int i = 0; i < 4; i++)
            if (blockTransforms[i] == null)
                Debug.LogError("Block " + i + " is not assigned.");
    }

    void UpdateDistances() {
        /*  p1  p2
            p3  p4  */
        var p1 = blockTransforms[0].position;
        var p2 = blockTransforms[1].position;
        var p3 = blockTransforms[2].position;
        var p4 = blockTransforms[3].position;

        /*  d1: ↘
            d2: ↙  */
        var d1 = (p4 - p1);
        var d2 = (p3 - p2);

        var l1 = d1.magnitude;
        var l2 = d2.magnitude;

        d1 /= l1;
        d2 /= l2;

        var n = Vector3.Cross(d1, d2);

        var a = Vector3.Dot(d1, d2);
        var p1p2 = p2 - p1;
        
        var c1 = Vector3.Dot(p1p2, d1);
        var c2 = Vector3.Dot(p1p2, d2);

        distances[0] = (a * c2 - c1) / (a * a - 1);
        distances[1] = (c2 - a * c1) / (a * a - 1);
        distances[2] = l2 - distances[1];
        distances[3] = l1 - distances[0];

        

        var center = p1 + d1 * distances[0];
        transform.position = center;
        transform.rotation = Quaternion.LookRotation(d1 - d2, n);


        for (int i = 0; i < 4; i++) {
            if (isPulling[i] && distances[i] <= pullThreshold) {
                isPulling[i] = false;
                OnPullEnd[i]?.Invoke();
            } else if (!isPulling[i] && distances[i] > pullThreshold) {
                isPulling[i] = true;
                OnPullBegin[i]?.Invoke();
            }
            
            textMeshPro[i].text = distances[i].ToString("F2");
        }
    }

    void Update()
    {
        UpdateDistances();
    }
}
