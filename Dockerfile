#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Net5ByDocker/Net5ByDocker.csproj", "Net5ByDocker/"]
RUN dotnet restore "Net5ByDocker/Net5ByDocker.csproj"  # ��ԭ��Ŀ��Nuget��
COPY . .
WORKDIR "/src/Net5ByDocker"
RUN dotnet build "Net5ByDocker.csproj" -c Release -o /app/build # �ڷ���ģʽ��������Ŀ�� ���ɹ�����д���м�ӳ��� app/build/ Ŀ¼��

FROM build AS publish
RUN dotnet publish "Net5ByDocker.csproj" -c Release -o /app/publish # �ڷ���ģʽ�·�����Ŀ�� �ѷ���������д������ӳ��� app/publish/ Ŀ¼��
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net5ByDocker.dll"] # ����