using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RectTransform bar;
    [SerializeField] private float lerpSpeed = 3f;

    private void Update()
    {
        float fillAmount = (int)player.Health / player.MaxHealth;
        bar.localScale = new Vector3(Mathf.Lerp(bar.localScale.x, fillAmount, Time.deltaTime * lerpSpeed), 1, 1);
    }
    
}