//------------------------------------------------
//	script	:	TileMapActiveHighlight.cs
//	author	:	dwl398
//------------------------------------------------

using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

[InitializeOnLoad]
public static class TileMapActiveHighlight
{
	private const string MenuPath = "Tools/TileMapActiveHighlight/Enable";

	private static readonly Color HighlightColor = new Color(1, 1, 1, 1);

	private static readonly Color LowlightColor = new Color(1, 1, 1, 0.5f);

	private static bool _enable = false;

	[MenuItem(MenuPath)]
	private static void ChangeEnable()
	{
		_enable = !_enable;

		Menu.SetChecked(MenuPath, _enable);

		if(_enable == false)
		{
			Tilemap[] tilemaps = GetTilemaps();
			ClearHightlight(tilemaps);
		}
	}

	static TileMapActiveHighlight()
	{
		Menu.SetChecked(MenuPath, _enable);

		GridPaintingState.scenePaintTargetChanged += OnAcitveChanged;

		EditorApplication.playModeStateChanged += OnPlayModeStateChanded;
	}

	private static void OnAcitveChanged(GameObject gameObject)
	{
		if(EditorApplication.isPlaying)
		{
			return;
		}

		if(_enable == false)
		{
			return;
		}

		Tilemap[] tilemaps = GetTilemaps();
		RefleshHightlight(tilemaps, gameObject);
	}

	private static void OnPlayModeStateChanded(PlayModeStateChange state)
	{
		if(state != PlayModeStateChange.ExitingEditMode)
		{
			return;
		}

		if(_enable == false)
		{
			return;
		}

		Tilemap[] tilemaps = GetTilemaps();
		ClearHightlight(tilemaps);
	}

	private static void RefleshHightlight(Tilemap[] tilemaps, GameObject activeTilemap)
	{
		foreach(Tilemap tilemap in tilemaps)
		{
			bool isActiveTilemap = string.Equals(activeTilemap.name, tilemap.name);

			tilemap.color = isActiveTilemap ? HighlightColor : LowlightColor;
		}
	}

	private static void ClearHightlight(Tilemap[] tilemaps)
	{
		foreach(Tilemap tilemap in tilemaps)
		{
			tilemap.color = HighlightColor;
		}
	}

	private static Tilemap[] GetTilemaps()
	{
		return GameObject.FindObjectsOfType<Tilemap>();
	}
}
