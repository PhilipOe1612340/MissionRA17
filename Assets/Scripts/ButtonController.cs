using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Transform lightRef;
    public GameObject sparksObj;

    public AudioClip fail;
    public AudioClip win;

    private Material lightMaterial;
    private int level = 1;
    private int inpIdx = 0;
    private int[] solution = new int[6];
    private int[] input;
    private bool finished = false;

    private IEnumerator coroutine;
    private IEnumerator resetAfter;
    private bool beeingReset = false;

    private AudioSource audioData;
    private ParticleSystem sparks;
    

    // Start is called before the first frame update
    void Start()
    {
        lightMaterial = lightRef.GetComponent<MeshRenderer>().material;
        SetColor(Color.black);
        GenerateSolution();
        StartDisplaySolutionCoroutine();
        
        audioData = GetComponent<AudioSource>();

        sparks = sparksObj.GetComponent<ParticleSystem>();
    }

    public void RedButtonPressed(){
        ButtonPressed(0);
    }
    public void BlueButtonPressed(){
        ButtonPressed(1);
    }
    public void GreenButtonPressed(){
        ButtonPressed(2);
    }
    public void YellowButtonPressed(){
        ButtonPressed(3);
    }

    private void ButtonPressed(int num){
        if(finished){
            return;
        }

        StopCoroutine(coroutine);
        if(resetAfter != null){
            StopCoroutine(resetAfter);
        }
        
        if(input == null){
            resetInput();
        }

        // save input
        input[inpIdx] = num;
        SetColor(lookUpColor(num));
        ResetAfter();

        inpIdx++;

        if(InputCorrectSoFar()){

            // level complete
            if(inpIdx == level){
                level++;
                inpIdx = 0;
                input = null;
                StartDisplaySolutionCoroutine();
            }

            // puzzle solved
            if(level == solution.Length + 1){
                finished = true;
                audioData.PlayOneShot(win);
                return;
            }

        } else {
            // reset to level 1
            level = 1;
            inpIdx = 0;
            input = null;

            // play shock sound
            audioData.PlayOneShot(fail);

            // make sparks
            if (!sparks.isPlaying)
            {
                sparksObj.SetActive(true);
                sparks.Play();
            }

            // change solution
            GenerateSolution();
            StartDisplaySolutionCoroutine();
        }
    }

    private void ResetAfter(){
        resetAfter = ResetAfterRoutine(.7f);
        StartCoroutine(resetAfter);
    }

    private void StartDisplaySolutionCoroutine(){
        coroutine = ShowSolutionRoutine(0.8f, .2f);
        StartCoroutine(coroutine);
    }

    private void SetColor(Color color){
        lightMaterial.color = color;
    }

    private Color lookUpColor(int colIdx){
        switch (colIdx)
        {
            case 0: return Color.red;
            case 1: return Color.blue;
            case 2: return Color.green;
            case 3: return Color.yellow;
        }

        return Color.black;
    }

    private void resetInput(){
        input = new int[solution.Length];
        for (int i = 0; i < input.Length; i++){
            input[i] = -1;
        }
    }

    private IEnumerator ResetAfterRoutine(float secs){
        beeingReset = true;
        yield return new WaitForSeconds(secs);
        SetColor(Color.black);
        beeingReset = false;
    }

    private IEnumerator ShowSolutionRoutine(float onTime, float delay)
    {

        // wait for reset
        while(beeingReset)       
            yield return new WaitForSeconds(0.1f);

        SetColor(Color.black);
        yield return new WaitForSeconds(onTime * 2);
        for (int i = 0; i < level; i++)
        {
            SetColor(lookUpColor(solution[i]));
            yield return new WaitForSeconds(onTime);
            SetColor(Color.black);
            yield return new WaitForSeconds(delay);
        }
    }

    private bool InputCorrectSoFar(){
        for (int i = 0; i < solution.Length; i++)
        {
            // if there is no more input treat as correct
            if(input[i] == -1)
            {
                return true;
            }

            bool correct = solution[i] == input[i];
            if(!correct)
            {
                return false;
            }
        }

        return true;
    }


    private void GenerateSolution(){
        for (int i = 0; i < solution.Length ; i++)
        {
            solution[i] = Random.Range(0, 4);
        }
    }

}
