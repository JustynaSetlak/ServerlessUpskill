resource "azurerm_app_service_plan" "appService" {
  name                = "app-service-plan-${var.environment_tag}"
  location            = var.resource_location
  resource_group_name = var.resource_name

  sku {
    tier = "Standard"
    size = "S1"
  }

  tags = {
    environment = var.environment_tag
  }
}

resource "azurerm_storage_account" "storageAccount" {
  resource_group_name = var.resource_name
  location = var.resource_location
  name = "funcstorageaccount${var.environment_tag}"
  account_replication_type = "LRS"
  account_tier = "Standard"

  tags = {
    environment = var.environment_tag
  }
}

resource "azurerm_function_app" "functionApp" {
  name                      = var.function_name
  location                  = var.resource_location
  resource_group_name       = var.resource_name
  app_service_plan_id       = azurerm_app_service_plan.appService.id
  storage_connection_string = azurerm_storage_account.storageAccount.primary_connection_string
}