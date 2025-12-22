terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "4.52.0"
    }
  }
}

provider "azurerm" {
  features {  }
  subscription_id = "8ad9c54e-c375-4ed2-a58c-d843ece82e46"
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "resource_group" {
  name     = "SpaceBattles"
  location = "West Europe"
}

resource "azurerm_service_plan" "asp" {
  name                = "spacebattles-asp"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.region
  os_type             = "Windows"
  sku_name            = "F1"
}

resource "azurerm_windows_web_app" "app" {
  name                = "SpaceBattles"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.region
  service_plan_id     = azurerm_service_plan.asp.id

  sticky_settings {
    app_setting_names = [
      "CosmosDB__AccountKey",
      "CosmosDB__DatabaseName",
      "CosmosDB__EndpointUrl"
    ]
  }
  
  app_settings = {
    "CosmosDB__AccountKey" = ""
    "CosmosDB__DatabaseName" = ""
    "CosmosDB__EndpointUrl" = ""
  }

  https_only = true
  identity {
    type = "SystemAssigned"
  }

  site_config {
    application_stack {
      dotnet_version = "v8.0"
    }

    ftps_state = "FtpsOnly"
    always_on           = false
    http2_enabled       = true
    minimum_tls_version = "1.3"
  }
}