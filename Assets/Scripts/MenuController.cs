using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : ABaseController
{

    public override void OnSpawn(GameManager manager)
    {
        base.OnSpawn(manager);

        MainMenu menu = OnRequestUI?.Invoke(EUI.MAIN_MENU).GetWidgetAs<MainMenu>();
        if (menu != null)
        {
            menu.OnSceneChangeRequest += OnSceneChange;
            menu.AddToViewport();
        }
    }

}
