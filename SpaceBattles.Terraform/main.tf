terraform {
  required_version = ">= 1.14.3"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "4.58.0"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = "8ad9c54e-c375-4ed2-a58c-d843ece82e46"
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "resource_group" {
  name     = "SpaceBattles"
  location = var.Location
}