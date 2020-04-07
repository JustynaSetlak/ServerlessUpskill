
resource "azurerm_storage_account" "storageAccount" {
  resource_group_name = var.resource_group_name
  location = var.resource_location
  name = "${var.environment_tag}storageaccount"
  account_replication_type = "LRS"
  account_tier = "Standard"

  tags = {
    environment = var.environment_tag
  }
}

resource "azurerm_storage_table" "categories_storage_table" {
  name                 = "Categories"
  storage_account_name = azurerm_storage_account.storageAccount.name
}
