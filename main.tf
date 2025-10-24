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

variable "region" {
  type    = string
  default = "France central"
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
  location            = "${var.region}"
  os_type             = "Windows"
  sku_name            = "F1"
}

# terraform import azurerm_windows_web_app.app /subscriptions/8ad9c54e-c375-4ed2-a58c-d843ece82e46/resourceGroups/SpaceBattles/providers/Microsoft.Web/sites/SpaceBattles
resource "azurerm_windows_web_app" "app" {
  name                = "SpaceBattles"
  resource_group_name = azurerm_resource_group.rg.name
  location            = "${var.region}"
  service_plan_id     = azurerm_service_plan.asp.id

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

resource "azurerm_cosmosdb_account" "cosmos" {
  name                = "test-cosno"
  location            = "${var.region}"
  resource_group_name = azurerm_resource_group.rg.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  automatic_failover_enabled = false

  consistency_policy {
    consistency_level       = "Eventual"
  }

  geo_location {
    location          = "eastus"
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_sql_database" "cosmos" {
  name                = "spacebattles"
  resource_group_name = azurerm_resource_group.rg.name
  account_name        = azurerm_cosmosdb_account.cosmos.name
}

resource "azurerm_cosmosdb_sql_container" "example" {
  name                  = "example-container"
  resource_group_name   = azurerm_cosmosdb_account.cosmos.resource_group_name
  account_name          = azurerm_cosmosdb_account.cosmos.name
  database_name         = azurerm_cosmosdb_sql_database.cosmos.name
  partition_key_paths   = ["/definition/id"]
  partition_key_version = 1
  throughput            = 400

  indexing_policy {
    indexing_mode = "consistent"

    included_path {
      path = "/*"
    }

    included_path {
      path = "/included/?"
    }

    excluded_path {
      path = "/excluded/?"
    }
  }

  unique_key {
    paths = ["/definition/idlong", "/definition/idshort"]
  }
}
