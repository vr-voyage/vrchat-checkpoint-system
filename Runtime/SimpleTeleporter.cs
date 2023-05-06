
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SimpleTeleporter : UdonSharpBehaviour
{
    public Transform destination;
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        player.TeleportTo(destination.position, destination.rotation);
    }
}
