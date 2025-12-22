locals {
  base_command = "/subscriptions/${data.azurerm_client_config.current.subscription_id}/resourceGroups/${azurerm_resource_group.resource_group.name}"
}

output "resource_group_import_command" {
  description = "Import command for the resource group"
  value = "terraform import azurerm_resource_group.resource_group ${local.base_command}"
}

output "asp_import_command" {
  description = "Import command for the app service plan"
  value = "terraform import azurerm_service_plan.asp ${local.base_command}/providers/Microsoft.Web/serverFarms/${azurerm_service_plan.asp.name}"
}

output "app_import_command" {
  description = "Import command for the app service"
  value = "terraform import azurerm_windows_web_app.app ${local.base_command}/providers/Microsoft.Web/sites/${azurerm_windows_web_app.app.name}"
}