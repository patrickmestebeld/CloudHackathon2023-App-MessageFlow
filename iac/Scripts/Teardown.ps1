. .\PsFunctions\ReadEnvVariables.ps1

# Delete the resource group
az group delete --name $env:RESOURCE_GROUP_NAME --yes
