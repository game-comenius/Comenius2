﻿//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable
//{
//    public IUseable MyUseable { get; set; }


//    [SerializeField]
//    private Text sactackSize;

//    public Button MyButton { get; private set; }

//    private Stack<IUseable> useables =  new Stack<IUseable>();

//    private int count;

//    public Image MyIcon
//    {
//        get
//        {
//            return Icon;
//        }

//        set
//        {
//            Icon = value;
//        }
//    }

//    public int MyCount
//    {
//        get
//        {
//            return count;
//        }
//    }

//    public Text MyStackText
//    {
//        get
//        {
//            return sactackSize;
//        }
//    }

//    [SerializeField]
//    private Image Icon;
    
    

//    // Use this for initialization
//    void Start () {
//        MyButton = GetComponent<Button>();
//        MyButton.onClick.AddListener(OnClick);
//        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpadateItemCount);
//    }

//    // Update is called once per frame
//    void Update () {
		
//	}

//    public void OnClick()
//    {
//        if (HandScript.MyInstance == null)
//        {
//            if (MyUseable != null)
//            {
//                MyUseable.Use();
//            }
//            if (useables != null && useables.Count > 0)
//            {
//                useables.Peek().Use();
//            }
//        }
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
//            {
//                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
//            }
//        }
//    }



//    public void SetUseable(IUseable useable)
//    {
//        if (useable is Item)
//        {
//            useables = InventoryScript.MyInstance.GetUseables(useable);
//            count = useables.Count;
//            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
//            InventoryScript.MyInstance.FromSlot = null;
//        }
//        else
//        {
//            this.MyUseable = useable;
//        }

//        UpdateVisual();
//    }

//    public void UpdateVisual()
//    {
//        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
//        MyIcon.color = Color.white;

//        if (count > 1)
//        {
//            UIManager.MyInstance.UpdateStackSize(this);
//        }
//    }

//    public void UpadateItemCount (Item item)
//    {
//       if (item  is IUseable && useables.Count > 0)
//        {
//            if (useables.Peek().GetType() == item.GetType())
//            {
//                useables = InventoryScript.MyInstance.GetUseables(item as IUseable);

//                count = useables.Count;

//                UIManager.MyInstance.UpdateStackSize(this);
//            }
//        }
//    }

//}
