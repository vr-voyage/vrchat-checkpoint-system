
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ConnectToBase : UdonSharpBehaviour
{
    public GameObject otherTeleporter;
    public Transform otherTeleporterExit;
    Transform otherTeleporterTransform;
    SphereCollider sphereCollider;
    bool activated = false;

    public void LogError(string message)
    {
        Debug.LogError($"[{name}] [ConnectToBase] {message}");
    }

    void OnEnable()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            LogError($"collider is not set (null). Disabling.");
            gameObject.SetActive(false);
            return;
        }

        if (otherTeleporter == null)
        {
            LogError($"otherTeleporter is not set (null). Disabling.");
            gameObject.SetActive(false);
            return;
        }

        if (otherTeleporterExit == null)
        {
            LogError($"otherTeleporterExit is not set (null). Disabling.");
            gameObject.SetActive(false);
            return;
        }

        otherTeleporterTransform = otherTeleporter.transform;
        sphereCollider.enabled = activated;
    }

    public void Activate(Transform at)
    {
        if (at == null) return;
        otherTeleporterTransform.SetPositionAndRotation(at.position, at.rotation);
        otherTeleporter.SetActive(true);
        sphereCollider.enabled = true;
        activated = true;
    }

    public void Desactivate()
    {
        activated = false;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        player.TeleportTo(otherTeleporterExit.position, otherTeleporterExit.rotation);
    }
}
