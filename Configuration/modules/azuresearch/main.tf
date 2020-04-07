resource "azurerm_search_service" "azuresearch" {
  name                = "products-search-${var.environment_tag}"
  location            = var.resource_location
  resource_group_name = var.resource_group_name
  sku                 = "free"
}