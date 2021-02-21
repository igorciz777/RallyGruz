using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    [SerializeField]
    public MainControls controls;
    private CarController carController;
    [SerializeField]
    private ParticleSystem[] wheelEffects;
    [SerializeField]
    private AudioSource gravelSound;
    public int lightMode = 0;
    [SerializeField]
    private Light[] headLights, brakeLights, rearLights, reverseLights;
    [SerializeField]
    private Material insideLampMat, outsideLampMat;
    // Start is called before the first frame update
    void Start()
    {
        carController = this.transform.GetComponent<CarController>();
    }
    private void Awake()
    {
        controls = new MainControls();
        
        controls.Car.Lights.performed += ctx => setLights();
    }
    private void OnEnable() {
        controls.Enable();
    }
    private void OnDisable() {
        controls.Disable();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        setGroundAudioPitch();
        updateLights();
        foreach (ParticleSystem effect in wheelEffects)
        {
            if (carController.playWheelSlipEffect)
            {
                if (!effect.isPlaying) effect.Play(false);
                if (!gravelSound.isPlaying) gravelSound.Play();
            }
            else
            {
                if (effect.isPlaying) effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                if (gravelSound.isPlaying) gravelSound.Stop();
            }
        }
    }
    private void setGroundAudioPitch()
    {
        gravelSound.pitch = carController.wheelsRPM / 520;
        if (gravelSound.pitch > 1.5f)
        {
            gravelSound.pitch = 1.5f;
        }
    }
    private void updateLights()
    {

        foreach (Light brakeLight in brakeLights)
        {
            if (carController.isBraking)
            {
                brakeLight.enabled = true;
            }else{
                brakeLight.enabled = false;
            }

        }
        foreach (Light reverseLight in reverseLights){
            if(carController.inReverse){
                reverseLight.enabled = true;
            }else{
                reverseLight.enabled = false;
            }
        }
    }
    private void setLights()
    {
        lightMode++;
        if (lightMode > 2)
        {
            lightMode = 0;
        }
        foreach (Light headLight in headLights)
        {
            switch (lightMode)
            {
                case 0:
                    headLight.enabled = false;
                    insideLampMat.SetColor("_EmissionColor", Color.black * 0f);
                    outsideLampMat.SetColor("_EmissionColor", Color.black * 0f);
                    break;
                case 1:
                    headLight.enabled = true;
                    headLight.spotAngle = 100;
                    headLight.intensity = 2;
                    headLight.range = 40;
                    insideLampMat.SetColor("_EmissionColor", Color.white * 1f);
                    outsideLampMat.SetColor("_EmissionColor", Color.white * 0.6f);
                    break;
                case 2:
                    headLight.enabled = true;
                    headLight.spotAngle = 70;
                    headLight.intensity = 2f;
                    headLight.range = 100;
                    insideLampMat.SetColor("_EmissionColor", Color.white * 2f);
                    outsideLampMat.SetColor("_EmissionColor", Color.white * 0.8f);
                    break;
            }
        }
        foreach (Light rearLight in rearLights)
        {
            switch (lightMode)
            {
                case 0:
                    rearLight.enabled = false;
                    break;
                case 1:
                    rearLight.enabled = true;
                    break;
                case 2:
                    rearLight.enabled = true;
                    break;
            }
        }
    }
}
