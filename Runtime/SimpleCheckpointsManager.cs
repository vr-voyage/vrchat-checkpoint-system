
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SimpleCheckpointsManager : UdonSharpBehaviour
{
    public Transform spawnPoint;

    public Material materialOff;
    public Material materialOn;

    public AudioSource source;
    public AudioClip activatedClip;
    
    public ConnectToBase connectToBase;

    SimpleCheckpoint[] checkpoints = new SimpleCheckpoint[16];

    int nCheckpoints;

    public void RegisterCheckpoint(SimpleCheckpoint checkpoint)
    {
        if (checkpoints == null) return;
        if (checkpoint == null) return;
        if (nCheckpoints == checkpoints.Length) return;

        int checkpointID = nCheckpoints;
        checkpoints[checkpointID] = checkpoint;
        nCheckpoints++;
    }

    void PlaySound(AudioClip clip)
    {
        if (((source != null) & (clip != null)) && (!source.isPlaying))
        {
            source.clip = clip;
            source.Play();
        }
    }

    public void SetCurrent(SimpleCheckpoint checkpoint)
    {
        for (int checkpointIndex = 0; checkpointIndex < nCheckpoints; checkpointIndex++)
        {
            var storedCheckpoint = checkpoints[checkpointIndex];
            storedCheckpoint.SetMaterial(materialOff);
            storedCheckpoint.isCurrent = false;
        }
        var checkpointTransform = checkpoint.transform;
        spawnPoint.SetPositionAndRotation(
            checkpointTransform.position,
            checkpointTransform.rotation);
        checkpoint.SetMaterial(materialOn);
        checkpoint.isCurrent = true;
        PlaySound(activatedClip);
        connectToBase.Activate(checkpoint.baseTeleportLocation);
    }
}
