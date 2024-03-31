using System;
using System.Collections.Generic;
using UnityEngine;

namespace _SDK.UI.Utils
{
    public abstract class ToggleMultipleButton<T>: MonoBehaviour where T: Enum
    {
        [SerializeField] protected List<GameObject> stateViews;
        [SerializeField] protected T currentState;

        private void OnEnable()
        {
            OnSetup();
        }

        protected abstract void OnSetup();

        public void SetState(T state)
        {
            currentState = state;
            SetUIState(state);
        }
        
        private void SetUIState(T state)
        {
            for (int i = 0; i < stateViews.Count; i++)
            {
                stateViews[i].SetActive(false);
            }
            
            stateViews[(int) (object) state].SetActive(true);
        }

        public abstract void OnClick();
    }
}