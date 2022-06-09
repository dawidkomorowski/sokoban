﻿using System;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Components;

namespace Sokoban
{
    internal sealed class GameEntityFactory
    {
        private readonly IAssetStore _assetStore;
        private readonly IEngineManager _engineManager;

        public GameEntityFactory(IAssetStore assetStore, IEngineManager engineManager)
        {
            _assetStore = assetStore;
            _engineManager = engineManager;
        }

        public void CreateRestartLevelEntity(Scene scene)
        {
            var entity = scene.CreateEntity();
            entity.CreateComponent<RestartLevelComponent>();
        }

        public Entity CreateInGameMenu(Scene scene)
        {
            var inGameMenu = scene.CreateEntity();

            inGameMenu.CreateComponent<Transform2DComponent>();

            var rectangleRendererComponent = inGameMenu.CreateComponent<RectangleRendererComponent>();
            rectangleRendererComponent.Color = Color.FromArgb(150, 0, 0, 0);
            rectangleRendererComponent.FillInterior = true;
            rectangleRendererComponent.Dimension = new Vector2(1280, 720);
            rectangleRendererComponent.SortingLayerName = "UI";
            rectangleRendererComponent.OrderInLayer = 0;

            var inputComponent = inGameMenu.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "ToggleMenu",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Escape) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "OptionUp",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Up) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "OptionDown",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Down) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SelectOption",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Enter) } }
                    }
                }
            };

            inGameMenu.CreateComponent<InGameMenuComponent>();

            var menuOptionsContainer = inGameMenu.CreateChildEntity();
            var menuOptionsContainerTransform = menuOptionsContainer.CreateComponent<Transform2DComponent>();
            menuOptionsContainerTransform.Translation = new Vector2(-300, 100);

            CreateInGameMenuOption(menuOptionsContainer, "Restart level", 0, () => { CreateRestartLevelEntity(scene); });
            CreateInGameMenuOption(menuOptionsContainer, "Exit", 1, () => { _engineManager.ScheduleEngineShutdown(); });

            return inGameMenu;
        }

        private void CreateInGameMenuOption(Entity menuOptionsContainerEntity, string text, int index, Action action)
        {
            var entity = menuOptionsContainerEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(0, -index * 100);

            var textRendererComponent = entity.CreateComponent<TextRendererComponent>();
            textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            textRendererComponent.Text = text;
            textRendererComponent.FontSize = FontSize.FromDips(100);
            textRendererComponent.SortingLayerName = "UI";
            textRendererComponent.OrderInLayer = 1;

            var inGameMenuOptionComponent = entity.CreateComponent<InGameMenuOptionComponent>();
            inGameMenuOptionComponent.Index = index;
            inGameMenuOptionComponent.Action = action;
        }
    }
}