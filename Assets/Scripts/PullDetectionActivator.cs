using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PullDetectionActivator : MonoBehaviour
{
    PullDetection pullDetection;

    [SerializeField]
    TextMeshPro debugText;

    int targetCount = 0;

    void Awake()
    {
        pullDetection = GetComponent<PullDetection>();
        pullDetection.enabled = false;
    }

    public void OnTargetFound() {
        targetCount += 1;
        debugText.text = targetCount.ToString();
        if (targetCount == 0) {
            pullDetection.enabled = true;
        }
    }

    public void OnTargetLost() {
        targetCount -= 1;
        debugText.text = targetCount.ToString();
        if (targetCount == -4) {
            pullDetection.enabled = false;
        }
    }


}
