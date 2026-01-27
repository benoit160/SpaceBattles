resource "azurerm_service_plan" "asp" {
  name                = "spacebattles-asp"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  os_type             = "Windows"
  sku_name            = "F1"
}

resource "azurerm_windows_web_app" "app" {
  name                = "SpaceBattles"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  service_plan_id     = azurerm_service_plan.asp.id

  sticky_settings {
    app_setting_names = [
      "CosmosDB__AccountKey",
      "CosmosDB__DatabaseName",
      "CosmosDB__EndpointUrl"
    ]
  }

  app_settings = {
    "CosmosDB__AccountKey"   = ""
    "CosmosDB__DatabaseName" = ""
    "CosmosDB__EndpointUrl"  = ""
  }

  https_only = true
  identity {
    type = "SystemAssigned"
  }

  site_config {
    application_stack {
      dotnet_version = "v8.0"
    }

    ftps_state          = "FtpsOnly"
    always_on           = false
    http2_enabled       = true
    minimum_tls_version = "1.3"
  }
}