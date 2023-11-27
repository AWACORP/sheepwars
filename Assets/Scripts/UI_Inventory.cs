using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] public SlimeTypeSprite[] slimeSprites;
    private Inventory inventory;
    private Transform slimeSlotContainer;
    private Transform slimeSlotTemplate;

    private void Awake()
    {
        slimeSlotContainer = transform.Find("slimeSlotContainer");
        slimeSlotTemplate = slimeSlotContainer.Find("slimeSlotTemplate");
        slimeSlotTemplate.gameObject.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventorySlimes();
    }

    public void RefreshInventorySlimes()
    {
        //Détruire les enfants pour éviter les doublons
        foreach (Transform child in slimeSlotContainer)
        {
            if (child == slimeSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Slime slime in inventory.GetSlimeList())
        {
            RectTransform slimeSlotRectTransform = Instantiate(slimeSlotTemplate, slimeSlotContainer).GetComponent<RectTransform>();
            slimeSlotRectTransform.gameObject.SetActive(true);

            Image slimeImage = slimeSlotRectTransform.Find("slimeImage").GetComponent<Image>(); // Assurez-vous que le template a un enfant avec ce nom
            TextMeshProUGUI slimeText = slimeSlotRectTransform.Find("slimeText").GetComponent<TextMeshProUGUI>(); // Assurez-vous que le template a un enfant avec ce nom

            slimeImage.sprite = GetSpriteForSlimeType(slime.slimeType);
            slimeText.text = slime.amount.ToString();

            slimeImage.enabled = slime.amount >= 0;
            slimeText.enabled = slime.amount >= 0;
        }

    }

    public void UpdateSlimeSelectionUI()
    {
        for (int i = 0; i < slimeSlotContainer.childCount; i++)
        {
            Transform slotTransform = slimeSlotContainer.GetChild(i);
            // Activez la bordure dorée pour le slot sélectionné.
            slotTransform.Find("SelectedBorder").gameObject.SetActive(i == inventory.selectedSlimeIndex);
        }
    }

    private Sprite GetSpriteForSlimeType(Slime.SlimeType slimeType)
    {
        foreach (var slimeTypeSprite in slimeSprites)
        {
            if (slimeTypeSprite.slimeType == slimeType)
            {
                return slimeTypeSprite.sprite;
            }
        }
        // Retourner un sprite par défaut si aucun n'est trouvé
        return null;
    }

}
