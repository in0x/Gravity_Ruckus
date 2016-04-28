using UnityEngine;
using System.Collections;
using JetBrains.Annotations;


public interface IGravityObserver
    {
        //GravityHandler GravityHandlerRef { get; set; }
        void GravitySwitch(Vector3 gravity);
    }
