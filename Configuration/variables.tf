#variables
variable "resource_group_name_prefix" {
  type    = string
  default = "serverlessUpskill"
}

variable "location" {
  type        = string
  default     = "westeurope"
  description = "The Azure region in which resources will be created"
}

variable "failover_location" {
  type = string
  default = "westeurope"
}

variable "product_table_name"{
  type = string
  default = "products"
}

variable "environment_tag"{}

