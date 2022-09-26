﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class LpcSpriteProcessor : AssetPostprocessor {

	enum LpcAnimationState {
		Hurt,
		Shoot,
		Slash,
		Walkcycle,
		Thrust,
		Spellcast
	}

	private const int LPC_SHEET_WIDTH = 832;
	private const int LPC_SHEET_HEIGHT = 1344;
	private const int LPC_SPRITE_SIZE = 64;

	private int m_PixelsPerUnit; // Sets the Pixels Per Unit in the Importer
	private int m_ScFrames;      // Spellcast animation frames
	private int m_ThFrames;      // Thrust animation frames
	private int m_WcFrames;      // Walkcycle animation frames
	private int m_SlFrames;      // Slash animation frames
	private int m_ShFrames;      // Shoot animation frames
	private int m_HuFrames;      // Hurt animation frames

	private bool m_ImportEmptySprites;
	private int m_ColCount;
	private int m_RowCount;

	void RetrieveSettings() {
		// Retrieve Basic Settings
		m_ImportEmptySprites = LpcSpriteSettings.GetImportEmptySprites();
		m_PixelsPerUnit = LpcSpriteSettings.GetPixelsPerUnit();

		// Retrieve Animation Settings
		m_ScFrames = LpcSpriteSettings.GetScFrameCount();
		m_ThFrames = LpcSpriteSettings.GetThFrameCount();
		m_WcFrames = LpcSpriteSettings.GetWcFrameCount();
		m_SlFrames = LpcSpriteSettings.GetSlFrameCount();
		m_ShFrames = LpcSpriteSettings.GetShFrameCount();
		m_HuFrames = LpcSpriteSettings.GetHuFrameCount();

		// Retrieve Other Settings
		m_ColCount = LpcSpriteSettings.GetColCount();
		m_RowCount = LpcSpriteSettings.GetRowCount();
	}

    private void SetDefaultTextureSettings() {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spriteImportMode = SpriteImportMode.Multiple;
        textureImporter.mipmapEnabled = false;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.spritePixelsPerUnit = m_PixelsPerUnit;
        textureImporter.spritePackingTag = "character";
    }

    void OnPreprocessTexture() {
		RetrieveSettings();
		SetDefaultTextureSettings();
	}

	private bool IsTransparent(Texture2D texture, Rect rect) {
        for (int x = (int)rect.xMin; x < rect.xMax; x++)
            for (int y = (int)rect.yMin; y < rect.yMax; y++)
                if (texture.GetPixel(x, y).a != 0)
                    return false;
        return true;
    }

	public void OnPostprocessTexture(Texture2D texture) {
		// Do nothing if it not a LPC Based Sprite
		if (!assetPath.Contains("LPC/spritesheets"))
			return;

		if (!IsLpcSpriteSheet(texture)) {
			Debug.LogWarning($"The following file is not a LPC spritesheet: {assetPath}");
			return;
		}

		Debug.Log("Importing LPC Character Sheet");
		List<SpriteMetaData> spriteMetadata = new List<SpriteMetaData>();
		for (int row = 0; row < m_RowCount; ++row) {
			for (int col = 0; col < m_ColCount; ++col) {
				var spriteRect = new Rect(
					col * LPC_SPRITE_SIZE,
					texture.height - ((row + 1) * LPC_SPRITE_SIZE),
					LPC_SPRITE_SIZE,
					LPC_SPRITE_SIZE);
				
				if (IsTransparent(texture, spriteRect))
					continue;

				SpriteMetaData meta = new() {
					rect = spriteRect
				};

				LpcAnimationState animState = GetAnimationState(row);

				if (!m_ImportEmptySprites) {
					if (animState == LpcAnimationState.Hurt && col >= m_HuFrames)
						break;
					if (animState == LpcAnimationState.Shoot && col >= m_ShFrames)
						break;
					if (animState == LpcAnimationState.Slash && col >= m_SlFrames)
						break;
					if (animState == LpcAnimationState.Thrust && col >= m_ThFrames)
						break;
					if (animState == LpcAnimationState.Walkcycle && col >= m_WcFrames)
						break;
					if (animState == LpcAnimationState.Spellcast && col >= m_ScFrames)
						break;
				}

				string[] path_branch = assetImporter.assetPath.Split('/');

				string prefix = "";
				for (int i = 4; i < path_branch.Length; i++) {
					string node = path_branch[i];
					string[] split_node = node.Split('.');

					prefix += string.Format("{0}_", split_node[0]);
				}

				string namePrefix = ResolveLpcNamePrefix(row, prefix);

				meta.name = namePrefix + col;
				spriteMetadata.Add(meta);
			}
		}
		TextureImporter textureImporter = (TextureImporter)assetImporter;
		textureImporter.spritesheet = spriteMetadata.ToArray();
		Debug.Log($"LPC Spritesheet {assetPath} has been imported successfully");
	}

	public void OnPostprocessSprites(Texture2D texture, Sprite[] sprites) {
		Debug.Log("Sliced Sprites: " + sprites.Length);
	}

	// Check if a texture is a LPC Spritesheet by
	// checking the textures width and height
	private bool IsLpcSpriteSheet(Texture2D texture) {
		if(texture.height <= LPC_SHEET_HEIGHT &&
			texture.width <= LPC_SHEET_WIDTH) {
			return true;
		} else {
			return false;
		}
	}

	private LpcAnimationState GetAnimationState(int row) {
		switch (row) {
			case (0):
			case (1):
			case (2):
			case (3):
                return LpcAnimationState.Spellcast;
            case (4):
			case (5):
			case (6):
			case (7):
                return LpcAnimationState.Thrust;
            case (8):
			case (9):
			case (10):
			case (11):
                return LpcAnimationState.Walkcycle;
            case (12):
			case (13):
			case (14):
			case (15):
                return LpcAnimationState.Slash;
            case (16):
			case (17):
			case (18):
			case (19):
                return LpcAnimationState.Shoot;
            case (20):
                return LpcAnimationState.Hurt;
            default:
				Debug.LogError("GetAnimationState unknown row: " + row);
				return 0;
		}
	}

	private string ResolveLpcNamePrefix(int row, string prefix) {
		switch (row) {
            case (0):
                return prefix + "sc_t_";
            case (1):
				return prefix + "sc_l_";
            case (2):
                return prefix + "sc_d_";
            case (3):
                return prefix + "sc_r_";
            case (4):
                return prefix + "th_t_";
            case (5):
                return prefix + "th_l_";
            case (6):
                return prefix + "th_d_";
            case (7):
                return prefix + "th_r_";
            case (8):
                return prefix + "wc_t_";
            case (9):
                return prefix + "wc_l_";
            case (10):
                return prefix + "wc_d_";
            case (11):
                return prefix + "wc_r_";
            case (12):
                return prefix + "sl_t_";
            case (13):
                return prefix + "sl_l_";
            case (14):
                return prefix + "sl_d_";
            case (15):
                return prefix + "sl_r_";
            case (16):
                return prefix + "sh_t_";
            case (17):
                return prefix + "sh_l_";
            case (18):
                return prefix + "sh_d_";
            case (19):
                return prefix + "sh_r_";
            case (20):
                return prefix + "hu_";
            default:
				Debug.LogError("ResolveLpcNamePrefix unknown row: " + row);
				return prefix + "";
		}
	}
}
