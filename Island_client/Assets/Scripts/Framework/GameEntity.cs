using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ISL_Net;
using ISL_Data;

public class GameEntity: ISL_Helper.ISL_Instance<GameEntity>
{
    public NetworkManager gNetManager;
    public DataManager gDataManager;


    //游戏初始化
    public void InitGame()
    {
        gNetManager = NetworkManager.Instance;
        gDataManager = DataManager.Instance;

        gDataManager.Init();
        gNetManager.Init();


    }

     
    public void Tick()
    {

    }

}

