using UnityEngine;

public class BuildManager : MonoBehaviour
{
	#region Singleton implementation
	// NOTE: Singleton pattern - Only one instance of BuildManager in the scene 
	// that is easily accessible by everything in the scene.
	public static BuildManager instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}
	#endregion

	public GameObject buildEffectPrefab;

	TurretBlueprint turretToBuild;
	Node selectedNode;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool CanAfford { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public NodeUI nodeUI;

	public void BuildTurretOn(Node node)
	{
		if (PlayerStats.Money < turretToBuild.cost)
		{
			// TODO: Display status to user.
			Debug.Log($"Not enough money to build that -- Current: {PlayerStats.Money}; Cost: {turretToBuild.cost}");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;
		
		// NOTE: Quaternion.identity means we don't rotate
		var turret = Instantiate(turretToBuild.turretPrefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		// TODO: Make this part of the turret prefab instead of having a single build effect for all turrets so that we can call the appropriate effect for the turret.
		var buildEffect = Instantiate(turretToBuild.buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
		Destroy(buildEffect, 5.0f);

		Debug.Log($"Turret built for {turretToBuild.cost}. Money remaining: {PlayerStats.Money}");
	}

	// TODO: Move build logic to the menu that appears on a node rather than doing this. See SelectTurretToBuild(). Prior art: Ancient Planet.
	public void SelectNode(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		turretToBuild = null;

		nodeUI.SetTargetNode(node);
	}

	// TODO: Move build logic to the menu that appears on a node rather than doing this. See SelectNode(). Prior art: Ancient Planet.
	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		turretToBuild = turret;

		DeselectNode();
	}

	void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}
}
