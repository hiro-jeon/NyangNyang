using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopSlotPrefab; // 에디터에서 추가
    public const int numSlots = 3;

    Image[] itemImages = new Image[numSlots];
    Text[] itemNames = new Text[numSlots];
    Text[] itemPrices = new Text[numSlots];
    Item[] items = new Item[numSlots];
    Button[] buyButtons = new Button[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    public Item testItem;

    void Start()
    {
        CreateSlots();
        AddItem(testItem);
    }

    public void CreateSlots()
    {
        if (shopSlotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(shopSlotPrefab);

                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).GetChild(1).transform);

                slots[i] = newSlot;
                itemImages[i] = newSlot.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
                itemNames[i] = newSlot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
                itemPrices[i] = newSlot.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
                buyButtons[i] = newSlot.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>();
            }
        }
    }

    public bool AddItem(Item itemToSet)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = Instantiate(itemToSet);
                items[i].quantity = 1;

                itemImages[i].enabled = true;
                itemImages[i].sprite = items[i].sprite;

                itemNames[i].text = items[i].objectName;
                itemPrices[i].text = items[i].price.ToString();
                slots[i].SetActive(true);
                buyButtons[i].onClick.AddListener(() => BuyItem(items[i]));
                return true;
            }
        }

        return false;
    }

    public bool BuyItem(Item itemToBuy)
    {
        int coin = loadMyCoin();
        Debug.Log("Coin: " + coin);
        if (coin >= itemToBuy.price)
        {
            Debug.Log("구매 진입");
            if (GameManager.sharedInstance.player.inventory.AddItem(itemToBuy))
            {
                Debug.Log("구매 성공!!");
                return Cash(itemToBuy.price);
            }
            else
            {
                Debug.Log("인벤토리 공간 부족");
                return false;
            }
        }
        return false;
    }

    public int loadMyCoin() 
    {
        for (int i = 0; i < GameManager.sharedInstance.player.inventory.items.Length; i++)
        {
            if (GameManager.sharedInstance.player.inventory.items[i] == null)
            {
                return (0);
            }
            else if (GameManager.sharedInstance.player.inventory.items[i].objectName == "coin")
            { 
                return GameManager.sharedInstance.player.inventory.items[i].quantity;
            }
        }
        return (0);
    }

    public bool Cash(int amount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (GameManager.sharedInstance.player.inventory.items[i] == null)
            {
                return true;
            }
            if (GameManager.sharedInstance.player.inventory.items[i].objectName == "coin")
            {
                GameManager.sharedInstance.player.inventory.items[i].quantity -= amount;
                return true;
            }
        }
        return false;
    }
}