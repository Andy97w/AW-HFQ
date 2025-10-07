output "api_gateway_url" {
  value = "https://${aws_api_gateway_rest_api.api.id}.execute-api.${var.aws_region}.amazonaws.com/${aws_api_gateway_stage.api.stage_name}"
}

output "frontend_url" {
  value = "http://${aws_s3_bucket.frontend.bucket}.s3-website-${var.aws_region}.amazonaws.com"
}

output "lambda_function_name" {
  value = aws_lambda_function.api.function_name
}

output "s3_bucket_name" {
  value = aws_s3_bucket.frontend.bucket
}