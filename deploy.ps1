#!/usr/bin/env pwsh
[CmdletBinding()]
param(
  [string]$Environment = "dev",
  [string]$Region = "eu-west-1",
  [switch]$Destroy
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Section($t,$c='Yellow'){ Write-Host "`n=== $t ===" -ForegroundColor $c }

Write-Host "Deployment - Highfield Tech Test" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Cyan
Write-Host "Region: $Region" -ForegroundColor Cyan

Push-Location terraform
try {
  Section "Terraform Init"
  terraform init

  if ($Destroy) {
    Section "Destroy Infrastructure" Red
    terraform destroy -var="environment=$Environment" -var="aws_region=$Region" -auto-approve
    Pop-Location
    return
  }

  Section ".NET Lambda Build"
  Push-Location ../HighfieldTechTest.Api
  dotnet restore
  dotnet build -c Release --no-restore
  dotnet lambda package --configuration Release --framework net8.0 --output-package ../deployment-package.zip
  Pop-Location

  Section "Terraform Apply (Infra & Lambda)"
  terraform apply -var="environment=$Environment" -var="aws_region=$Region" -auto-approve

  $apiUrl = terraform output -raw api_gateway_url
  $frontendUrl = terraform output -raw frontend_url

  Section "Building Frontend with Real API URL"
  Push-Location ../HighfieldTechTest.Web
  $envFile = "VITE_API_BASE_URL=$apiUrl`n"
  if ($PSVersionTable.PSEdition -eq 'Core') {
    Set-Content -Path ".env.production" -Value $envFile -Encoding utf8NoBOM
  } else {
    Set-Content -Path ".env.production" -Value $envFile -Encoding UTF8
  }
  if (Test-Path package-lock.json) { npm ci } else { npm install }
  npm run build
  Pop-Location

  Section "Uploading Frontend Assets"
  terraform apply -var="environment=$Environment" -var="aws_region=$Region" -auto-approve

  Section "Done" Green
  Write-Host "API URL: $apiUrl" -ForegroundColor Cyan
  Write-Host "Frontend URL: $frontendUrl" -ForegroundColor Cyan
}
catch {
  Write-Error "Deployment failed: $($_.Exception.Message)"
  exit 1
}
finally {
  Pop-Location
}