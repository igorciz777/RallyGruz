using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Input
{


    [StructLayout(LayoutKind.Explicit, Size = 4)]
    struct ThrustmasterT3PAInputReport : IInputStateTypeInfo
    {
        public FourCC format => new FourCC('H', 'I', 'D');
        [FieldOffset(0)] public byte reportId;

        [InputControl(name = "Throttle",format = "BIT" ,layout = "Axis", sizeInBits=8, bit = 0)]
        [FieldOffset(8)] public byte throttle;
        [InputControl(name = "Brake",format = "BIT" ,layout = "Axis", sizeInBits=8, bit = 0)]
        [FieldOffset(10)] public byte brake;
        [InputControl(name = "Clutch",format = "BIT" ,layout = "Axis", sizeInBits=8, bit = 0)]
        [FieldOffset(12)] public byte clutch;

    }

    [InputControlLayout(stateType = typeof(ThrustmasterT3PAInputReport))]
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    class ThrustmasterT3PA : Joystick
    {

        static ThrustmasterT3PA() 
        {
            InputSystem.RegisterLayout<ThrustmasterT3PA>(
                matches: new InputDeviceMatcher()
                    .WithInterface("HID")
                    .WithCapability("vendorId", 0x44F) //vendor ID here, converted to hex
                    .WithCapability("productId", 0xB678)); //product ID here, converted to hex
        }

        [RuntimeInitializeOnLoadMethod]
        static void Init() { }

    }
}