
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SimpleCheckpoint : UdonSharpBehaviour
{

    public SimpleCheckpointsManager manager;

    public Transform baseTeleportLocation;

    public bool isCurrent = false;

    MeshRenderer checkpointRenderer;

    public void LogError(string message)
    {
        Debug.LogError($"[{name}] [SimpleCheckPoint] {message}");
    }

    void OnEnable()
    {
        checkpointRenderer = GetComponent<MeshRenderer>();
        if (checkpointRenderer == null)
        {
            LogError($"renderer is not set (null). Disabling.");
            gameObject.SetActive(false);
            return;
        }
        manager.RegisterCheckpoint(this);
    }

    public void SetMaterial(Material newMaterial)
    {
        Debug.Log($"<color=green>Setting material {newMaterial.name} on {name}</color>");
        checkpointRenderer.sharedMaterial = newMaterial;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if ((player.isLocal) & (!isCurrent))
        {
            manager.SetCurrent(this);
        }
    }

}
