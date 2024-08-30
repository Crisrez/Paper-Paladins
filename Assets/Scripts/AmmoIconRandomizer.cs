using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AmmoIconRandomizer : MonoBehaviour {

    [SerializeField]
    private Sprite[] iconSprites;

    private Image image;
    private void Awake() {
        image = GetComponent<Image>();
    }
    
    private void OnEnable() {
        image.sprite = iconSprites[Random.Range(0, iconSprites.Length)];
    }
}
