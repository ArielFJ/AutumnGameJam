using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCup : MonoBehaviour
{
    public GameObject mLiquid;
    public GameObject mLiquidMesh;

    public float mSloshSpeed = 60;
    public float mRotateSpeed = 1;

    public int Difference = 25;

    public float fillAmount;
    public float maxFillAmount;

    private void Update()
    {
        //motion
        //Slosh();

        //rotation
        //mLiquidMesh.transform.Rotate(Vector3.up * mRotateSpeed * Time.deltaTime, Space.Self);

        //scale and translate with fill
        if(fillAmount > 0)
        {
            float fillPercent = fillAmount / maxFillAmount;
            mLiquid.SetActive(true);
            mLiquidMesh.transform.localScale = new Vector3(Mathf.Lerp(0.07f, 0.09f, fillPercent), 0, Mathf.Lerp(0.07f, 0.09f, fillPercent));
            mLiquidMesh.transform.localPosition = new Vector3(0, Mathf.Lerp(-0.075f, 0.145f, fillPercent), 0);
        }
        else
        {
            mLiquid.SetActive(false);
        }
    }

    void Slosh()
    {
        //inverse cup rotation
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);

        //rotate to
        Vector3 finalRotation = Quaternion.RotateTowards(mLiquid.transform.localRotation, inverseRotation, mSloshSpeed * Time.deltaTime).eulerAngles;

        //clamp
        finalRotation.x = ClampRotationValue(finalRotation.x,Difference);
        finalRotation.z = ClampRotationValue(finalRotation.z, Difference);

        mLiquid.transform.eulerAngles = finalRotation;
    }

    private float ClampRotationValue(float value,float difference)
    {
        float returnValue = 0f;

        if(value > 180)
        {
            //clamp
            returnValue = Mathf.Clamp(value, 360 - difference, 360);
        }
        else
        {
            //clamp
            returnValue = Mathf.Clamp(value, 0, difference);
        }

        return returnValue;
    }
}
