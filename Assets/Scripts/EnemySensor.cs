using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySensor : MonoBehaviour {

    public List<PersonajeJugable> Targets = new();
    private PersonajeJugable ownerCharacter;

    private void Awake() {
        ownerCharacter = GetComponentInParent<PersonajeJugable>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<PersonajeJugable>(out var character)) {
            if (character == ownerCharacter) return;
            Targets.Add(character);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<PersonajeJugable>(out var character)) {
            if (character == ownerCharacter) return;
            Targets.Remove(character);
        }
    }
    public bool HasTargets() {
        return Targets.Count > 0;
    }
}
