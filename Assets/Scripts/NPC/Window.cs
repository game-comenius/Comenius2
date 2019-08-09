using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Player player;

    [SerializeField]
    private NPC npc;

    /// <summary>
    /// ////////////////////////////---CLOSE_NPCS_WINDONS--////////////////////////////////////////
    /// </summary>
    public virtual void Open (NPC npc)
    {
        this.npc = npc;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// ////////////////////////////---CLOSE_MOST_WINDONS--////////////////////////////////////////
    /// </summary>
    public virtual void OpenMenus()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        //player.speed = 0f;
    }
    /// <summary>
    /// ////////////////////////////---CLOSE_NPCS_WINDONS--////////////////////////////////////////
    /// </summary>
    public virtual void Close()
    {
        npc.IsInteracting = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        npc = null;
    }
    /// <summary>
    /// ////////////////////////////---CLOSE_MOST_WINDONS--////////////////////////////////////////
    /// </summary>
    public virtual void CloseMenus()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //player.speed = 1.5f;
    }
}
