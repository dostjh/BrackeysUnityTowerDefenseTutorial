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

	TurretBlueprint turretToBuild;
	Node selectedNode;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool CanAfford { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public NodeUI nodeUI;

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

	// TODO: Make this into a property with only a getter.
	public TurretBlueprint GetTurretToBuild()
	{
		return turretToBuild;
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}
}
