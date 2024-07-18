using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMode : MonoBehaviour
{

    public static int modeChoose = 0; //0 is normal mode
    public Character character1;
    public Character character2;





    private void ChangeMode()
    {
        if (modeChoose == 1) //1: crazy mode (raise car and player speed and throw more material)
        {
            character1.PlayerSpeed = 6;
            character2.PlayerSpeed = 6;
            Character.bigScale = 1;
            carMove.speed = 8f;
            carMoveSenceCity.speed = 15f;
            carMoveSenceDarkCity.speed = 10f;
            CarThrow.modeChange = 0.3f;
            CarThrow.bigScale = 1f;
            Furnace.bigScale = 1;
            Trees.bigScale = 1;
        }

        else if (modeChoose == 2) //2: big scale mode (improve scale of material and raise player speed)
        {
            character1.PlayerSpeed = 6;
            character2.PlayerSpeed = 6;
            Character.bigScale = 5;
            carMove.speed = 3.5f;
            carMoveSenceCity.speed = 10;
            carMoveSenceDarkCity.speed = 5.5f;
            CarThrow.modeChange = 1f;
            CarThrow.bigScale = 5;
            Furnace.bigScale = 5;
            Trees.bigScale = 5;
        }
        else if (modeChoose == 0) // normal mode
        {
            character1.PlayerSpeed = 2;
            character2.PlayerSpeed = 2;
            Character.bigScale = 1;
            carMove.speed = 3.5f;
            carMoveSenceCity.speed = 10;
            carMoveSenceDarkCity.speed = 5.5f;
            CarThrow.modeChange = 1f;
            CarThrow.bigScale = 1f;
            Furnace.bigScale = 1;
            Trees.bigScale = 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        character1 = GameObject.Find("animal_people_wolf_1").GetComponent<Character>();
        character2 = GameObject.Find("animal_people_wolf_2").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMode();
    }
}
