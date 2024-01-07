using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField] TextMeshPro title, description;
    [SerializeField] SpriteRenderer rarityImage, iconImage;
    [SerializeField] Sprite[] rarities;
    bool selecting;


    public void setUpgrade(string title, string rarity)
    {
        SetRarity(rarity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRarity(string rarity)
    {
        switch (rarity)
        {
            case "common":
                rarityImage.sprite = rarities[0];
                break;
            case "uncommon":
                rarityImage.sprite = rarities[1];
                break;
            case "rare":
                rarityImage.sprite = rarities[2];
                break;
            case "epic":
                rarityImage.sprite = rarities[3];
                break;
            case "legendary":
                rarityImage.sprite = rarities[4];
                break;
            default:
                rarityImage.sprite = rarities[0];
                break;
        }
    }

    private void UpgradeSelected()
    {

    }

    IEnumerator IndicateSelection()
    {
        selecting = true;
        float selectionTime = 0f;
        while (selecting && selectionTime < 1f)
        {
            yield return new WaitForSeconds(0.1f);
            selectionTime += 0.1f;
        }
        if (selecting)
        {
            UpgradeSelected();
        } else
        {

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(IndicateSelection());
    }

    private void OnTriggerExit(Collider other)
    {
        selecting = false;
    }
}
