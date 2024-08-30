using System;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class CharacterSprite : MonoBehaviour {
    private Animator animator;
    private Transform camera;
    private Transform parent;
    private Rigidbody parentRigidbody;
    private PersonajeJugable character;
    private NavMeshAgent agent;
    private static readonly int ViewAngle = Animator.StringToHash("ViewAngle");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private void Awake() {
        parent = transform.parent;
        character = GetComponentInParent<PersonajeJugable>();
        parentRigidbody = parent.GetComponent<Rigidbody>();
        agent = parent.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if (Camera.main != null) camera = Camera.main.transform;
    }

    private void LateUpdate() {
        var cameraVector = transform.position - camera.position;
        cameraVector.y = 0;

        transform.LookAt(transform.position + cameraVector);

        float speed = 0;
        if (parentRigidbody)
            speed = Mathf.Max(parentRigidbody.velocity.magnitude, speed);
        if (agent)
            speed = Mathf.Max(agent.velocity.magnitude, speed);

        animator.SetBool(IsRunning, speed > 0.1f);
        animator.SetFloat(ViewAngle, transform.localRotation.eulerAngles.y);
        animator.SetBool(IsDead, character.GetVida() == 0);
    }
}
