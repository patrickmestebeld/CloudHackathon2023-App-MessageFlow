. .\PsFunctions\ReadEnvVariables.ps1
$resourceGroupName = "rg-$env:BASE_NAME-$env:ENVIRONMENT"

# Delete the resource group
az group delete --name $resourceGroupName --yes
