using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Health))]
public class AntAI : MonoBehaviour
{
    public delegate void FOnDeadSignature();
    public event FOnDeadSignature OnDead;

    private string paramAnim_MoveSpeed = "mAntMovementSpeed";
    private SplineAnimate mSplineAnimator;
    private Animator mAnimator;

    [SerializeField] private float mAntMoveSpeed = 10f;
    [SerializeField] private BarComponent mBarComponent;
    [SerializeField] private SplineContainer mSpline;
    
    private void Awake()
    {
        GetComponent<Health>().OnHealthUpdate += OnHealthUpdate;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            StartCoroutine(Travelling());
        }
    }
    public void OnSpawn(SplineContainer spline)
    {
        mAnimator = GetComponent<Animator>();
        if(mAnimator != null)
        {
            mAnimator.SetFloat(paramAnim_MoveSpeed, mAntMoveSpeed/10);
        }
        if (spline != null)
        {
            mSpline = spline;
            mSplineAnimator = GetComponent<SplineAnimate>();
            mSplineAnimator.Container = mSpline;
            mSplineAnimator.Loop = SplineAnimate.LoopMode.Once;
            mSplineAnimator.Duration = mAntMoveSpeed;
            mSplineAnimator.Play();
        }
    }
    private IEnumerator Travelling()
    {
        yield return new WaitForSeconds(1f);
        OnDead?.Invoke();
        OnDead = null;

        Destroy(this.gameObject);
    }

    private void OnHealthUpdate(int value)
    {  
        mBarComponent.UpdateBar(value);
        if (value <= 0)
        {
            OnDead?.Invoke();
            OnDead = null;
        }
    }
}