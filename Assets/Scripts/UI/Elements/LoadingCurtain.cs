using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Elements
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private float _curtainAlphaFade = 0.03f;
        [SerializeField] private float _timeToFade = 0.03f;
        
        [FormerlySerializedAs("Curtain")] 
        [SerializeField] private CanvasGroup _curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _curtainAlphaFade;
                yield return new WaitForSeconds(_timeToFade);
            }

            gameObject.SetActive(false);
        }
    }
}