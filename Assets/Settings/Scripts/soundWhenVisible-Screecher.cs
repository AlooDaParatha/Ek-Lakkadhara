using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundUntilVisible : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The object the camera needs to look at.")]
    public Transform targetObject;
    
    [Tooltip("The camera checking for visibility. Leaves blank to use Main Camera.")]
    public Camera targetCamera;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private Renderer targetRenderer;
    int flag = -1;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource1 = GetComponentInChildren<AudioSource>();
        audioSource2 = GetComponentInChildren<AudioSource>();
        
        // Ensure the audio loops smoothly
        audioSource2.loop = true;
        
        // Cache the renderer of the target object to check boundaries
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
        }
    }

    void Update()
    {
        if (targetObject == null || targetCamera == null || targetRenderer == null) return;

        // Check if the target is within the camera's view frustum
        bool isVisible = IsTargetVisible();

        if (isVisible)
        {
            // Resume playing audio if the object is being seen
            if (!audioSource1.isPlaying)
            {
                if (flag==0)
                {   audioSource1.Play();
                    flag=1;
                }
                else
                    audioSource2.UnPause(); // Use audioSource.Play(); if you used Stop() above
            }
        }
        else
        {
            // Pause or stop the audio if the camera does not see the object
            if (audioSource1.isPlaying)
            {
                audioSource1.Stop(); flag=0; // Use audioSource.Stop(); if you want it to restart from the beginning later
            }
            if (audioSource2.isPlaying)
            {
                audioSource2.Pause(); flag=0; // Use audioSource.Stop(); if you want it to restart from the beginning later
            }
        }
    }

    private bool IsTargetVisible()
    {
        // 1. Quick Frustum Check: Is the object's bounding box inside the camera's view?
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        if (!GeometryUtility.TestPlanesAABB(planes, targetRenderer.bounds))
        {
            return false;
        }

        // 2. Line of Sight Check: Is the object blocked by walls/obstacles?
        Vector3 directionToTarget = targetObject.position - targetCamera.transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        // Cast a ray from the camera to the object
        if (Physics.Raycast(targetCamera.transform.position, directionToTarget.normalized, out RaycastHit hit, distanceToTarget))
        {
            // If the ray hits something else first, the object is blocked/hidden
            if (hit.transform != targetObject)
            {
                return false;
            }
        }

        return true;
    }
}
