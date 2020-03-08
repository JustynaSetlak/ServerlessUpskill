terraform {
  backend "azurerm" {
  }
}

#Configure the Azure provider
provider "azurerm" {
  version = "=1.44.0"
  subscription_id = "a0cad32f-8c4b-4749-8776-b207a38b8509"
}

# create resource group
resource "azurerm_resource_group" "resource" {
  name     = "${var.resource_group_name_prefix}-resources-${var.environment_tag}"
  location = var.location
  tags = {
    environment = var.environment_tag
  }
}

#create cosmos db
module "cosmosdb" {
  source       = "./modules/cosmosdb"
  resource_location = azurerm_resource_group.resource.location
  resource_name = azurerm_resource_group.resource.name
  account_name = "storedb-${var.environment_tag}"
  environment_tag = var.environment_tag
  failover_location = var.failover_location
  tables_to_create = {
    tableName = var.product_table_name
  }
}

#create functionApp
module "functionapp" {
  source       = "./modules/functionapp"
  resource_location = azurerm_resource_group.resource.location
  resource_name = azurerm_resource_group.resource.name
  environment_tag = var.environment_tag
  function_name = "${var.resource_group_name_prefix}-product-${var.environment_tag}"
}