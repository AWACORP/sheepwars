using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class FlamingSlime : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }
    public float explosionRadius = 5.0f;
    public float explosionForce = 1.0f;

    public float characterControllerMinMultiplier = 10.0f; // Minimum force multiplier
    public float characterControllerMaxMultiplier = 100.0f; // Maximum force multiplier


    public void Init(Vector3 forward)
    {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        GetComponent<Rigidbody>().velocity = forward;
    }

    public FlamingSlime()
    {

    }

    /* private void OnTriggerEnter(Collider other)
     {
         Debug.Log(other.name);
     }
 */
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Explode();
            Runner.Despawn(Object);
        }
    }

    private void Explode()
    {
        Vector3 explosionPosition = transform.position;

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.name);

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            CharacterController cc = hit.GetComponent<CharacterController>();
            // Check if the object has a Rigidbody to apply force
            if (rb != null)
            {
                //rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }

            else if (cc != null)
            {
                Vector3 explosionToCharacter = hit.transform.position - explosionPosition;
                explosionToCharacter.y = 0; // Assuming you want to remove vertical movement, but you can adjust if needed
                explosionToCharacter.Normalize();

                // Calculate the distance-based multiplier
                float distance = Vector3.Distance(hit.transform.position, explosionPosition);
                float normalizedDistance = distance / explosionRadius;
                float forceMultiplier = Mathf.Lerp(characterControllerMaxMultiplier, characterControllerMinMultiplier, normalizedDistance);

                // Calculate the "force" to apply
                float force = explosionForce * forceMultiplier;

                // Apply the "force" to the character directly
                cc.Move(explosionToCharacter * force * Time.deltaTime);
            }

            // Here, you can add code to deal damage to objects if needed
        }
    }
}
