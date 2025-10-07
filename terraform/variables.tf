variable "aws_region" {
  description = "AWS region"
  type        = string
  default     = "eu-west-1"
}

variable "environment" {
  description = "Environment name"
  type        = string
  default     = "dev"
}

variable "project_name" {
  description = "Project name"
  type        = string
  default     = "highfield-tech-test"
}

variable "lambda_zip_path" {
  description = "Path to Lambda deployment package"
  type        = string
  default     = "../deployment-package.zip"
}

variable "frontend_build_path" {
  description = "Path to frontend build directory"
  type        = string
  default     = "../HighfieldTechTest.Web/dist"
}