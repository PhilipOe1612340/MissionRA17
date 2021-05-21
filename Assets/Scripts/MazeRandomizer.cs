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

// [ExecuteInEditMode]
public class MazeRandomizer : MonoBehaviour
{

    public GameObject start;
    public GameObject goal;

    public AudioClip fail;
    public AudioClip win;
    private AudioSource audioData;

    private int mazeNr;
    private Vector2 goalPosition;

    private Wall[][] walls =
    {
        new Wall[] { 
            new Wall(new Vector2(1, 1), new bool[] {true, false, true, true}),
            new Wall(new Vector2(1, 3), new bool[] {true, false, false, true}),
            new Wall(new Vector2(1, 4), new bool[] {false, true, false, true}),
            new Wall(new Vector2(1, 5), new bool[] {false, true, true, false}),

            new Wall(new Vector2(2, 0), new bool[] {false, true, false, false}),
            new Wall(new Vector2(2, 1), new bool[] {false, true, true, false}),
            new Wall(new Vector2(2, 3), new bool[] {false, true, false, true}),
            new Wall(new Vector2(2, 4), new bool[] {false, false, true, true}),
            new Wall(new Vector2(2, 5), new bool[] {false, true, false, false}),

            new Wall(new Vector2(3, 2), new bool[] {true, true, false, false}),
            new Wall(new Vector2(3, 3), new bool[] {false, true, true, true}),
            new Wall(new Vector2(3, 4), new bool[] {false, true, false, false}),

            new Wall(new Vector2(4, 1), new bool[] {true, true, false, false}),
            new Wall(new Vector2(4, 2), new bool[] {false, true, true, true}),
            new Wall(new Vector2(4, 4), new bool[] {false, false, true, false}),

            new Wall(new Vector2(5, 3), new bool[] {false, false, true, false}),
        },

        new Wall[] { 
            new Wall(new Vector2(0, 1), new bool[] {false, true, false, false}),
            new Wall(new Vector2(0, 2), new bool[] {false, true, true, false}),
            new Wall(new Vector2(0, 3), new bool[] {false, true, false, false}),
            new Wall(new Vector2(0, 4), new bool[] {false, false, true, false}),

            new Wall(new Vector2(1, 1), new bool[] {false, true, false, false}),
            new Wall(new Vector2(1, 2), new bool[] {false, true, false, false}),
            new Wall(new Vector2(1, 3), new bool[] {false, true, false, false}),
            new Wall(new Vector2(1, 4), new bool[] {false, true, true, false}),

            new Wall(new Vector2(2, 1), new bool[] {true, true, false, false}),
            new Wall(new Vector2(2, 2), new bool[] {false, true, false, false}),

            new Wall(new Vector2(3, 3), new bool[] {true, true, false, false}),
            new Wall(new Vector2(3, 4), new bool[] {false, true, true, true}),

            new Wall(new Vector2(4, 1), new bool[] {true, false, true, false}),

            new Wall(new Vector2(5, 2), new bool[] {true, false, false, true}),
            new Wall(new Vector2(5, 3), new bool[] {false, false, false, true}),
            new Wall(new Vector2(5, 4), new bool[] {false, false, false, true}),
        },

        new Wall[] { 
            new Wall(new Vector2(0, 1), new bool[] {false, true, true, false}),
            new Wall(new Vector2(0, 4), new bool[] {true, true, false, false}),

            new Wall(new Vector2(1, 2), new bool[] {false, true, true, false}),
            new Wall(new Vector2(1, 4), new bool[] {false, true, true, false}),

            new Wall(new Vector2(2, 1), new bool[] {true, true, false, true}),
            new Wall(new Vector2(2, 3), new bool[] {false, false, false, true}),
            new Wall(new Vector2(2, 4), new bool[] {true, true, false, true}),

            new Wall(new Vector2(3, 0), new bool[] {false, false, true, false}),
            new Wall(new Vector2(3, 2), new bool[] {false, true, true, false}),
            new Wall(new Vector2(3, 3), new bool[] {false, true, false, false}),
            new Wall(new Vector2(3, 4), new bool[] {false, true, false, false}),

            new Wall(new Vector2(4, 1), new bool[] {true, true, false, false}),
            new Wall(new Vector2(4, 2), new bool[] {false, false, true, false}),
            new Wall(new Vector2(4, 4), new bool[] {false, true, false, false}),
            new Wall(new Vector2(4, 5), new bool[] {false, true, false, false}),

            new Wall(new Vector2(5, 2), new bool[] {false, false, true, false}),
        },
    };

    private Vector2 currentPosition;

    void Start(){
        audioData = GetComponent<AudioSource>();
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
        // check if a move leads out of bounds or through a wall
        if(!isAllowedMove(inp)){
            audioData.PlayOneShot(fail);
            reset();
            return;
        }

        // move 
        currentPosition += inp.toVector();

        start.SetActive(false);

        if(foundSolution()){
            audioData.PlayOneShot(win);
            reset();
        }
    }

    private bool foundSolution(){
        return currentPosition.x == goalPosition.x && currentPosition.y == goalPosition.y;
    }

    private bool isAllowedMove(InRec input){
        Vector2 nextPosition = currentPosition + input.toVector();

        if (nextPosition.x > 5f || nextPosition.x < 0f || nextPosition.y > 5f || nextPosition.y < 0f ){
            return false;
        }

        // check for walls
        Wall blockingWall = Array.Find(walls[mazeNr], element => element.blocks(currentPosition, nextPosition, input));
        return blockingWall == null;
    }

    private Vector3 getRandomPosition(out Vector2 position){
        int x = UnityEngine.Random.Range(0, 6);
        int y = UnityEngine.Random.Range(0, 6);
        position = new Vector2(x, y);
        return getPosition(position);
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

        mazeNr = UnityEngine.Random.Range(0, 3);

        int randomX = mazeNr / 3;
        int randomY = mazeNr % 3;
        mat.mainTextureOffset = new Vector2(baseOffset + randomX * tileOffset, baseOffset + randomY * tileOffset);

        // set start and goal position
        start.transform.localPosition = getRandomPosition(out currentPosition);
        goal.transform.localPosition = getRandomPosition(out goalPosition);

        start.SetActive(true);
    }

}
