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
            character1.PlayerSpeed = 4;
            character2.PlayerSpeed = 4;
            Character.bigScale = 1;
            carMove.speed = 8f;
            carMoveSenceCity.speed = 15f;
            carMoveSenceDarkCity.speed = 10f;
            CarThrow.intervalChange = 0.2f;
            Bomb.explosionRadius = 1.5f;
            CarThrow.bombRandom = 1;
            TrafficControl.minTime =2;
            TrafficControl.maxTime = 4;
            if (SceneController.sceneID == 1)//city
            {
                CarThrow.bigScale = 2;
                Furnace.bigScale = 2;
                Trees.bigScale = 2;
                ThrowBomb.bigScale = 2;
            }
            else
            {
                CarThrow.bigScale = 1f;
                Furnace.bigScale = 1;
                Trees.bigScale = 1;
                ThrowBomb.bigScale = 1;
            }
        }

        else if (modeChoose == 2) //2: big scale mode (improve scale of material and raise player speed)
        {
            character1.PlayerSpeed = 6;
            character2.PlayerSpeed = 6;
            carMove.speed = 3.5f;
            carMoveSenceCity.speed = 10;
            carMoveSenceDarkCity.speed = 5.5f;
            CarThrow.intervalChange = 1f;
            CarThrow.bombRandom = 0;
            CarThrow.bigScale = 4;
            Character.bigScale = 4;
            Furnace.bigScale = 4;
            Trees.bigScale = 4;
            ThrowBomb.bigScale = 4;
            Bomb.explosionRadius = 4;
            TrafficControl.minTime =4;
            TrafficControl.maxTime = 6;

        }
        else if (modeChoose == 0) // normal mode
        {
            character1.PlayerSpeed = 2;
            character2.PlayerSpeed = 2;
            Character.bigScale = 1;
            carMove.speed = 3.5f;
            carMoveSenceCity.speed = 10;
            carMoveSenceDarkCity.speed = 5.5f;
            CarThrow.intervalChange = 1f;
            CarThrow.bombRandom = 1;
            Bomb.explosionRadius = 1.5f;
            TrafficControl.minTime =4;
            TrafficControl.maxTime = 6;
            if (SceneController.sceneID == 1)//city
            {
                CarThrow.bigScale = 2;
                Furnace.bigScale = 2;
                Trees.bigScale = 2;
                ThrowBomb.bigScale = 2;
            }
            else
            {
                CarThrow.bigScale = 1f;
                Furnace.bigScale = 1;
                Trees.bigScale = 1;
                ThrowBomb.bigScale = 1;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        character1 = GameObject.Find("animal_people_wolf_1").GetComponent<Character>();
        character2 = GameObject.Find("animal_people_wolf_2").GetComponent<Character>();
        ChangeMode();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
