﻿using Geisha.Common.Math;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;

namespace Sokoban.Editor.UserInterface
{
    internal sealed class UserInterfaceEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public UserInterfaceEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        public Entity CreateCursor(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Editor.Cursor);
            spriteRendererComponent.SortingLayerName = "UI";

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    // Main Controls
                    new ActionMapping
                    {
                        ActionName = "MoveUp",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Up) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveDown",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Down) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveLeft",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Left) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveRight",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Right) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "Delete",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Delete) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "Exit",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Escape) } }
                    },
                    // Ground
                    new ActionMapping
                    {
                        ActionName = "RemoveGround",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F1) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SetBrownGround",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F2) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SetGreenGround",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F3) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SetGrayGround",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F4) } }
                    },
                    // Walls
                    new ActionMapping
                    {
                        ActionName = "CreateRedWall",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F5) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateRedGrayWall",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F6) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateGrayWall",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F7) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateBrownWall",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F8) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateRedWallTop",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F9) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateRedGrayWallTop",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F10) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateGrayWallTop",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F11) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateBrownWallTop",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F12) } }
                    },
                    // Crates
                    new ActionMapping
                    {
                        ActionName = "CreateBrownCrate",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Q) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateRedCrate",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.W) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateBrownCrateSpot",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.A) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateRedCrateSpot",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.S) } }
                    },
                    // Player
                    new ActionMapping
                    {
                        ActionName = "PlacePlayer",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.P) } }
                    }
                }
            };

            entity.CreateComponent<CursorComponent>();

            return entity;
        }

        public Entity CreateHelp(Scene scene)
        {
            var help = scene.CreateEntity();

            var transform2DComponent = help.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(-635, 100);

            var index = 0;
            AddHelpLabel(help, "Arrows - Move Cursor", index++);
            AddHelpLabel(help, "Delete - Remove Object", index++);
            AddHelpLabel(help, "Enter - Toggle Game/Edit Mode", index++);
            AddHelpLabel(help, "Esc - Exit Editor", index++);
            AddHelpLabel(help, string.Empty, index++);
            AddHelpLabel(help, "F1 - Remove Ground", index++);
            AddHelpLabel(help, "F2 - Set Brown Ground", index++);
            AddHelpLabel(help, "F3 - Set Green Ground", index++);
            AddHelpLabel(help, "F4 - Set Gray Ground", index++);
            AddHelpLabel(help, string.Empty, index++);
            AddHelpLabel(help, "F5 - Create Red Wall", index++);
            AddHelpLabel(help, "F6 - Create Red-Gray Wall", index++);
            AddHelpLabel(help, "F7 - Create Gray Wall", index++);
            AddHelpLabel(help, "F8 - Create Brown Wall", index++);
            AddHelpLabel(help, "F9 - Create Red Wall Top", index++);
            AddHelpLabel(help, "F10 - Create Red-Gray Wall Top", index++);
            AddHelpLabel(help, "F11 - Create Gray Wall Top", index++);
            AddHelpLabel(help, "F12 - Create Brown Wall Top", index++);
            AddHelpLabel(help, string.Empty, index++);
            AddHelpLabel(help, "Q - Create Brown Crate", index++);
            AddHelpLabel(help, "W - Create Red Crate", index++);
            AddHelpLabel(help, "A - Create Brown Crate Spot", index++);
            AddHelpLabel(help, "S - Create Red Crate Spot", index++);
            AddHelpLabel(help, string.Empty, index++);
            AddHelpLabel(help, "P - Place Player", index);

            return help;
        }

        private void AddHelpLabel(Entity help, string label, int index)
        {
            const int size = 14;
            var labelEntity = help.CreateChildEntity();
            var transform2DComponent = labelEntity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(0, -index * size);

            var textRendererComponent = labelEntity.CreateComponent<TextRendererComponent>();
            textRendererComponent.Text = label;
            textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            textRendererComponent.FontSize = FontSize.FromDips(size);
            textRendererComponent.SortingLayerName = "UI";
        }
    }
}