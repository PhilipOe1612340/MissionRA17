using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class CapDoorControl : MonoBehaviour
{
    public Transform outerHandle;
    public Transform innerHandle;
    public GameObject Handle;
    [SerializeField] private int snapRotationAmount;
    [SerializeField] private float angleTolerance;

    private XRBaseInteractor interactor;
    private float startAngle;
    private float totalAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;


    private HingeJoint doorHinge;
    private JointLimits limits;
    bool isLocked;
    

    public AudioClip unlockDoor;
    public AudioClip lockDoor;
    private AudioSource audioData;

    public UnityEvent OnGameComplete = new UnityEvent();



    // Start is called before the first frame update
    void Start()
    {

        doorHinge = GetComponent<HingeJoint>();
        limits = doorHinge.limits;
        ToggleHinge(0);
        isLocked = true;
        
        totalAngle = 0;
        audioData = GetComponent<AudioSource>();


    }

    #region Handlegrab
    public void GrabEnd(SelectExitEventArgs arg0)
    {
        shouldGetHandRotation = false;
        requiresStartAngle = true;
    }

    public void GrabbedBy(SelectEnterEventArgs arg0)
    {
        //Debug.Log("we go here2");
        //interactor = Handle.GetComponent<XRSimpleInteractable>().selectingInteractor;
        interactor = Handle.GetComponent<XRGrabInteractable>().selectingInteractor;
        interactor.GetComponent<XRDirectInteractor>().hideControllerOnSelect = true;
        
        shouldGetHandRotation = true;        
        startAngle = 0f; 

    }
    #endregion

    void Update()
    {
        if (shouldGetHandRotation && doorHinge.angle < 5f)
        {
            //if (doorHinge.limits.max > 0)
            //{
                //ToggleHinge(0);
            //}
            //Debug.Log("TOTAL" + totalAngle);
            //Debug.Log("fun");
            //move Handle
            var rotationAngle = GetInteractorRotation(); //current controller angle
            GetRotationDistance(rotationAngle);
            if (isLocked && totalAngle >= 360)
            {
                UnlockDoor();
            }
            if (!isLocked && totalAngle == 0) 
            {
                LockDoor();
            }
        }
    }

    private void ToggleHinge(float angle)
    {
        //Debug.Log("toggle hinge");
        limits.max = angle;
        doorHinge.limits = limits;
    }

    private void LockDoor()
    {
        isLocked = true;
        ToggleHinge(0);
        //Debug.Log("door locked");
        audioData.PlayOneShot(lockDoor);
        OnGameComplete.Invoke();
    }

    private void UnlockDoor()
    {
        ToggleHinge(100);
        isLocked = false;
        //Debug.Log("door unlocked");
        audioData.PlayOneShot(unlockDoor);
    }

    #region Handlemovement
    public float GetInteractorRotation() => interactor.GetComponent<Transform>().eulerAngles.z;


    private void GetRotationDistance(float currentAngle)
    {

        if (!requiresStartAngle)
        {
            var angleDifference = Mathf.Abs(startAngle - currentAngle);

            //Debug.Log("3we go here" + (startAngle - currentAngle));
            //Debug.Log("start" + startAngle);
            //Debug.Log("curr" + currentAngle);

            if (angleDifference > angleTolerance)
            {

                if (angleDifference > 270f) //checking to see if the user has gone from 0-360 - a very tiny movement but will trigger the angletolerance
                {
                    float angleCheck = CheckAngle(currentAngle, startAngle);
                    if (angleCheck < angleTolerance)
                        return;
                    else
                    {
                        if (startAngle < currentAngle)
                        {
                            if (totalAngle + snapRotationAmount >=0 )
                            {
                                RotateOutAntiClockwise();
                                startAngle = currentAngle;
                            }
                            else { return; }
                        }
                        else if (startAngle > currentAngle)
                        {
                            if (totalAngle - snapRotationAmount <= 360) 
                            {
                                RotateOutClockwise();
                                startAngle = currentAngle;
                            }
                            else { return; }
                        }
                    }
                }
                else
                {
                    if (startAngle < currentAngle)
                    {
                        if (totalAngle + snapRotationAmount <=360)
                        {
                            RotateOutClockwise();
                            startAngle = currentAngle;
                        }
                        else { return; }
                    }
                    else if (startAngle > currentAngle)
                    {
                        if (totalAngle - snapRotationAmount >=0)
                        {
                            RotateOutAntiClockwise();
                            startAngle = currentAngle;
                        }
                        else { return; }
                    }
                }
            }
            else { return; }
            
        }
        else
        {
            requiresStartAngle = false;
            startAngle = currentAngle;
        }

    }


    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;



    private void RotateOutClockwise()
    {
        //Debug.Log("1yeah" + totalAngle);
        outerHandle.localEulerAngles = new Vector3(outerHandle.localEulerAngles.x,
                                                      outerHandle.localEulerAngles.y,
                                                      outerHandle.localEulerAngles.z + snapRotationAmount);

        innerHandle.localEulerAngles = new Vector3(innerHandle.localEulerAngles.x,
                                                      innerHandle.localEulerAngles.y,
                                                      innerHandle.localEulerAngles.z + snapRotationAmount);
        totalAngle += snapRotationAmount;
    }

    private void RotateOutAntiClockwise()
    {
        //Debug.Log("2yeah" + totalAngle);
        outerHandle.localEulerAngles = new Vector3(outerHandle.localEulerAngles.x,
                                                      outerHandle.localEulerAngles.y,
                                                      outerHandle.localEulerAngles.z - snapRotationAmount);

        innerHandle.localEulerAngles = new Vector3(innerHandle.localEulerAngles.x,
                                                      innerHandle.localEulerAngles.y,
                                                      innerHandle.localEulerAngles.z - snapRotationAmount);
        totalAngle -= snapRotationAmount;

    }
#endregion

}