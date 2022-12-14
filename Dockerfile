#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Net5ByDocker/Net5ByDocker.csproj", "Net5ByDocker/"]
RUN dotnet restore "Net5ByDocker/Net5ByDocker.csproj"  # 还原项目的Nuget包
COPY . .
WORKDIR "/src/Net5ByDocker"
RUN dotnet build "Net5ByDocker.csproj" -c Release -o /app/build # 在发布模式下生成项目。 生成工件将写入中间映像的 app/build/ 目录。

FROM build AS publish
RUN dotnet publish "Net5ByDocker.csproj" -c Release -o /app/publish # 在发布模式下发布项目。 已发布的捆绑将写入最终映像的 app/publish/ 目录。
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net5ByDocker.dll"] # 启动