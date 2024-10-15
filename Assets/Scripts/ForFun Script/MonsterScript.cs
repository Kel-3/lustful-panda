using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{

    [SerializeField]
    Transform _target;
    [SerializeField]
    Transform _headBone;

    [SerializeField]
    float _headMaxTurnAngle;
    [SerializeField]
    float _headTracingSpeed;

    private void LateUpdate()
    {
        HeadTracing();
    }

    void HeadTracing()
    {
        Quaternion currentLocalRotation = _headBone.localRotation;

        _headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = -(_target.position - _headBone.position);
        Vector3 targetLocalLookDir = _headBone.InverseTransformDirection(targetWorldLookDir);

        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * _headMaxTurnAngle,
            0
            );

        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        _headBone.localRotation = Quaternion.Slerp(
            currentLocalRotation,
            targetLocalRotation,
            1 - Mathf.Exp(-_headTracingSpeed * Time.deltaTime)
            );

    }
}
