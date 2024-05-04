﻿using SpaceBattles.UI.Pages;

namespace SpaceBattles.UI.Tests.Pages;

public class UniverseCreatorTests : TestContext
{
    public UniverseCreatorTests()
    {
        Services.AddMudServices();
        JSInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);

        Services.AddCoreSpaceBattlesServices();
    }
    
    [Fact]
    public void Render_Component()
    {
        //  Arrange
        
        // Act
        RenderComponent<UniverseCreator>();

        // Assert
        Assert.True(true);
    }
}