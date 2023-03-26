using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomImg : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image img;
    [SerializeField] Sprite[] sprites;
    bool fade;

    private void Start()
    {
        fade = true;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float timer = 0f;
        while (timer <= 2f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            //true = fadein false = fadeout
            canvasGroup.alpha = fade ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
            if(timer >= 2f)
            {
                if (fade)
                {
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    int n = Random.Range(0, sprites.Count());
                    img.sprite = sprites[n];
                }
                fade = !fade;
                timer = 0f;
            }
        }
    }
}
