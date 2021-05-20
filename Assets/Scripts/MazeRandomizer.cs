using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Wall{
    Vector2 pos;
    bool[] block;

    public Wall(Vector2 position, bool[] blocked){
        pos = position;
        block = blocked;
    }

    public bool blocks(Vector2 start, Vector2 target, InRec move){

        if(start != pos && target != pos ){
            return false;
        }

        Vector2 a = start;
        Vector2 b = target;
        bool invert = false;

        if(target == pos){
            a = target;
            b = start;
            invert = true;
        }

        int dir;
        if(invert){
            dir = (move.rec + 2) % 4;
        } else {
            dir = move.rec;
        }

        return block[dir];
    }
}

class InRec {
    public int rec;
    public int num;

    public InRec(int record){
        rec = record;
        num = 1;
    }

    public InRec(int record, int number){
        rec = record;
        num = number;
    }

    public Vector2 toVector(){
        switch(rec){
            case 0: return Vector2.down;
            case 1: return Vector2.right;
            case 2: return Vector2.up;
            case 3: return Vector2.left;
        }

        return new Vector2(0, 0);
    }

    public string toString(){
        string type = "";
        switch(rec){
            case 0: type = "up"; break;
            case 1: type = "right"; break;
            case 2: type = "down"; break;
            case 3: type = "left";  break;
        }

        return "input: " + type + " x" + num;
    }
}

[ExecuteInEditMode]
public class MazeRandomizer : MonoBehaviour
{

    public Transform start;
    public Transform goal;

    public int mazeNr;
    private Vector2[] startPositions = {
        new Vector2(1, 0), 
        new Vector2(5, 4)
    };

    private Vector2[] goalPositions = {
        new Vector2(0, 3), 
        new Vector2(4, 5)
    };

    private InRec[][] solutions =
    {
        new InRec[] { 
            new InRec(0, 1),    // left once
            new InRec(3, 3),    // 3x down
        },
    };

    private Wall[][] walls =
    {
        new Wall[] { 
            new Wall(new Vector2(1, 1), new bool[] {true, false, true, true}), 
            new Wall(new Vector2(1, 3), new bool[] {true, false, false, true}), 
            new Wall(new Vector2(1, 4), new bool[] {true, false, true, false}), 
            new Wall(new Vector2(1, 5), new bool[] {false, false, true, true}), 
        },
    };

    private Vector2 currentPosition;

    private int inputCount = 0;

    void Start()
    {
      reset();
    }

    public void inputLeft(){
        input(new InRec(3));
    }
    public void inputUp(){
        input(new InRec(0));
    }
    public void inputRight(){
        input(new InRec(1));
    }
    public void inputDown(){
        input(new InRec(2));
    }

    private void input(InRec inp){
        // print("moved " + inp.toString() + "  " + inp.toVector());
        if(!isAllowedMove(inp)){
            print("move not allowed");
            reset();
            return;
        }

        currentPosition += inp.toVector();
        start.transform.localPosition = getPosition(currentPosition);

        if(foundSolution()){
            print("won!!");
            reset();
        }
    }

    private bool foundSolution(){
        var goalPos = goalPositions[mazeNr];
        return currentPosition.x == goalPos.x && currentPosition.y == goalPos.y;
    }

    private bool isAllowedMove(InRec input){
        Vector2 nextPosition = currentPosition + input.toVector();

        // print("nextPosition: " + nextPosition);

        if (nextPosition.x > 6f || nextPosition.x < 0f || nextPosition.y > 6f || nextPosition.y < 0f ){
            return false;
        }

        // check for walls
        Wall blockingWall = Array.Find(walls[mazeNr], element => element.blocks(currentPosition, nextPosition, input));
        return blockingWall == null;
    }

    private Vector3 getPosition(Vector2 position){
        float xPos = remap(position.x, 0f, 5f, 4.15f, -4.27f);
        float zPos = remap(position.y, 0f, 5f, 4.2f, -4.21f);
        return new Vector3(zPos, 0.3f, xPos);
    }

    private float remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void reset(){
        // choose random texture

        var mat = GetComponent<MeshRenderer>().material;
        float baseOffset = 0.001f;
        float tileOffset = 1f / 3f;

        mazeNr = 0; // Random.Range(0, 10);

        int randomX = mazeNr / 3;
        int randomY = mazeNr % 3;
        mat.mainTextureOffset = new Vector2(baseOffset + randomX * tileOffset, baseOffset + randomY * tileOffset);


        // set start and goal position
        start.transform.localPosition = getPosition(startPositions[mazeNr]);
        goal.transform.localPosition = getPosition(goalPositions[mazeNr]);
        currentPosition = startPositions[mazeNr];
        inputCount = 0;
    }

}
