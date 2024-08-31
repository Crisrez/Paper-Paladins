using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class CharacterSprite : MonoBehaviour {
    [System.Serializable]
    public class CharacterColorScheme {
        public Color primary;
        public Color secondary;
        public Color line;
    }
    private Animator animator;
    private Transform camera;
    private Transform parent;
    private Rigidbody parentRigidbody;
    private PersonajeJugable character;
    private NavMeshAgent agent;
    private SpriteRenderer renderer;
    private Material rendererMaterial;
    [SerializeField] private CharacterColorScheme[] colorSchemes;
    
    private static readonly int ViewAngle = Animator.StringToHash("ViewAngle");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private MaterialPropertyBlock propertyBlock;
    private static readonly int ColorA = Shader.PropertyToID("_ColorA");
    private static readonly int ColorB = Shader.PropertyToID("_ColorB");
    private static readonly int LineColor = Shader.PropertyToID("_LineColor");
    private void Awake() {
        parent = transform.parent;
        renderer = GetComponent<SpriteRenderer>();
        
        character = GetComponentInParent<PersonajeJugable>();
        parentRigidbody = parent.GetComponent<Rigidbody>();
        agent = parent.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void RandomizeColorScheme() {
        var scheme = colorSchemes[Random.Range(0, colorSchemes.Length)];
        propertyBlock ??= new();
        propertyBlock.SetColor(ColorA,scheme.primary);
        propertyBlock.SetColor(ColorB,scheme.secondary);
        propertyBlock.SetColor(LineColor,scheme.line);
        
        renderer.SetPropertyBlock(propertyBlock);
    }
    
    private void Start() {
        if(Application.isPlaying) RandomizeColorScheme();
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
