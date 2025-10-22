terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "4.49.0"
    }
  }
}

provider "azurerm" {
  features { }
  subscription_id = "8ad9c54e-c375-4ed2-a58c-d843ece82e46"
}

# terraform import azurerm_resource_group.rg /subscriptions/8ad9c54e-c375-4ed2-a58c-d843ece82e46/resourceGroups/SpaceBattles
resource "azurerm_resource_group" "rg" {
  name     = "SpaceBattles"
  location = "West Europe"
}

# terraform import azurerm_service_plan.asp /subscriptions/8ad9c54e-c375-4ed2-a58c-d843ece82e46/resourceGroups/SpaceBattles/providers/Microsoft.Web/serverFarms/spacebattles-asp
resource "azurerm_service_plan" "asp" {
  name                = "spacebattles-asp"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = "Windows"
  sku_name            = "F1"
}

# terraform import azurerm_windows_web_app.app /subscriptions/8ad9c54e-c375-4ed2-a58c-d843ece82e46/resourceGroups/SpaceBattles/providers/Microsoft.Web/sites/SpaceBattles
resource "azurerm_windows_web_app" "app" {
  name                = "SpaceBattles"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_service_plan.asp.location
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
    
    always_on           = false
    http2_enabled       = true
    minimum_tls_version = "1.3"
    
  }
}