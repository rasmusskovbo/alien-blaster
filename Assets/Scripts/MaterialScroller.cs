using UnityEngine;

public class MaterialScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;

    private Vector2 offset;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset = scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset += offset;
    }

}