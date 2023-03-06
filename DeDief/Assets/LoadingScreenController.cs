using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoadingScreenController : UIController
{
    ProgressBar progressBar;
    bool animateBar = false;
    
    protected override void UISetup()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        progressBar = root.Q<ProgressBar>("ProgressBar");
        animateBar = true;
    }

    void Update()
    {
        if (animateBar)
        {
            progressBar.value = progressBar.value + Time.deltaTime;
            if (progressBar.value >= 100)
            {
                progressBar.value = 0;
            }
        }
    }
}

