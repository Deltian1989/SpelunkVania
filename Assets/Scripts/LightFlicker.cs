using UnityEngine;

namespace SpelunkVania
{
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField]
        private float initialAlphaValue;

        [SerializeField]
        private float endAlphaValue;

        [SerializeField]
        private float lerpTime;

        private int initialAlphaValueInt;

        private int endAlphaValueInt;

        private float destinationAlphaValue;

        private float startAlphaValue;

        private float currentAlphaValue;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            initialAlphaValueInt = (int)(initialAlphaValue * 100);

            endAlphaValueInt = (int)(endAlphaValue * 100);

            currentAlphaValue = spriteRenderer.color.a;
            destinationAlphaValue = spriteRenderer.color.a;
        }

        void LateUpdate()
        {
            //if (destinationAlphaValue == currentAlphaValue)
            //{
            //    destinationAlphaValue = Random.Range(initialAlphaValueInt, endAlphaValueInt) * 0.01f;

            //    startAlphaValue = currentAlphaValue;

            //    Debug.Log(destinationAlphaValue);
            //}

            //currentAlphaValue = Mathf.Lerp(startAlphaValue, destinationAlphaValue, lerpTime * Time.deltaTime);

            //Debug.Log(System.Math.Round(currentAlphaValue, 2));

            //var color = spriteRenderer.color;

            //color.a = (float)System.Math.Round(currentAlphaValue, 2);

            //spriteRenderer.color= color;
        }
    }
}
