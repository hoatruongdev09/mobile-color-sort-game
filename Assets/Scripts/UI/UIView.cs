using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityEngine.UI {
    [RequireComponent (typeof (CanvasGroup))]
    public class UIView : MonoBehaviour {
        public OnViewInteract onViewShowed { get; set; }
        public OnViewInteract onViewHided { get; set; }
        public CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup> (); } }
        public delegate void OnViewInteract ();

        public virtual void Show (System.Action showCallback = null) {
            transform.SetAsLastSibling ();
            gameObject.SetActive (true);
            CanvasGroup.interactable = true;
            AnimateShow ().setOnComplete (() => { onViewShowed?.Invoke (); showCallback?.Invoke (); });
        }
        public virtual void Hide (System.Action hideCallback = null) {
            CanvasGroup.interactable = false;
            AnimateHide ().setOnComplete (() => {
                onViewHided?.Invoke ();
                gameObject.SetActive (false);
                hideCallback?.Invoke ();
            });
        }
        protected virtual LTDescr AnimateShow () {
            return CanvasGroup.LeanAlpha (1, .3f);
        }
        protected virtual LTDescr AnimateHide () {
            return CanvasGroup.LeanAlpha (0, .3f);
        }
    }
}