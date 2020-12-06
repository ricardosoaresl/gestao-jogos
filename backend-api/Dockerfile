#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["1-Presentation/GestaoJogos.Presentation.Api/GestaoJogos.Presentation.Api.csproj", "1-Presentation/GestaoJogos.Presentation.Api/"]
COPY ["5-CrossCutting/GestaoJogos.CrossCutting.Validation/GestaoJogos.CrossCutting.Validation.csproj", "5-CrossCutting/GestaoJogos.CrossCutting.Validation/"]
COPY ["5-CrossCutting/GestaoJogos.CrossCutting.Auditing/GestaoJogos.CrossCutting.Auditing.csproj", "5-CrossCutting/GestaoJogos.CrossCutting.Auditing/"]
COPY ["5-CrossCutting/GestaoJogos.CrossCutting.Notification/GestaoJogos.CrossCutting.Notification.csproj", "5-CrossCutting/GestaoJogos.CrossCutting.Notification/"]
COPY ["4-Infrastructure/GestaoJogos.Infrastructure.EntityFramework/GestaoJogos.Infrastructure.EntityFramework.csproj", "4-Infrastructure/GestaoJogos.Infrastructure.EntityFramework/"]
COPY ["3-Domain/GestaoJogos.Domain.Principal/GestaoJogos.Domain.Principal.csproj", "3-Domain/GestaoJogos.Domain.Principal/"]
COPY ["6-SharedKernel/GestaoJogos.SharedKernel.Infrastructure/GestaoJogos.SharedKernel.Infrastructure.csproj", "6-SharedKernel/GestaoJogos.SharedKernel.Infrastructure/"]
COPY ["6-SharedKernel/GestaoJogos.SharedKernel.Application/GestaoJogos.SharedKernel.Application.csproj", "6-SharedKernel/GestaoJogos.SharedKernel.Application/"]
COPY ["2-Application/GestaoJogos.Application.ApplicationServices/GestaoJogos.Application.ApplicationServices.csproj", "2-Application/GestaoJogos.Application.ApplicationServices/"]
RUN dotnet restore "1-Presentation/GestaoJogos.Presentation.Api/GestaoJogos.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/1-Presentation/GestaoJogos.Presentation.Api"
RUN dotnet build "GestaoJogos.Presentation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestaoJogos.Presentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestaoJogos.Presentation.Api.dll"]