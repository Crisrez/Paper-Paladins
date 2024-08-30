using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {
    private static int index;
    private static readonly List<RespawnPoint> respawnPoints = new();

    private void OnEnable() {
        respawnPoints.Add(this);
    }

    private void OnDisable() {
        respawnPoints.Add(this);
    }

    public static RespawnPoint GetRespawnPoint() {
        RespawnPoint respawnPoint = respawnPoints[index];
        index = (index + 1) % respawnPoints.Count;
        return respawnPoint;
    }
}
