using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
	private Node targetNode;
	public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;

	public Text sellAmount;

	public void SetTargetNode(Node node)
	{
		targetNode = node;

		transform.position = node.GetBuildPosition();

		if (!targetNode.IsUpgraded)
		{
			upgradeCost.text = $"${node.turretBlueprint.upgradeCost}";
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeCost.text = "COMPLETE";
			upgradeButton.interactable = false;
		}

		sellAmount.text = $"${node.turretBlueprint.GetSellAmount()}";

		ui.SetActive(true);
	}

	public void Hide()
	{
		ui.SetActive(false);
	}

	public void Upgrade()
	{
		targetNode.UpgradeTurret();
		// Deselect the node to hide the UI after we upgrade.
		BuildManager.instance.DeselectNode();
	}

	public void Sell()
	{
		targetNode.SellTurret();
		BuildManager.instance.DeselectNode();
	}
}
