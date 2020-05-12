using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color insufficientFundsColor;
	public Color hoverColor;

	[Tooltip("Y-axis offset so object placement isn't embedded in node")]
	public Vector3 positionOffset;

	[HideInInspector]
	public GameObject Turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool IsUpgraded = false;

	Renderer rend;
	Color startColor;

	BuildManager buildManager;

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	// TODO: public void UpgradeAllOfType()

	public void UpgradeTurret()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			// TODO: Display status to user.
			Debug.Log($"Not enough money to upgrade that -- Current: {PlayerStats.Money}; Cost: {turretBlueprint.cost}");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

		// Remove previous turret from map
		Destroy(Turret);

		// Build the new turret
		// NOTE: Quaternion.identity means we don't rotate
		var turret = Instantiate(turretBlueprint.upgradedTurretPrefab, GetBuildPosition(), Quaternion.identity);
		Turret = turret;

		// TODO: Make this part of the turret prefab instead of having a single build effect for all turrets so that we can call the appropriate effect for the turret.
		// TODO: Make a different turret build?
		var buildEffect = Instantiate(turretBlueprint.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
		Destroy(buildEffect, 5.0f);

		IsUpgraded = true;

		Debug.Log($"Turret upgraded for {turretBlueprint.upgradeCost}. Money remaining: {PlayerStats.Money}");
	}

	public void SellTurret()
	{
		var sellAmount = turretBlueprint.GetSellAmount();
		PlayerStats.Money += sellAmount;

		var sellEffect = Instantiate(turretBlueprint.sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
		Destroy(sellEffect, 5.0f);

		Destroy(Turret);
		turretBlueprint = null;

		Debug.Log($"Turret sold for {sellAmount}. Money remaining: {PlayerStats.Money}");
	}

	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
	}

	void OnMouseDown()
	{
		// Don't allow animation or clicking when hovering over an existing UI element.
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (Turret != null)
		{
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		BuildTurret(buildManager.GetTurretToBuild());
	}

	void BuildTurret(TurretBlueprint turretToBuild)
	{
		if (PlayerStats.Money < turretToBuild.cost)
		{
			// TODO: Display status to user.
			Debug.Log($"Not enough money to build that -- Current: {PlayerStats.Money}; Cost: {turretToBuild.cost}");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;

		// NOTE: Quaternion.identity means we don't rotate
		var turret = Instantiate(turretToBuild.turretPrefab, GetBuildPosition(), Quaternion.identity);
		Turret = turret;

		turretBlueprint = turretToBuild;

		// TODO: Make this part of the turret prefab instead of having a single build effect for all turrets so that we can call the appropriate effect for the turret.
		var buildEffect = Instantiate(turretToBuild.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
		Destroy(buildEffect, 5.0f);

		Debug.Log($"Turret built for {turretToBuild.cost}. Money remaining: {PlayerStats.Money}");
	}

	// Hover animation
	void OnMouseEnter()
	{
		// Don't allow animation or clicking when hovering over an existing UI element.
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		if (buildManager.CanAfford)
		{
			rend.material.color = hoverColor;
		}
		else
		{
			rend.material.color = insufficientFundsColor;
		}
	}

	// Undo hover animation
	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
