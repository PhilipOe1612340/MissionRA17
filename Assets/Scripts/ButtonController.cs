using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Transform lightRef;
    private Material lightMaterial;
    private int level = 1;
    private int inpIdx = 0;
    private int[] solution = new int[6];
    private int[] input;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        lightMaterial = lightRef.GetComponent<MeshRenderer>().material;
        setColor(Color.black);
        generateSolution();
        startCoroutine();
    }

    private void startCoroutine(){
        coroutine = ShowSolutionRoutine(1f);
        StartCoroutine(coroutine);
    }

    private IEnumerator ShowSolutionRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < level; i++)
        {
            setColor(lookUpColor(solution[i]));
            yield return new WaitForSeconds(waitTime);
        }

        // reset to black
        setColor(Color.black);
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
        print("level: " + level + " input " + num);
        StopCoroutine(coroutine);
        
        if(input == null){
            resetInput();
        }

        // save input
        input[inpIdx] = num;
        setColor(lookUpColor(num));

        inpIdx++;

        if(check()){

            // level complete
            if(inpIdx == level){
                level++;
                inpIdx = 0;
                input = null;
                startCoroutine();
            }

            // puzzle solved
            if(level == solution.Length + 1){
                print("complete");
            }

        } else {
            // reset to level 1
            level = 1;
            inpIdx = 0;
            input = null;
            // change solution
            generateSolution();
            startCoroutine();
        }

    }


    private void setColor(Color color){
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
        input = new int[5];
        for (int i = 0; i < input.Length; i++){
            input[i] = -1;
        }
    }

    private bool check(){
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
                print("would have been " + solution[i]);
                return false;
            }
        }

        return true;
    }


    private void generateSolution(){
        for (int i = 0; i < solution.Length ; i++)
        {
            solution[i] = Random.Range(0, 4);
        }
    }

}
