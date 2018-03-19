# cad-service

# To use Microsoft CORS package, you need to install from NuGet package.
 
Go to Tools Menu-> Library Package Manager -> Package Manager Console -> execute the below command.
 
`Install-Package Microsoft.AspNet.WebApi.Cors`
http://www.c-sharpcorner.com/article/enable-cors-in-asp-net-webapi-2/

# Build production version
Comment line 18 - 19 in WebApiConfig.cs

# Install URL Rewrite on IIS server.

URL Rewrite installer download link: https://www.iis.net/downloads/microsoft/url-rewrite
