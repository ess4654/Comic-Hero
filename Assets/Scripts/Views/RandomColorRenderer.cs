using UnityEngine;

public class RandomColorRenderer : MonoBehaviour
{
    [SerializeField] private Color[] randomColors; 

    private void Awake()
    {
        var props = new MaterialPropertyBlock();
        var rend = GetComponent<SpriteRenderer>();
        rend.GetPropertyBlock(props);
        props.SetColor("_Color", randomColors.Length == 0 ? Color.white : randomColors.SelectRandom());
        rend.SetPropertyBlock(props);
    }
}