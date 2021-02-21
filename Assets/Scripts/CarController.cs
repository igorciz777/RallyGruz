using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField]
    public MainControls carController;
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField] private driveType drive;
    //public float engineTorque = 600;
    public float maxAngle = 30;
    //public float steeringRatio = 1;
    public bool hasRpmLimiter, handbrake, inNeutral, inReverse, engineLerp = false, playWheelSlipEffect = false, isBraking = false;
    public float idleRPM, limitRPM;
    public float[] gearRatio;
    public int gearNum;
    public float engineRPM, KMH, brakeBias = 0.6f, brakePower = 1200f, handbrakePower = 2500f;
    [HideInInspector]public float wheelsRPM;
    [SerializeField]
    private WheelCollider WHEEL_LF, WHEEL_RF, WHEEL_LR, WHEEL_RR;
    private WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField]
    private GameObject wheelObj_LF, wheelObj_RF, wheelObj_LR, wheelObj_RR;
    private GameObject[] wheelObjects = new GameObject[4];
    [SerializeField]
    private GameObject steeringWheel;
    [SerializeField]
    private Vector3 com;
    private Vector2 axisInput;
    private float brakeInput, brakeAxis;
    [SerializeField]
    private Rigidbody carRigidbody;
    private float vertical, horizontal, totalPower, engineLerpValue, brakeForce = 0;
    public AnimationCurve enginePower;
    [SerializeField]
    private AudioSource engineSound;
    [SerializeField]
    private GameObject[] gearChangeSound;
    public Text rpmText, kmhText, gearText;
    [SerializeField]
    private GameObject rpmNeedle, kmhNeedle;
    public float rpmNeedleValue = 31.5f, kmhNeedleValue = 1.1f, throttleResponseValue = 200;
    // Start is called before the first frame update
    void Start()
    {
        setObjects();
        carRigidbody.centerOfMass = com;
        engineSound.Play();

    }
    // Update is called once per frame
    void Update()
    {
        wheelTransformUpdate();
        wheelSlipEffect();
        calculateEnginePower();
        steeringWheelMatch();
        engineSoundPitch();
        setBrakes();
    }
    void FixedUpdate()
    {
        axisUpdate();
        setUIText();
        animateDash();
    }
    void Awake()
    {
        carController = new MainControls();
        
        carController.Car.ThrottleAxis.performed += ctx => axisInput.y = ctx.ReadValue<float>();
        carController.Car.BrakeAxis.performed += ctx => brakeInput = ctx.ReadValue<float>();
        carController.Car.SteeringAxis.performed += ctx => axisInput.x = ctx.ReadValue<float>();
        carController.Car.GearShift.performed += ctx => changeGears(ctx);
        carController.Car.Handbrake.performed += ctx => handbrake = true;
        carController.Car.Handbrake.canceled += ctx => handbrake = false;

        rpmText = GameObject.Find("Canvas/RPM").GetComponent<Text>();
        kmhText = GameObject.Find("Canvas/KMH").GetComponent<Text>();
        gearText = GameObject.Find("Canvas/Gear").GetComponent<Text>();

    }
    private void OnEnable() {
        carController.Enable();
    }
    private void OnDisable() {
        carController.Disable();
    }
    private void setUIText()
    {
        rpmText.text = "RPM:" + System.Math.Round(engineRPM, 0);
        kmhText.text = "KMH:" + System.Math.Round(KMH, 1);
        switch (gearNum)
        {
            case 0:
                gearText.text = "Gear:R";
                break;
            case 1:
                gearText.text = "Gear:N";
                break;
            default:
                gearText.text = "Gear:" + (gearNum - 1);
                break;
        }
    }
    private void animateDash()
    {
        rpmNeedle.transform.localEulerAngles = new Vector3(0, engineRPM / rpmNeedleValue, 0);
        kmhNeedle.transform.localEulerAngles = new Vector3(0, KMH * kmhNeedleValue, 0);
    }
    private void engineSoundPitch()
    {
        engineSound.pitch = engineRPM / 3000;
    }
    private void axisUpdate()
    {
        vertical = axisInput.y;
        brakeAxis = brakeInput;
        ///smooth steering solution:
        ///https://forum.unity.com/threads/axis-gravity-smoothing.943131/
        ///
        if(axisInput.x == 0){
            horizontal = Mathf.MoveTowards(horizontal, 0f, Time.deltaTime * 2f);
        }else{
            horizontal = Mathf.MoveTowards(horizontal, axisInput.x, Time.deltaTime * 2f);
        }
        
        horizontal = Mathf.Clamp(horizontal, -1, 1);
    }
    private void steeringWheelMatch()
    {
        steeringWheel.transform.localEulerAngles = Vector3.back * Mathf.Clamp((horizontal * 450), -450, 450);
    }
    private void wheelPower()
    {
        float angle = maxAngle * horizontal;
        if (inNeutral)
        {
            foreach (WheelCollider wheel in wheelColliders)
            {
                wheel.motorTorque = 0;
            }
        }
        if (!inNeutral)
        {
            if (drive == driveType.allWheelDrive)
            {
                foreach (WheelCollider wheel in wheelColliders)
                {
                    wheel.motorTorque = inReverse ? -totalPower / 4 : totalPower / 4;
                }
            }
            else if (drive == driveType.rearWheelDrive)
            {
                wheelColliders[2].motorTorque = inReverse ? -totalPower / 2 : totalPower / 2;
                wheelColliders[3].motorTorque = inReverse ? -totalPower / 2 : totalPower / 2;
            }
            else
            {
                wheelColliders[0].motorTorque = inReverse ? -totalPower / 2 : totalPower / 2;
                wheelColliders[1].motorTorque = inReverse ? -totalPower / 2 : totalPower / 2;
            }
        }
        else
        {
            totalPower = 0;
        }
        wheelColliders[0].steerAngle = angle;
        wheelColliders[1].steerAngle = angle;
        wheelColliders[0].brakeTorque = brakeForce * brakeBias;
        wheelColliders[1].brakeTorque = brakeForce * brakeBias;
        wheelColliders[2].brakeTorque = brakeForce - brakeForce * brakeBias;
        wheelColliders[3].brakeTorque = brakeForce - brakeForce * brakeBias;
        if (handbrake)
        {
            wheelColliders[2].brakeTorque = handbrakePower;
            wheelColliders[3].brakeTorque = handbrakePower;
        }
        KMH = carRigidbody.velocity.magnitude * 3.6f;
    }
    private void wheelTransformUpdate()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelObjects[i].transform.position = wheelPosition;
            wheelObjects[i].transform.rotation = wheelRotation;
        }
    }
    private void setObjects()
    {
        wheelColliders[0] = WHEEL_LF;
        wheelColliders[1] = WHEEL_RF;
        wheelColliders[2] = WHEEL_LR;
        wheelColliders[3] = WHEEL_RR;
        wheelObjects[0] = wheelObj_LF;
        wheelObjects[1] = wheelObj_RF;
        wheelObjects[2] = wheelObj_LR;
        wheelObjects[3] = wheelObj_RR;
    }
    private void changeGears(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0 && gearNum != gearRatio.Length - 1)
        {
            gearNum++;
        }
        else if (ctx.ReadValue<float>() < 0 && gearNum > 0)
        {
            gearNum--;
        }
        else
        {
            return;
        }
        GameObject gearSound = Instantiate(gearChangeSound[Random.Range(0, gearChangeSound.Length)]);
        Destroy(gearSound, 1f);
    }
    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheelColliders[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
    }
    private void calculateEnginePower()
    {
        inNeutral = gearNum == 1 ? true : false;
        inReverse = gearNum == 0 ? true : false;
        lerpEngine();
        wheelRPM();
        if (vertical != 0)
        {
            carRigidbody.drag = 0.005f;
        }
        if (vertical == 0)
        {
            carRigidbody.drag = 0.1f;
        }
        totalPower = 3.6f * enginePower.Evaluate(engineRPM) * (vertical);



        float velocity = 0.0f;
        if (engineRPM >= limitRPM && hasRpmLimiter)
        {
            setEngineLerp(limitRPM - 800);
        }
        if (!engineLerp)
            engineRPM = Mathf.SmoothDamp(engineRPM, idleRPM + (Mathf.Abs(inNeutral ? vertical * throttleResponseValue : wheelsRPM) * 3.6f * (gearRatio[gearNum])), ref velocity, .05f);

        if (engineRPM >= limitRPM + 1000) engineRPM = limitRPM + 1000;
        wheelPower();
    }
    private void setEngineLerp(float num)
    {
        engineLerp = true;
        engineLerpValue = num;
    }
    private void lerpEngine()
    {
        if (engineLerp)
        {
            engineRPM = Mathf.Lerp(engineRPM, engineLerpValue, 8 * Time.deltaTime);
            engineLerp = engineRPM <= engineLerpValue + 100 ? false : true;
        }
    }
    private void setBrakes()
    {
        if (brakeAxis > 0)
        {
            brakeForce = brakePower * brakeAxis;
            isBraking = true;
        }
        else
        {
            brakeForce = 0;
            isBraking = false;
        }
    }
    private void wheelSlipEffect(){
        WheelHit wheelHit;

        foreach(WheelCollider wheel in wheelColliders){
            wheel.GetGroundHit(out wheelHit);

            if(wheelHit.sidewaysSlip >= 0.2f || wheelHit.sidewaysSlip <= -0.2f || wheelHit.forwardSlip >= 0.2f || wheelHit.forwardSlip <= -0.2f){
                playWheelSlipEffect = true;
            }
            else{
                playWheelSlipEffect = false;
            }
        }
    }
}
