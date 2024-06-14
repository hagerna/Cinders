using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Spawner leftSpawner;
    [SerializeField] Spawner rightSpawner;
    PlayerHUD hud;
    Torch torch;
    FlameHand flameHand;
    Campfire campfire;
    UpgradeSelector upgradeSelector;


    // Start is called before the first frame update
    void Awake()
    {
        hud = FindObjectOfType<PlayerHUD>();
        torch = FindObjectOfType<Torch>();
        flameHand = FindObjectOfType<FlameHand>();
        campfire = FindObjectOfType<Campfire>();
        upgradeSelector = FindObjectOfType<UpgradeSelector>();
    }

    private void Start()
    {
        GameManager.instance.newRun += (RunSettings obj) => StartTutorialSequence();
        torch.useable = false;
        flameHand.useable = true;
        campfire.immune = true;
        upgradeSelector.ClearUpgradeOptions();

    }

    void StartTutorialSequence()
    {
        hud.ShowMessage("You awake as the sun sets to the smell of smoke and ash. \nA good thing...");
        hud.QueueMessage("As darkness begins to fall, \nlight your Torch when you are ready to face the night...");
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(10f);
        torch.useable = true;
        while (!torch.IsLit()) yield return new WaitForSeconds(0.2f);

        hud.ShowMessage("The words of your oath ring in your mind: \n\"Protect the Flame at all costs...\"");
        hud.QueueMessage("The Lost are drawn to its magic, don't let them reach it!");
        yield return new WaitForSeconds(12f);
        leftSpawner.SpawnEnemies("Ghost");
        hud.QueueMessage("More are coming!");
        yield return new WaitForSeconds(6f);
        rightSpawner.SpawnEnemies("Ghost");
        leftSpawner.SpawnEnemies("Ghost");
        while (FindAnyObjectByType<Enemy>()) yield return null;

        hud.ShowMessage("The Torch can only survive so many hits before it must be relit! \nPrepare as more come...");
        yield return new WaitForSeconds(9f);
        rightSpawner.SpawnEnemies("Ghost", 2, 3f);
        yield return new WaitForSeconds(1f);
        leftSpawner.SpawnEnemies("Ghost", 2, 6f);
        yield return new WaitForSeconds(4f);
        while (FindAnyObjectByType<Enemy>()) yield return null;

        hud.ShowMessage("The Lost will attempt to smother the Flame. If too many reach it, the Flame will go out...");
        hud.QueueMessage("But your Torch is not your only tool to fend them off...");
        hud.QueueMessage("As one of the Cinderguard you can shape and wield fire itself...");
        yield return new WaitForSeconds(hud.displayTime * 2);
        hud.ShowTooltip("( Hold Trigger to create a Firebolt and Release to throw )", 1f, true);
        flameHand.useable = true;
        while (FindAnyObjectByType<Firebolt>() == null) yield return null;

        yield return new WaitForSeconds(2f);
        hud.ClearTooltip();
        hud.ShowMessage("Now use what you have learned to survive the night!");
        yield return new WaitForSeconds(hud.displayTime);
        leftSpawner.SpawnEnemies("Ghost", 4, 10f);
        yield return new WaitForSeconds(2f);
        rightSpawner.SpawnEnemies("Ghost", 4, 10f);
        while (FindAnyObjectByType<Enemy>()) yield return null;

        hud.ShowMessage("The sun rises on a new day, and with it the Flame grows stronger...");
        hud.QueueMessage("After each night you will have the opportunity to select an upgrade!");
        yield return new WaitForSeconds(6f);
        upgradeSelector.CreateCustomUpgrades(
            types: new System.Type[3] { typeof(TorchDamageUpgrade), typeof(TorchHitsUpgrade), typeof(FireboltThrowSpeedUpgrade) },
            rarities: new Rarity[3] { Rarity.Common, Rarity.Uncommon, Rarity.Common }
            );
        hud.ShowTooltip("( Select an upgrade by holding your torch to its brazier )");
        while (upgradeSelector.IsDisplayed()) yield return null;


    }
}
