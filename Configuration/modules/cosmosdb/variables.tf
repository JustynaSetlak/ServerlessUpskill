variable "environment_tag"{
  type = string
  default = "test"
}

variable "account_name" {
    type = string
    default = "storedb"
}

variable "resource_location" {
    type = string
    default = "westeurope"
}

variable "resource_name" {
    type = string
    default = ""
}

variable "failover_location" {
    type = string
    default = "westeurope"
}

variable "tables_to_create"{}