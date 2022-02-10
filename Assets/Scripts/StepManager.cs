using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepManager : MonoBehaviour
{
    private string[] steps;
    public string[] animation_strings;
    private int currStep = 0;
    public TutorialText text;
    public TMP_Text tutorialText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //initialize the steps array with size of text.sentences
        steps = new string[text.sentences.Length];

        //load the steps array with content of text.sentences
        for (int i = 0; i < text.sentences.Length; i++)
        {
            steps[i] = text.sentences[i];
        }

        DiplayFirstText();
    }

    private void DiplayFirstText()
    {
        tutorialText.text = steps[currStep];
        PlayAnimation(currStep);
    }

    private void DisplayNextText()
    {
        //increment only if the step is not at last step (index [size - 1])
        if (currStep != steps.Length -1)
        {
            currStep++;
        }
        tutorialText.text = steps[currStep];
        PlayAnimation(currStep);
    }

    private void DisplayPreviousText()
    {
        //decrement only if the step is not at first step (index 0)
        if (currStep != 0)
        {
            currStep--;
        }
        tutorialText.text = steps[currStep];
        PlayAnimation(currStep);
    }

    void PlayAnimation(int step)
    {
        anim.Play(animation_strings[step]);
    }

    public void bindModel(GameObject placedObject)
    {
        anim = placedObject.GetComponent<Animator>();
        PlayAnimation(currStep);
    }
    
}
