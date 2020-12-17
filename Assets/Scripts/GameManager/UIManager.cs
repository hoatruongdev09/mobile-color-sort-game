using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public UIView CurrentView { get; private set; }
    public PanelGame PanelGame => panelGame;
    [SerializeField] private PanelGame panelGame;

    public void OpenView (UIView view, Action showCallback = null) {
        view.Show (showCallback);
    }
    public void CloseView (UIView view, Action hideCallback = null) {
        view.Hide (hideCallback);
    }
    public void ChangeView (UIView nextView, Action showCallback = null, Action hideCallback = null) {
        if (CurrentView == nextView) { return; }
        if (CurrentView is null) {
            CurrentView = nextView;
            CurrentView.Show (showCallback);
            return;
        }
        CurrentView.Hide (() => {
            CurrentView = nextView;
            hideCallback?.Invoke ();
            CurrentView.Show (showCallback);
        });
    }

}