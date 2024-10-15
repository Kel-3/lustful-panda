using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LegStepper : MonoBehaviour
{
    [SerializeField]
    private Transform _homeTransform;    
    [SerializeField]
    private float _wantStepAtDistance;    
    [SerializeField]
    private float _moveDuration;

    public bool Moving;

    IEnumerator MoveToHome()
    {
        Moving = true;

        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = _homeTransform.rotation;
        Vector3 endPoint = _homeTransform.position;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / _moveDuration;

            transform.position = Vector3.Lerp(startPoint, endPoint,normalizedTime);
            transform.rotation = Quaternion.Lerp(startRot, endRot,normalizedTime);

            yield return null;
        }
        while (timeElapsed < _moveDuration);

        Moving = false;

    }

    private void Update()
    {
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, _homeTransform.position);

        if (distFromHome > _wantStepAtDistance)
        {
            StartCoroutine(MoveToHome());
        }
    }
}
