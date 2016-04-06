using UnityEngine;

/*\ 
|*| This should be attached to any parts of a model
|*| that have a RigidBody and should deal damage to the
|*| player when hit. Hitchecks such as RayCasts and Sphere-
|*| intersections performed by weapons should check for this component.
\*/

// Individual DamageReciever should inherit from this class and override ApplyDamage to correctly modify the value
// i.e. HeadDamageReciever returns 2x value

public class DamageReciever
{
    // This may not be needed, can use sendMessageUpwards instead
    // May however be a bad idea since it would break the convention of linking objects
    public HealthController parentHealthControl;
    
    public virtual void ApplyDamage(float value)
    {

    }
}

