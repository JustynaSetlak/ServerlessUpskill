resource "azurerm_cosmosdb_account" "dbaccount" {
  name                = var.account_name
  location            = var.resource_location
  resource_group_name = var.resource_name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  enable_automatic_failover = true

  consistency_policy {
    consistency_level = "Eventual"
  }

  geo_location {
    location          = var.failover_location
    failover_priority = 0
  }
  
  capabilities {
    name = "EnableTable"
  }

  tags = {
    environment = var.environment_tag
  }
 }

 resource "azurerm_cosmosdb_table" "tables" {
    for_each = var.tables_to_create
        name                = each.value
        resource_group_name = azurerm_cosmosdb_account.dbaccount.resource_group_name
        account_name        = azurerm_cosmosdb_account.dbaccount.name  
}